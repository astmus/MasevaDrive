using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using System.Threading;
using System.Drawing;
using Telegram.Bot.Types.ReplyMarkups;
using System.IO.MemoryMappedFiles;
using FrameworkData.Settings;
using FrameworkData;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using MasevaDriveService.Telegram.GMessages;
using System.Net.Http;
using Telegram.Bot.Types.InlineQueryResults;
using System.Diagnostics;
using Telegram.Bot.Types.InputFiles;
using System.Collections.Concurrent;

namespace MasevaDriveService
{	
	public class TelegramClient
	{
		private TelegramBotClient Bot;
		private Dictionary<string, long> Subscribers { get; set; } = new Dictionary<string, long>();

		private static readonly Lazy<TelegramClient> lazy = new Lazy<TelegramClient>(() => new TelegramClient());
		private static readonly ConcurrentBag<MemoryMappedFile> memoryFiles = new ConcurrentBag<MemoryMappedFile>();
		public static TelegramClient Instance { get { return lazy.Value; } }
		public Action<string> ErrorOcccured { get; set; }
		public static Stack<Message> MessagesHystory = new Stack<Message>();
		public IOrderedEnumerable<StorageItemInfo> lastFilesQuery;
		public static readonly string urlFormat = "http://astmus.com/giveout/{0}.jpg";
		public static readonly string urlTHFormat = "http://astmus.com/giveout/thum{0}.jpg";

		private TelegramClient()
		{			
			Bot = new TelegramBotClient(Environment.GetEnvironmentVariable("MasevaBotKey", EnvironmentVariableTarget.User));			
			Bot.Timeout = TimeSpan.FromMinutes(5);
			LoadSubscribers();
			Bot.OnMessage += OnMessageRecieved;
			Bot.OnCallbackQuery += Bot_OnCallbackQuery;			
			Bot.OnInlineQuery += Bot_OnInlineQuery;
			Bot.OnInlineResultChosen += Bot_OnInlineResultChosen;
			Bot.OnReceiveGeneralError += Bot_OnReceiveGeneralError;
			//Bot.OnReceiveError += BotOnReceiveError;
			//Bot.StopReceiving();			
		}

		private void Bot_OnReceiveGeneralError(object sender, ReceiveGeneralErrorEventArgs e) => ErrorOcccured?.Invoke(e.Exception.Message);

		private void Bot_OnInlineResultChosen(object sender, ChosenInlineResultEventArgs e)
		{
			//int i = 0;
			//e.ChosenInlineResult.
		}

		private async void Bot_OnInlineQuery(object sender, InlineQueryEventArgs e)
		{			
			int page = 0;			
			if (lastFilesQuery == null)
				await Task.FromResult(1);
			else
			{				
				int.TryParse(e.InlineQuery.Offset, out page);

				const int batchSize = 30;
				var accessor = lastFilesQuery.Skip(page * batchSize).Take(batchSize);				
				InlineQueryResultBase[] result = accessor.Select((file) => PhotoResult(file)).ToArray();

				var thrds = lastFilesQuery.Skip(page * batchSize).Take(batchSize).AsParallel().WithMergeOptions(ParallelMergeOptions.NotBuffered);
				Console.WriteLine("make file started " + thrds.Count());				
				thrds.ForAll(storageItem =>
				{
					if (!storageItem.IsFile)
						return;
					var mapa = MemoryMappedFile.CreateOrOpen(storageItem.Hash, 8.KB(), MemoryMappedFileAccess.ReadWrite, MemoryMappedFileOptions.DelayAllocatePages, HandleInheritability.None);
					var thumb = mapa.CreateViewStream();
					var command = FFTools.CreateInlineThumbnail(storageItem.FullPath, 120, ref thumb);
					command.Wait();					
					thumb.Close();					
					memoryFiles.Add(mapa);
				});

				string nextPage = result.Length < batchSize ? "" :(page += 1).ToString();
				try
				{					
					await Task.Delay(1000).ContinueWith(t => Bot.AnswerInlineQueryAsync(e.InlineQuery.Id, result, 0, false, nextPage));	
				}catch(Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
				
				Console.WriteLine("method ended");
				Console.WriteLine("return");
			}			
		}

		private InlineQueryResultBase PhotoResult(StorageItemInfo file)
		{
			string url = string.Format(urlFormat, file.Hash);					

			return new InlineQueryResultPhoto(id: file.Hash, url, url)
			{
				Caption = file.Name + " Capt",
				ParseMode = ParseMode.Html,
				PhotoWidth = 100,
				PhotoHeight = 100,
				Description = file.Name + "Desc"
			};
		}

		private async void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
		{			
			QueryHandler handler = e.CallbackQuery.Parse(Bot);			
			try
			{
				await handler.ReplyToRequest();
			}
			catch (Exception error)
			{
				RaiseError(error.ToString());
				/*var pair = Subscribers.FirstOrDefault(kv => kv.Value == e.CallbackQuery.From.Id);
				Bot.SendChatActionAsync(e.CallbackQuery.From.Id,ChatAction.UploadAudio,*/
			}
			#region commented
			/*
			 * var hash = e.CallbackQuery.Data.Substring(2);			
			if (StorageItemsProvider.Instance[hash] != null)
				await Bot.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId);
			
			if (result == MessageConfirmLevel.NeedConfirm)
				await Bot.EditMessageReplyMarkupAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId, MakeDeleteKeyboard(hash,MessageConfirmLevel.WaitDecision), default(CancellationToken));
			else
				await Bot.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId);*/
			/*if (RequestDeleteFile != null)
				RequestDeleteFile(e.CallbackQuery.Data);*/
			#endregion
		}

		public void RaiseError(string errorMessage)
		{
			ErrorOcccured?.Invoke(errorMessage);
		}

		public async void SendNotifyFileLoadSuccess(StorageItemInfo item)
		{
			TelegramContext.WithFileHash(item.Hash);
			if (!Subscribers.ContainsKey(item.Owner)) return;
			var ChatId = Subscribers[item.Owner];
			try
			{
				try
				{					
					using (MemoryStream imageStream = FFTools.CreateThumbnail(item.FullPath))
					{
						Message result = await Bot.SendPhotoAsync(
						ChatId,
						imageStream,
						string.Format("{0} ({1}) done", item.Name, item.GetFormatSize()), replyMarkup: Keyboards.CommonFileActions());
						MessagesHystory.Push(result);
					}
				}
				catch (Exception)
				{
					await Bot.SendTextMessageAsync(ChatId, string.Format("{0} ({1}) done", item.Name, item.GetFormatSize()));
				}
			}
			catch (Exception)
			{
				await Task.Delay(3000).ContinueWith(async (a) =>
				{
					await Bot.SendTextMessageAsync(ChatId, string.Format("{0} ({1}) done", item.Name, item.GetFormatSize()));
				});
			}
		}		

		public async void SendNotifyAboutError(string errorMessage)
		{
			await Bot.SendTextMessageAsync(Subscribers["astmus@live.com"], errorMessage);
		}

		public async void SendNotifyAboutSyncError(string email, string errorMessage)
		{
			if (Subscribers.ContainsKey(email))
				try
				{
					await Bot.SendTextMessageAsync(Subscribers[email], errorMessage);
				}
				catch (System.Exception)
				{
					await Task.Delay(3000).ContinueWith(async (a) =>
					{
						await Bot.SendTextMessageAsync(Subscribers[email], errorMessage);
					});
				}
		}

		public void StartService()
		{
					
			//Bot.StartReceiving();
		}		

		public void StopService()
		{
			//Bot.StopReceiving();
		}

		private void LoadSubscribers()
		{
			try
			{
				Subscribers = JsonConvert.DeserializeObject<Dictionary<string, long>>(SolutionSettings.Default.TelegramSubscribers);
			}
			catch (System.Exception)
			{
				//SaveSubscribers();
			}
		}
		
		private async void OnMessageRecieved(object sender, MessageEventArgs e)
		{
			 var message = e.Message;
			
			if (message == null || message.Type == MessageType.Document)
			{
				var res = await Bot.GetFileAsync(e.Message.Document.FileId);
			}

			if (message == null || message.Type == MessageType.Video)
			{
				var res = await Bot.GetFileAsync(e.Message.Video.FileId);
			}

			if (message == null || message.Type == MessageType.Photo)
			{
				
			}


			if (message == null || message.Type != MessageType.Text)				
				return;

			switch (message.Text.Split(' ').First())
			{
				// send inline keyboard
				case "/start":
					await Bot.SendTextMessageAsync(message.Chat.Id,	"Hello. Please use 'subscribe' command with enter your email for get notifications");
					break;
				case "/subscribe":
					var splitted = message.Text.Split(' ');
					if (splitted.Length == 1)
					{
						await Bot.SendTextMessageAsync(message.Chat.Id, "You are not subscribed enter command with email");
						return;
					}
					var email = splitted[1];
					Subscribers.Add(email, message.Chat.Id);
					//SaveSubscribers();
					await Bot.SendTextMessageAsync(message.Chat.Id,	"You are subscribed to notification for "+email);
					break;
					case "/mainmenu":
					await Bot.SendMainMenu(message.Chat.Id);
					break;
				case "/notifications_on":
					await Bot.SendTextMessageAsync(message.Chat.Id, "вы подписались на уведомления о загрузке файлов");
					break;
				case "/lastfiles":
					///var request = "<a href=\"https://t.me/MasevaDriveBot?filestoday=image1234_56789">image name</a>";					
					await Bot.SendTextMessageAsync(message.Chat.Id, "сформировать запрос", ParseMode.Default, false, false, 0, new InlineKeyboardMarkup(InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("сформировать запрос", DateTime.Now.TimeOfDay.ToString().Substring(8))), default);;
					lastFilesQuery = StorageItemsProvider.Instance.Items.Values.OrderByDescending(file => new DateTime(Math.Max(file.DateTimeOriginal.Ticks, file.DateTimeCreation.Ticks)));
					
					break;
				case "/filestoday1":
					var splitteds = message.Text.Split('_');
					var url = string.Format(urlFormat, splitteds[1]);
					try
					{						
						await Bot.SendPhotoAsync(message.Chat.Id, new InputOnlineFile(url));
					}
					catch { }
					break;
				case "/wh":
					
					break;
					#region
					// send custom keyboard
					/*case "/keyboard":
						ReplyKeyboardMarkup ReplyKeyboard = new[]
						{
							new[] { "1.1", "1.2" },
							new[] { "2.1", "2.2" },
						};

						await Bot.SendTextMessageAsync(
							message.Chat.Id,
							"Choose",
							replyMarkup: ReplyKeyboard);
						break;
					case "/rootfolderlist":
						var subFolders = Directory.GetDirectories(selectedFolder);
						var items = subFolders.Count() != 0 ? subFolders.Select(folder => folder.Split(Path.DirectorySeparatorChar).Last()) : Directory.GetFiles(selectedFolder).Select(f => Path.GetFileName(f));

						ReplyKeyboardMarkup keyboardWithFolders = new ReplyKeyboardMarkup(
							items.Select(folder => new[] { new KeyboardButton(folder) }).Take(20));
						await Bot.SendTextMessageAsync(
							message.Chat.Id,
							"Choose",
							replyMarkup: keyboardWithFolders);
						;
						break;
					// send a photo
					case "/photo":
						await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

						const string file = @"d:\Temp\1.jpg";

						var fileName = file.Split(Path.DirectorySeparatorChar).Last();

						using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
						{
							await Bot.SendPhotoAsync(
								message.Chat.Id,
								fileStream,
								"Nice Picture");
						}
						break;

					// request location or contact
					case "/request":
						var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
						{
							KeyboardButton.WithRequestLocation("Location"),
							KeyboardButton.WithRequestContact("Contact"),
						});

						await Bot.SendTextMessageAsync(
							message.Chat.Id,
							"Who or Where are you?",
							replyMarkup: RequestReplyKeyboard);
						break;

					default:
						if (message.Text.ToLower().EndsWith(".jpg"))
						{
							var pathToFile = Path.Combine(selectedFolder, message.Text);
							Image image = Image.FromFile(pathToFile);
							var aspect = (double)image.Size.Width / (double)image.Size.Height;
							Image thumb = image.GetThumbnailImage((int)(480 * aspect), 480, () => false, IntPtr.Zero);
							using (MemoryStream imageStream = new MemoryStream())
							{
								thumb.Save(imageStream, ImageFormat.Jpeg);
								imageStream.Position = 0;
								await Bot.SendPhotoAsync(
								message.Chat.Id,
								imageStream,
								null);
							}
							return;
							// put the image into the memory stream						
						}

						selectedFolder = Path.Combine(selectedFolder, message.Text);
						try
						{
							var sFolders = Directory.GetDirectories(selectedFolder);
							var itms = sFolders.Count() != 0 ? sFolders.Select(folder => folder.Split(Path.DirectorySeparatorChar).Last()) : Directory.GetFiles(selectedFolder).Select(f => Path.GetFileName(f));
							var buttons = itms.Select(folder => new[] { new KeyboardButton(folder) }).Take(20).ToArray();
							ReplyKeyboardMarkup _keyboardWithFolders = new ReplyKeyboardMarkup(buttons);
							await Bot.SendTextMessageAsync(
								message.Chat.Id,
								null,
								replyMarkup: _keyboardWithFolders);
							;
						}
						catch (Exception ex)
						{
							await Bot.SendTextMessageAsync(
							message.Chat.Id,
							"Wrong command",
							replyMarkup: new ReplyKeyboardRemove());
						}

						break;*/
					#endregion
			}
		}
	
		private void InitQuery()
		{
			if (lastFilesQuery == null)
				lastFilesQuery = StorageItemsProvider.Instance.Items.Values.OrderByDescending(file => new DateTime(Math.Max(file.DateTimeOriginal.Ticks, file.DateTimeCreation.Ticks)));
		}
	}

	
}

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

namespace MasevaDriveService
{
	public class TelegramClient
	{
		private TelegramBotClient Bot;
		private Dictionary<string, long> Subscribers { get; set; } = new Dictionary<string, long>();

		private static readonly Lazy<TelegramClient> lazy = new Lazy<TelegramClient>(() => new TelegramClient());
		public static TelegramClient Instance { get { return lazy.Value; } }
		public Action<string> ErrorOcccured { get; set; }

		private TelegramClient()
		{			
			Bot = new TelegramBotClient(SolutionSettings.Default.TelegramSecretKey);
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

		private void Bot_OnInlineQuery(object sender, InlineQueryEventArgs e)
		{
			
		}

		private async void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
		{
			GMessage message = e.CallbackQuery.Parse(Bot);
			await message.Replay();
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
		}

		public async void SendNotifyFileLoadSuccess(string email, string fileName, string formattedSize, string pathToLoadedFile)
		{
			if (!Subscribers.ContainsKey(email)) return;
			var ChatId = Subscribers[email];
			try
			{
				try
				{
					var board = Keyboards.CommonFileActionsKeyboard(pathToLoadedFile.ToHash());

					using (MemoryStream imageStream = FFTools.CreateThumbnail(pathToLoadedFile))
					{
						await Bot.SendPhotoAsync(
						ChatId,
						imageStream,
						string.Format("{0} ({1}) done", fileName, formattedSize), replyMarkup: board);
					}
				}
				catch (Exception ex)
				{
					await Bot.SendTextMessageAsync(ChatId, string.Format("{0} ({1}) done", fileName, formattedSize));
				}
			}
			catch (System.Exception ex)
			{
				await Task.Delay(3000).ContinueWith(async (a) =>
				{
					await Bot.SendTextMessageAsync(ChatId, string.Format("{0} ({1}) done", fileName, formattedSize));
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
				catch (System.Exception ex)
				{
					await Task.Delay(3000).ContinueWith(async (a) =>
					{
						await Bot.SendTextMessageAsync(Subscribers[email], errorMessage);
					});
				}
		}

		public void StartService()
		{
			LoadSubscribers();			
			Bot.StartReceiving();
		}		

		public void StopService()
		{
			Bot.StopReceiving();
		}

		private void LoadSubscribers()
		{
			try
			{
				Subscribers = JsonConvert.DeserializeObject<Dictionary<string, long>>(SolutionSettings.Default.TelegramSubscribers);
			}
			catch (System.Exception ex)
			{
				//SaveSubscribers();
			}
		}
		
		private async void OnMessageRecieved(object sender, Telegram.Bot.Args.MessageEventArgs e)
		{
			 var message = e.Message;

			if (message == null || message.Type != MessageType.Text) return;

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


	}

	
}

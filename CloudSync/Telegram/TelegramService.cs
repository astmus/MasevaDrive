using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using CloudSync.Models;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using CloudSync.Windows;
using Medallion.Shell;
using Telegram.Bot.Types.ReplyMarkups;
using System.IO.MemoryMappedFiles;

namespace CloudSync.Telegram
{
	static class TelegramService
	{		
		private static readonly TelegramBotClient Bot;
		private static Dictionary<string, long> Subscribers { get; set; } = new Dictionary<string, long>();
		public static event Action<string> RequestDeleteFile;
		static TelegramService()
		{
			Bot = new TelegramBotClient("688413717:AAELvIkuj37vBedxvzIgtWsjZio8_B4QlR0");
			Bot.OnMessage += OnMessageRecieved;
			Bot.OnCallbackQuery += Bot_OnCallbackQuery;			
			Bot.OnInlineQuery += Bot_OnInlineQuery;
			Bot.OnInlineResultChosen += Bot_OnInlineResultChosen; ;
			//Bot.OnReceiveError += BotOnReceiveError;
			//Bot.StopReceiving();
		}

		private static void Bot_OnInlineResultChosen(object sender, global::Telegram.Bot.Args.ChosenInlineResultEventArgs e)
		{
			int i = 0;
		}

		private static void Bot_OnInlineQuery(object sender, global::Telegram.Bot.Args.InlineQueryEventArgs e)
		{
			int i = 0;
		}

		private async static void Bot_OnCallbackQuery(object sender, global::Telegram.Bot.Args.CallbackQueryEventArgs e)
		{
			await Bot.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId);
			if (RequestDeleteFile != null)
				RequestDeleteFile(e.CallbackQuery.Data);
		}

		public static async void SendNotifyFileLoadDone(string email, OneDriveSyncItem file, string pathToLoadedFile)
		{
			if (!Subscribers.ContainsKey(email)) return;
			var ChatId = Subscribers[email];
			try
			{
				try
				{
					var hashOfPath = pathToLoadedFile.ToHash();
					var RequestReplyKeyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[]
					{
							InlineKeyboardButton.WithCallbackData("удалить",hashOfPath),
							InlineKeyboardButton.WithUrl("скачать","http://192.168.0.103:9090/hid="+hashOfPath)
					});

					using (MemoryStream imageStream = CreateThumbnailOfFile(pathToLoadedFile))
					{
						await Bot.SendPhotoAsync(
						ChatId,
						imageStream,
						string.Format("{0} ({1}) done", file.Name, file.FormattedSize), replyMarkup: RequestReplyKeyboard);
					}
				}
				catch (Exception ex)
				{
					await Bot.SendTextMessageAsync(ChatId, string.Format("{0} ({1}) done", file.Name, file.FormattedSize));
				}
			}
			catch (System.Exception ex)
			{
				await Task.Delay(3000).ContinueWith(async (a) =>
				{
					await Bot.SendTextMessageAsync(ChatId, string.Format("{0} ({1}) done", file.Name, file.FormattedSize));
				});
			}
		}

		private static MemoryStream CreateThumbnailOfFile(string filePath)
		{
			string args = string.Format("-i \"{0}\" -qscale:v 5 -vf scale=\"360:-1\" -vframes 1 -f image2pipe pipe:1", filePath);				
			var createThumbnailProcess = Command.Run(Settings.Instance.AppProperties.FFmpegPath, null, options => options.StartInfo((i) =>
			{
				i.Arguments = args;
				i.RedirectStandardOutput = true;
				i.UseShellExecute = false;
			}));			
			
			var result = new MemoryStream();
			createThumbnailProcess.RedirectTo(result);
			createThumbnailProcess.Wait();
			
			result.Seek(0,SeekOrigin.Begin);
			return result;
		}

		public static void SendNotifyAboutDeleteFileError(string errorMessage)
		{
			Bot.SendTextMessageAsync(Subscribers["astmus@live.com"], errorMessage);
		}

		public static async void SendNotifyAboutSyncError(string email, string errorMessage)
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

		private async static void OnMessageRecieved(object sender, global::Telegram.Bot.Args.MessageEventArgs e)
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
					SaveSubscribers();
					await Bot.SendTextMessageAsync(message.Chat.Id,	"You are subscribed to notification for "+email);
					break;

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
			}
		}

		public static void StartService()
		{
			LoadSubscribers();
			Bot.StartReceiving();
		}

		public static void StopService()
		{
			Bot.StopReceiving();
		}

		private static void SaveSubscribers()
		{
			using (StreamWriter file = File.CreateText(@"subscribers"))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, Subscribers);
			}
		}

		private static void LoadSubscribers()
		{
			try
			{
				using (StreamReader file = File.OpenText(@"subscribers"))
				{
					JsonSerializer serializer = new JsonSerializer();
					Subscribers = (Dictionary<string, long>)serializer.Deserialize(file, typeof(Dictionary<string, long>));
				}
			}
			catch (System.Exception ex)
			{
				SaveSubscribers();
			}
		}
	}
}

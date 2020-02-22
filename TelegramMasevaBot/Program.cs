using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramMasevaBot
{
	class Program
	{
		private static readonly TelegramBotClient Bot = new TelegramBotClient("688413717:AAELvIkuj37vBedxvzIgtWsjZio8_B4QlR0");
		private static string RootFolder = @"z:\Images&Video\";
		private static string selectedFolder = RootFolder;
		static void Main(string[] args)
		{
			var me = Bot.GetMeAsync().Result;
			Console.Title = me.Username;

			Bot.OnMessage += BotOnMessageReceived;
			Bot.OnMessageEdited += BotOnMessageReceived;
			Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
			Bot.OnInlineQuery += BotOnInlineQueryReceived;
			Bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
			Bot.OnReceiveError += BotOnReceiveError;

			Bot.StartReceiving();
			Console.WriteLine($"Start listening for @{me.Username}");
			Console.ReadLine();
			Bot.StopReceiving();
		}

		private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
		{
			var message = messageEventArgs.Message;

			if (message == null || message.Type != MessageType.Text) return;

			switch (message.Text.Split(' ').First())
			{
				// send inline keyboard
				case "/inline":
					await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

					await Task.Delay(500); // simulate longer running task

					var inlineKeyboard = new InlineKeyboardMarkup(new[]
					{
						new [] // first row
                        {
							InlineKeyboardButton.WithCallbackData("1.1"),
							InlineKeyboardButton.WithCallbackData("1.2"),
						},
						new [] // second row
                        {
							InlineKeyboardButton.WithCallbackData("2.1"),
							InlineKeyboardButton.WithCallbackData("2.2"),
						}
					});

					await Bot.SendTextMessageAsync(
						message.Chat.Id,
						"Choose",
						replyMarkup: inlineKeyboard);
					break;

				// send custom keyboard
				case "/keyboard":
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
						Image thumb = image.GetThumbnailImage((int)(480*aspect), 480, () => false, IntPtr.Zero);
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
					/**/
					break;
			}
		}

		private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
		{
			var callbackQuery = callbackQueryEventArgs.CallbackQuery;

			await Bot.AnswerCallbackQueryAsync(
				callbackQuery.Id,
				$"Received {callbackQuery.Data}");

			await Bot.SendTextMessageAsync(
				callbackQuery.Message.Chat.Id,
				$"Received {callbackQuery.Data}");
		}

		private static async void BotOnInlineQueryReceived(object sender, InlineQueryEventArgs inlineQueryEventArgs)
		{
			Console.WriteLine($"Received inline query from: {inlineQueryEventArgs.InlineQuery.From.Id}");

			InlineQueryResultBase[] results = {
				new InlineQueryResultLocation(
					id: "1",
					latitude: 40.7058316f,
					longitude: -74.2581888f,
					title: "New York")   // displayed result
                    {
						InputMessageContent = new InputLocationMessageContent(
							latitude: 40.7058316f,
							longitude: -74.2581888f)    // message if result is selected
                    },

				new InlineQueryResultLocation(
					id: "2",
					latitude: 13.1449577f,
					longitude: 52.507629f,
					title: "Berlin") // displayed result
                    {
						InputMessageContent = new InputLocationMessageContent(
							latitude: 13.1449577f,
							longitude: 52.507629f)   // message if result is selected
                    }
			};

			await Bot.AnswerInlineQueryAsync(
				inlineQueryEventArgs.InlineQuery.Id,
				results,
				isPersonal: true,
				cacheTime: 0);
		}

		private static void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
		{
			Console.WriteLine($"Received inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
		}

		private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
		{
			Console.WriteLine("Received error: {0} — {1}",
				receiveErrorEventArgs.ApiRequestException.ErrorCode,
				receiveErrorEventArgs.ApiRequestException.Message);
		}
}
}

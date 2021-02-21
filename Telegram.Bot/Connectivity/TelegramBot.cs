using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
#if NET472
using System.Runtime.InteropServices.WindowsRuntime;
#endif
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Storage;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using File = Telegram.Bot.Types.File;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// Configurations for the bot
	/// </summary>
	public interface IBotOptions
	{
		/// <summary>
		/// 
		/// </summary>
		string Username { get; }

		/// <summary>
		/// Optional if client not needed. Telegram API token
		/// </summary>
		string ApiToken { get; }
		/// <summary>
		/// 
		/// </summary>
		string WebhookPath { get; }
		/// <summary>
		/// 
		/// </summary>
		TimeSpan Timeout { get; }
		/// <summary>
		/// 
		/// </summary>
		public IEnumerable<RegisteredUser> RegisteredUsersSource { get; }
	}

	/// <summary>
	/// 
	/// </summary>
	public class TelegramBot : BaseCentralDispatcher, IBot
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		protected override void SetupAPIConnection(InteractionContext context)
		{
			context.Connection = this;
		}
		/// <summary>
		/// 
		/// </summary>
		public int BotId { get; }

		private static readonly Update[] EmptyUpdates = { };

		private const string BaseUrl = "https://api.telegram.org/bot";

		private const string BaseFileUrl = "https://api.telegram.org/file/bot";

		private readonly string _baseRequestUrl;

		private readonly string _token;

		private readonly HttpClient _httpClient;
		private readonly IBotOptions _options;

		#region Config Properties

		/// <summary>
		/// Timeout for requests
		/// </summary>
		public TimeSpan Timeout
		{
			get => _httpClient.Timeout;
			set => _httpClient.Timeout = value;
		}

		/// <summary>
		/// Indicates if receiving updates
		/// </summary>
		public bool IsReceiving { get; set; }

		private CancellationTokenSource _receivingCancellationTokenSource;

		/// <summary>
		/// The current message offset
		/// </summary>
		public int MessageOffset { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public override IInteractionRouter InteractionRouter { get; set; }

		#endregion Config Properties
		/// <summary>
		/// 
		/// </summary>
		/// <param name="options"></param>
		private TelegramBot(IBotOptions options) : base(new StorageInteractionsRouter(), options.RegisteredUsersSource)
		{
			_options = options;
		}

		/// <summary>
		/// Create a new <see cref="TelegramBot"/> instance.
		/// </summary>
		/// <param name="options">Bot options</param>
		/// <param name="httpClient">A custom <see cref="HttpClient"/></param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="options"/> format is invalid</exception>
		public TelegramBot(IBotOptions options, HttpClient httpClient = null) : this(options)
		{
			_token = options.ApiToken ?? throw new ArgumentNullException(nameof(options.ApiToken));
			string[] parts = _token.Split(':');
			if (parts.Length > 1 && int.TryParse(parts[0], out int id))
			{
				BotId = id;
			}
			else
			{
				throw new ArgumentException(
					"Invalid format. A valid token looks like \"1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy\".",
					nameof(options.ApiToken)
				);
			}

			_baseRequestUrl = $"{BaseUrl}{_token}/";
			_httpClient = httpClient ?? new HttpClient();
			Timeout = options.Timeout;
		}

		/// <summary>
		/// Create a new <see cref="TelegramBot"/> instance behind a proxy.
		/// </summary>
		/// <param name="options">API token</param>
		/// <param name="webProxy">Use this <see cref="IWebProxy"/> to connect to the API</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="options"/> format is invalid</exception>
		public TelegramBot(IBotOptions options, IWebProxy webProxy) : this(options)
		{
			_token = options.ApiToken ?? throw new ArgumentNullException(nameof(options.ApiToken));
			string[] parts = _token.Split(':');
			if (int.TryParse(parts[0], out int id))
			{
				BotId = id;
			}
			else
			{
				throw new ArgumentException(
					"Invalid format. A valid token looks like \"1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy\".",
					nameof(options.ApiToken)
				);
			}

			_baseRequestUrl = $"{BaseUrl}{_token}/";
			var httpClientHander = new HttpClientHandler
			{
				Proxy = webProxy,
				UseProxy = true
			};
			_httpClient = new HttpClient(httpClientHander);
			Timeout = options.Timeout;
		}

		#region Helpers

		/// <inheritdoc />
		public async Task<TResponse> MakeRequestAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
		{
			string url = _baseRequestUrl + request.MethodName;

			var httpRequest = new HttpRequestMessage(request.Method, url)
			{
				Content = request.ToHttpContent()
			};

			var reqDataArgs = new ApiRequestEventArgs
			{
				MethodName = request.MethodName,
				HttpContent = httpRequest.Content,
			};
			// MakingApiRequest?.Invoke(this, reqDataArgs);

			HttpResponseMessage httpResponse;
			try
			{
				httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
			}
			catch (TaskCanceledException e)
			{
				if (cancellationToken.IsCancellationRequested)
					throw;

				throw new ApiRequestException("Request timed out", 408, e);
			}

			// required since user might be able to set new status code using following event arg
			var actualResponseStatusCode = httpResponse.StatusCode;
			string responseJson = await httpResponse.Content.ReadAsStringAsync()
				.ConfigureAwait(false);

			/*  ApiResponseReceived?.Invoke(this, new ApiResponseEventArgs
			  {
				  ResponseMessage = httpResponse,
				  ApiRequestEventArgs = reqDataArgs
			  });*/

			switch (actualResponseStatusCode)
			{
				case HttpStatusCode.OK:
					break;
				case HttpStatusCode.Unauthorized:
				case HttpStatusCode.BadRequest when !string.IsNullOrWhiteSpace(responseJson):
				case HttpStatusCode.Forbidden when !string.IsNullOrWhiteSpace(responseJson):
				case HttpStatusCode.Conflict when !string.IsNullOrWhiteSpace(responseJson):
					// Do NOT throw here, an ApiRequestException will be thrown next
					break;
				default:
					httpResponse.EnsureSuccessStatusCode();
					break;
			}

			var apiResponse =
				JsonConvert.DeserializeObject<ApiResponse<TResponse>>(responseJson)
				?? new ApiResponse<TResponse> // ToDo is required? unit test
				{
					Ok = false,
					Description = "No response received"
				};

			if (!apiResponse.Ok)
				throw ApiExceptionParser.Parse(apiResponse);

			return apiResponse.Result;
		}

		/// <summary>
		/// Test the API token
		/// </summary>
		/// <returns><c>true</c> if token is valid</returns>
		public async Task<bool> TestApiAsync(CancellationToken cancellationToken = default)
		{
			try
			{
				await GetMeAsync(cancellationToken).ConfigureAwait(false);
				return true;
			}
			catch (ApiRequestException e)
				when (e.ErrorCode == 401)
			{
				return false;
			}
		}

		/// <summary>
		/// Start update receiving
		/// </summary>
		/// <param name="allowedUpdates">List the types of updates you want your bot to receive.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="ApiRequestException"> Thrown if token is invalid</exception>
		public void StartReceiving(UpdateType[] allowedUpdates = null,
								   CancellationToken cancellationToken = default)
		{
			_receivingCancellationTokenSource = new CancellationTokenSource();

			cancellationToken.Register(() => _receivingCancellationTokenSource.Cancel());

			ReceiveAsync(allowedUpdates, _receivingCancellationTokenSource.Token);
		}

#pragma warning disable AsyncFixer03 // Avoid fire & forget async void methods
		private async void ReceiveAsync(
			UpdateType[] allowedUpdates,
			CancellationToken cancellationToken = default)
		{
			IsReceiving = true;
			while (!cancellationToken.IsCancellationRequested)
			{
				var timeout = Convert.ToInt32(Timeout.TotalSeconds);
				var updates = EmptyUpdates;

				try
				{
					updates = await GetUpdatesAsync(MessageOffset, timeout: timeout, allowedUpdates: allowedUpdates, cancellationToken: cancellationToken).ConfigureAwait(false);
				}
				catch (OperationCanceledException)
				{
				}
				catch (ApiRequestException)
				{
					//OnReceiveError?.Invoke(this, apiException);
				}
				catch (Exception)
				{
					//OnReceiveGeneralError?.Invoke(this, generalException);
				}

				try
				{
					foreach (var update in updates)
					{
						//OnUpdateReceived(new UpdateEventArgs(update));
						MessageOffset = update.Id + 1;
					}
				}
				catch
				{
					IsReceiving = false;
					throw;
				}
			}

			IsReceiving = false;
		}
#pragma warning restore AsyncFixer03 // Avoid fire & forget async void methods

		/// <summary>
		/// Stop update receiving
		/// </summary>
		public virtual void StopReceiving()
		{
			try
			{
				_receivingCancellationTokenSource.Cancel();
			}
			catch (WebException)
			{
			}
			catch (TaskCanceledException)
			{
			}
		}

		#endregion Helpers

		#region Getting updates

		/// <inheritdoc />
		public Task<Update[]> GetUpdatesAsync(int offset = default, int limit = default, int timeout = default, IEnumerable<UpdateType> allowedUpdates = default, CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new GetUpdatesRequest
			{
				Offset = offset,
				Limit = limit,
				Timeout = timeout,
				AllowedUpdates = allowedUpdates
			}, cancellationToken);

		#endregion Getting updates

		#region Available methods

		/// <inheritdoc />
		public Task<User> GetMeAsync(CancellationToken cancellationToken = default) => MakeRequestAsync(new GetMeRequest(), cancellationToken);

		/// <inheritdoc />
		public Task<Message> SendTextMessageAsync(
			ChatId chatId,
			string text,
			ParseMode parseMode = default,
			bool disableWebPagePreview = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new SendMessageRequest(chatId, text)
			{
				ParseMode = parseMode,
				DisableWebPagePreview = disableWebPagePreview,
				DisableNotification = disableNotification,
				ReplyToMessageId = replyToMessageId,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> ForwardMessageAsync(
			ChatId chatId,
			ChatId fromChatId,
			int messageId,
			bool disableNotification = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new ForwardMessageRequest(chatId, fromChatId, messageId)
			{
				DisableNotification = disableNotification
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> SendPhotoAsync(
			ChatId chatId,
			InputOnlineFile photo,
			string caption = default,
			ParseMode parseMode = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new SendPhotoRequest(chatId, photo)
			{
				Caption = caption,
				ParseMode = parseMode,
				ReplyToMessageId = replyToMessageId,
				DisableNotification = disableNotification,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> SendAudioAsync(
			ChatId chatId,
			InputOnlineFile audio,
			string caption = default,
			ParseMode parseMode = default,
			int duration = default,
			string performer = default,
			string title = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default,
			InputMedia thumb = default
		) =>
			MakeRequestAsync(new SendAudioRequest(chatId, audio)
			{
				Caption = caption,
				ParseMode = parseMode,
				Duration = duration,
				Performer = performer,
				Title = title,
				Thumb = thumb,
				DisableNotification = disableNotification,
				ReplyToMessageId = replyToMessageId,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> SendDocumentAsync(
			ChatId chatId,
			InputOnlineFile document,
			string caption = default,
			ParseMode parseMode = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default,
			InputMedia thumb = default
		) =>
			MakeRequestAsync(new SendDocumentRequest(chatId, document)
			{
				Caption = caption,
				Thumb = thumb,
				ParseMode = parseMode,
				DisableNotification = disableNotification,
				ReplyToMessageId = replyToMessageId,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> SendStickerAsync(
			ChatId chatId,
			InputOnlineFile sticker,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new SendStickerRequest(chatId, sticker)
			{
				DisableNotification = disableNotification,
				ReplyToMessageId = replyToMessageId,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> SendVideoAsync(
			ChatId chatId,
			InputOnlineFile video,
			int duration = default,
			int width = default,
			int height = default,
			string caption = default,
			ParseMode parseMode = default,
			bool supportsStreaming = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default,
			InputMedia thumb = default
		) =>
			MakeRequestAsync(new SendVideoRequest(chatId, video)
			{
				Duration = duration,
				Width = width,
				Height = height,
				Thumb = thumb,
				Caption = caption,
				ParseMode = parseMode,
				SupportsStreaming = supportsStreaming,
				DisableNotification = disableNotification,
				ReplyToMessageId = replyToMessageId,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> SendAnimationAsync(
			ChatId chatId,
			InputOnlineFile animation,
			int duration = default,
			int width = default,
			int height = default,
			InputMedia thumb = default,
			string caption = default,
			ParseMode parseMode = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new SendAnimationRequest(chatId, animation)
			{
				Duration = duration,
				Width = width,
				Height = height,
				Thumb = thumb,
				Caption = caption,
				ParseMode = parseMode,
				DisableNotification = disableNotification,
				ReplyToMessageId = replyToMessageId,
				ReplyMarkup = replyMarkup,
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> SendVoiceAsync(
			ChatId chatId,
			InputOnlineFile voice,
			string caption = default,
			ParseMode parseMode = default,
			int duration = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new SendVoiceRequest(chatId, voice)
			{
				Caption = caption,
				ParseMode = parseMode,
				Duration = duration,
				DisableNotification = disableNotification,
				ReplyToMessageId = replyToMessageId,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> SendVideoNoteAsync(
			ChatId chatId,
			InputTelegramFile videoNote,
			int duration = default,
			int length = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default,
			InputMedia thumb = default
		) =>
			MakeRequestAsync(new SendVideoNoteRequest(chatId, videoNote)
			{
				Duration = duration,
				Length = length,
				Thumb = thumb,
				DisableNotification = disableNotification,
				ReplyToMessageId = replyToMessageId,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message[]> SendMediaGroupAsync(
			IEnumerable<IAlbumInputMedia> inputMedia,
			ChatId chatId,
			bool disableNotification = default,
			int replyToMessageId = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new SendMediaGroupRequest(chatId, inputMedia)
			{
				DisableNotification = disableNotification,
				ReplyToMessageId = replyToMessageId,
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> SendLocationAsync(
			ChatId chatId,
			float latitude,
			float longitude,
			int livePeriod = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new SendLocationRequest(chatId, latitude, longitude)
			{
				LivePeriod = livePeriod,
				DisableNotification = disableNotification,
				ReplyToMessageId = replyToMessageId,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> SendVenueAsync(
			ChatId chatId,
			float latitude,
			float longitude,
			string title,
			string address,
			string foursquareId = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default,
			string foursquareType = default
		) =>
			MakeRequestAsync(new SendVenueRequest(chatId, latitude, longitude, title, address)
			{
				FoursquareId = foursquareId,
				FoursquareType = foursquareType,
				DisableNotification = disableNotification,
				ReplyToMessageId = replyToMessageId,
				ReplyMarkup = replyMarkup
			}, cancellationToken);



		/// <inheritdoc />
		public Task<Message> SendDiceAsync(
			ChatId chatId,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default,
			Emoji? emoji = default) =>
			MakeRequestAsync(
				new SendDiceRequest(chatId)
				{
					DisableNotification = disableNotification,
					ReplyToMessageId = replyToMessageId,
					ReplyMarkup = replyMarkup,
					Emoji = emoji
				},
				cancellationToken
			);

		/// <inheritdoc />
		public Task SendChatActionAsync(
			ChatId chatId,
			ChatAction chatAction,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new SendChatActionRequest(chatId, chatAction), cancellationToken);

		/// <inheritdoc />
		public Task<UserProfilePhotos> GetUserProfilePhotosAsync(
			int userId,
			int offset = default,
			int limit = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new GetUserProfilePhotosRequest(userId)
			{
				Offset = offset,
				Limit = limit
			}, cancellationToken);

		/// <inheritdoc />
		public Task<File> GetFileAsync(
			string fileId,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new GetFileRequest(fileId), cancellationToken);

		/// <inheritdoc />
		public async Task DownloadFileAsync(
			string filePath,
			Stream destination,
			CancellationToken cancellationToken = default
		)
		{
			if (string.IsNullOrWhiteSpace(filePath) || filePath.Length < 2)
			{
				throw new ArgumentException("Invalid file path", nameof(filePath));
			}

			if (destination == null)
			{
				throw new ArgumentNullException(nameof(destination));
			}

			var fileUri = new Uri($"{BaseFileUrl}{_token}/{filePath}");

			var response = await _httpClient
				.GetAsync(fileUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
				.ConfigureAwait(false);
			response.EnsureSuccessStatusCode();

			using (response)
			{
				await response.Content.CopyToAsync(destination)
					.ConfigureAwait(false);
			}
		}

		/// <inheritdoc />
		public async Task<File> GetInfoAndDownloadFileAsync(
			string fileId,
			Stream destination,
			CancellationToken cancellationToken = default
		)
		{
			var file = await GetFileAsync(fileId, cancellationToken)
				.ConfigureAwait(false);

			await DownloadFileAsync(file.FilePath, destination, cancellationToken)
				.ConfigureAwait(false);

			return file;
		}
		/// <inheritdoc />
		public Task<Chat> GetChatAsync(
			ChatId chatId,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new GetChatRequest(chatId), cancellationToken);


		/// <inheritdoc />
		public Task AnswerCallbackQueryAsync(
			string callbackQueryId,
			string text = default,
			bool showAlert = default,
			string url = default,
			int cacheTime = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new AnswerCallbackQueryRequest(callbackQueryId)
			{
				Text = text,
				ShowAlert = showAlert,
				Url = url,
				CacheTime = cacheTime
			}, cancellationToken);


		/// <inheritdoc />
		public Task SetChatAdministratorCustomTitleAsync(
			ChatId chatId,
			int userId,
			string customTitle,
			CancellationToken cancellationToken = default)
			=> MakeRequestAsync(
				new SetChatAdministratorCustomTitleRequest(chatId, userId, customTitle),
				cancellationToken);

		/// <inheritdoc />
		public Task<BotCommand[]> GetMyCommandsAsync(CancellationToken cancellationToken = default) =>
			MakeRequestAsync(new GetMyCommandsRequest(), cancellationToken);

		/// <inheritdoc />
		public Task SetMyCommandsAsync(
			IEnumerable<BotCommand> commands,
			CancellationToken cancellationToken = default) =>
			MakeRequestAsync(new SetMyCommandsRequest(commands), cancellationToken);

		/// <inheritdoc />
		public Task<Message> StopMessageLiveLocationAsync(
			ChatId chatId,
			int messageId,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new StopMessageLiveLocationRequest(chatId, messageId)
			{
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task StopMessageLiveLocationAsync(
			string inlineMessageId,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new StopInlineMessageLiveLocationRequest(inlineMessageId)
			{
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		#endregion Available methods

		#region Updating messages

		/// <inheritdoc />
		public Task<Message> EditMessageTextAsync(
			ChatId chatId,
			int messageId,
			string text,
			ParseMode parseMode = default,
			bool disableWebPagePreview = default,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new EditMessageTextRequest(chatId, messageId, text)
			{
				ParseMode = parseMode,
				DisableWebPagePreview = disableWebPagePreview,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task EditMessageTextAsync(
			string inlineMessageId,
			string text,
			ParseMode parseMode = default,
			bool disableWebPagePreview = default,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new EditInlineMessageTextRequest(inlineMessageId, text)
			{
				DisableWebPagePreview = disableWebPagePreview,
				ReplyMarkup = replyMarkup,
				ParseMode = parseMode
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> EditMessageCaptionAsync(
			ChatId chatId,
			int messageId,
			string caption,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default,
			ParseMode parseMode = default
		) =>
			MakeRequestAsync(new EditMessageCaptionRequest(chatId, messageId, caption)
			{
				ParseMode = parseMode,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task EditMessageCaptionAsync(
			string inlineMessageId,
			string caption,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default,
			ParseMode parseMode = default
		) =>
			MakeRequestAsync(new EditInlineMessageCaptionRequest(inlineMessageId, caption)
			{
				ParseMode = parseMode,
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> EditMessageMediaAsync(
			ChatId chatId,
			int messageId,
			InputMediaBase media,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new EditMessageMediaRequest(chatId, messageId, media)
			{
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task EditMessageMediaAsync(
			string inlineMessageId,
			InputMediaBase media,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new EditInlineMessageMediaRequest(inlineMessageId, media)
			{
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task<Message> EditMessageReplyMarkupAsync(
			ChatId chatId,
			int messageId,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(
				new EditMessageReplyMarkupRequest(chatId, messageId, replyMarkup),
				cancellationToken);

		/// <inheritdoc />
		public Task EditMessageReplyMarkupAsync(
			string inlineMessageId,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(
				new EditInlineMessageReplyMarkupRequest(inlineMessageId, replyMarkup),
				cancellationToken);

		/// <inheritdoc />
		public Task<Message> EditMessageLiveLocationAsync(
			ChatId chatId,
			int messageId,
			float latitude,
			float longitude,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new EditMessageLiveLocationRequest(chatId, messageId, latitude, longitude)
			{
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task EditMessageLiveLocationAsync(
			string inlineMessageId,
			float latitude,
			float longitude,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new EditInlineMessageLiveLocationRequest(inlineMessageId, latitude, longitude)
			{
				ReplyMarkup = replyMarkup
			}, cancellationToken);

		/// <inheritdoc />
		public Task DeleteMessageAsync(
			ChatId chatId,
			int messageId,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new DeleteMessageRequest(chatId, messageId), cancellationToken);

		#endregion Updating messages

		#region Inline mode

		/// <inheritdoc />
		public Task AnswerInlineQueryAsync(
			string inlineQueryId,
			IEnumerable<InlineQueryResultBase> results,
			int? cacheTime = default,
			bool isPersonal = default,
			string nextOffset = default,
			string switchPmText = default,
			string switchPmParameter = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new AnswerInlineQueryRequest(inlineQueryId, results)
			{
				CacheTime = cacheTime,
				IsPersonal = isPersonal,
				NextOffset = nextOffset,
				SwitchPmText = switchPmText,
				SwitchPmParameter = switchPmParameter
			}, cancellationToken);

		#endregion Inline mode

		#region Group and channel management

		/// <inheritdoc />
		public Task SetChatTitleAsync(
			ChatId chatId,
			string title,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new SetChatTitleRequest(chatId, title), cancellationToken);

		/// <inheritdoc />
		public Task SetChatDescriptionAsync(
			ChatId chatId,
			string description = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new SetChatDescriptionRequest(chatId, description), cancellationToken);

		/// <inheritdoc />
		public Task PinChatMessageAsync(
			ChatId chatId,
			int messageId,
			bool disableNotification = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new PinChatMessageRequest(chatId, messageId)
			{
				DisableNotification = disableNotification
			}, cancellationToken);

		/// <inheritdoc />
		public Task UnpinChatMessageAsync(
			ChatId chatId,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new UnpinChatMessageRequest(chatId), cancellationToken);

		/// <inheritdoc />
		public Task SetChatStickerSetAsync(
			ChatId chatId,
			string stickerSetName,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new SetChatStickerSetRequest(chatId, stickerSetName), cancellationToken);

		/// <inheritdoc />
		public Task DeleteChatStickerSetAsync(
			ChatId chatId,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new DeleteChatStickerSetRequest(chatId), cancellationToken);

		#endregion

		#region Stickers

		/// <inheritdoc />
		public Task<StickerSet> GetStickerSetAsync(
			string name,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new GetStickerSetRequest(name), cancellationToken);

		/// <inheritdoc />
		public Task<File> UploadStickerFileAsync(
			int userId,
			InputFileStream pngSticker,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new UploadStickerFileRequest(userId, pngSticker), cancellationToken);

		/// <inheritdoc />
		public Task CreateNewStickerSetAsync(
			int userId,
			string name,
			string title,
			InputOnlineFile pngSticker,
			string emojis,
			bool isMasks = default,
			MaskPosition maskPosition = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new CreateNewStickerSetRequest(userId, name, title, pngSticker, emojis)
			{
				ContainsMasks = isMasks,
				MaskPosition = maskPosition
			}, cancellationToken);

		/// <inheritdoc />
		public Task AddStickerToSetAsync(
			int userId,
			string name,
			InputOnlineFile pngSticker,
			string emojis,
			MaskPosition maskPosition = default,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new AddStickerToSetRequest(userId, name, pngSticker, emojis)
			{
				MaskPosition = maskPosition
			}, cancellationToken);

		/// <inheritdoc />
		public Task CreateNewAnimatedStickerSetAsync(
			int userId,
			string name,
			string title,
			InputFileStream tgsSticker,
			string emojis,
			bool isMasks = default,
			MaskPosition maskPosition = default,
			CancellationToken cancellationToken = default) =>
			MakeRequestAsync(
				new CreateNewAnimatedStickerSetRequest(userId, name, title, tgsSticker, emojis)
				{
					ContainsMasks = isMasks,
					MaskPosition = maskPosition
				},
				cancellationToken
			);

		/// <inheritdoc />
		public Task AddAnimatedStickerToSetAsync(
			int userId,
			string name,
			InputFileStream tgsSticker,
			string emojis,
			MaskPosition maskPosition = default,
			CancellationToken cancellationToken = default) =>
			MakeRequestAsync(
				new AddAnimatedStickerToSetRequest(userId, name, tgsSticker, emojis)
				{
					MaskPosition = maskPosition
				},
				cancellationToken
			);

		/// <inheritdoc />
		public Task SetStickerPositionInSetAsync(
			string sticker,
			int position,
			CancellationToken cancellationToken = default) =>
			MakeRequestAsync(
				new SetStickerPositionInSetRequest(sticker, position),
				cancellationToken
			);

		/// <inheritdoc />
		public Task DeleteStickerFromSetAsync(
			string sticker,
			CancellationToken cancellationToken = default
		) =>
			MakeRequestAsync(new DeleteStickerFromSetRequest(sticker), cancellationToken);

		/// <inheritdoc />
		public Task SetStickerSetThumbAsync(
			string name,
			int userId,
			InputOnlineFile thumb = default,
			CancellationToken cancellationToken = default) =>
			MakeRequestAsync(
				new SetStickerSetThumbRequest(name, userId, thumb),
				cancellationToken
			);

		#endregion

		/// <summary>
		/// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking for each.    
		/// </summary>
		/// <typeparam name="TUpdateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</typeparam>    
		/// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
		public void StartReceiving<TUpdateHandler>(CancellationToken cancellationToken = default) where TUpdateHandler : IInteractionHandler, new()
		{
			StartReceiving(new TUpdateHandler(), cancellationToken);
		}
		/// <summary>
		/// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking for each.    
		/// </summary>		
		/// <param name="updateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
		public void StartReceiving(IInteractionHandler updateHandler, CancellationToken cancellationToken = default)
		{
			if (updateHandler == null)
				throw new ArgumentNullException(nameof(updateHandler));

			Task.Run(async () =>
			{
				try
				{
					await ReceiveAsync(updateHandler, cancellationToken);
				}
				catch (Exception ex)
				{
					await updateHandler.HandleErrorAsync(ex, cancellationToken);
				}
			}, cancellationToken);
		}

		/// <summary>
		/// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking for each.        
		/// </summary>
		/// <typeparam name="TUpdateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</typeparam>        
		/// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
		/// <returns></returns>
		public Task ReceiveAsync<TUpdateHandler>(CancellationToken cancellationToken = default) where TUpdateHandler : IInteractionHandler, new()
		{
			return ReceiveAsync(new TUpdateHandler(), cancellationToken);
		}

		/// <summary>
		/// Starts receiving <see cref="Update"/>s on the ThreadPool, invoking  for each.        
		/// </summary>        
		/// <param name="updateHandler">The <see cref="IUpdateHandler"/> used for processing <see cref="Update"/>s</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/> with which you can stop receiving</param>
		/// <returns></returns>
		public async Task ReceiveAsync(IInteractionHandler updateHandler, CancellationToken cancellationToken = default)
		{
			if (updateHandler == null)
				throw new ArgumentNullException(nameof(updateHandler));

			//UpdateType[]? allowedUpdates = updateHandler.AllowedUpdates;
			int messageOffset = 0;
			var emptyUpdates = new Update[] { };

			while (!cancellationToken.IsCancellationRequested)
			{
				int timeout = (int)this.Timeout.TotalSeconds;
				var updates = emptyUpdates;
				try
				{
					updates = await this.MakeRequestAsync(new GetUpdatesRequest()
					{
						Offset = messageOffset,
						Timeout = timeout,
						AllowedUpdates = null,
					}, cancellationToken).ConfigureAwait(false);
				}
				catch (OperationCanceledException)
				{
					// Ignore
				}
				catch (Exception ex)
				{
					await updateHandler.HandleErrorAsync(ex, cancellationToken).ConfigureAwait(false);
				}

				foreach (var update in updates)
				{
					await updateHandler.HandleAsync(cancellationToken).ConfigureAwait(false);

					messageOffset = update.Id + 1;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="chatId"></param>
		/// <param name="text"></param>
		/// <param name="replyMarkup"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task<Message> SendTextMessageAsync(ChatId chatId, string text, IReplyMarkup replyMarkup = null, CancellationToken cancellationToken = default)
		{
			return SendTextMessageAsync(chatId, text, default, default, default, default, replyMarkup, cancellationToken);
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using File = Telegram.Bot.Types.File;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public interface IBot
	{
		/// <summary>
		/// Unique identifier for the bot from bot token. For example, for the bot token
		/// "1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy", the bot id is "1234567".
		/// </summary>
		int BotId { get; }

		#region Config Properties

		/// <summary>
		/// Timeout for requests
		/// </summary>
		TimeSpan Timeout { get; set; }

		/// <summary>
		/// Indicates if receiving updates
		/// </summary>
		bool IsReceiving { get; }

		/// <summary>
		/// The current message offset
		/// </summary>
		int MessageOffset { get; set; }

		#endregion  Config Properties

		#region Helpers

		/// <summary>
		/// Send a request to Bot API
		/// </summary>
		/// <typeparam name="TResponse">Type of expected result in the response object</typeparam>
		/// <param name="request">API request object</param>
		/// <param name="cancellationToken"></param>
		/// <returns>Result of the API request</returns>
		Task<TResponse> MakeRequestAsync<TResponse>(
			IRequest<TResponse> request,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Test the API token
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns><c>true</c> if token is valid</returns>
		Task<bool> TestApiAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Start update receiving
		/// </summary>
		/// <param name="allowedUpdates">List the types of updates you want your bot to receive.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <exception cref="Exceptions.ApiRequestException"> Thrown if token is invalid</exception>
		void StartReceiving(UpdateType[] allowedUpdates = null,
							CancellationToken cancellationToken = default);

		/// <summary>
		/// Stop update receiving
		/// </summary>
		void StopReceiving();

		#endregion Helpers

		#region Getting updates

		/// <summary>
		/// Use this method to receive incoming updates using long polling (wiki).
		/// </summary>
		/// <param name="offset">
		/// Identifier of the first <see cref="Update"/> to be returned.
		/// Must be greater by one than the highest among the identifiers of previously received updates.
		/// By default, updates starting with the earliest unconfirmed update are returned. An update is considered
		/// confirmed as soon as <see cref="GetUpdatesAsync"/> is called with an offset higher than its <see cref="Update.Id"/>.
		/// The negative offset can be specified to retrieve updates starting from -offset update from the end of the updates queue. All previous updates will forgotten.
		/// </param>
		/// <param name="limit">
		/// Limits the number of updates to be retrieved. Values between 1-100 are accepted.
		/// </param>
		/// <param name="timeout">
		/// Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling. Should be positive, short polling should be used for testing purposes only.
		/// </param>
		/// <param name="allowedUpdates">
		/// List the <see cref="UpdateType"/> of updates you want your bot to receive. See <see cref="UpdateType"/> for a complete list of available update types. Specify an empty list to receive all updates regardless of type (default).
		/// If not specified, the previous setting will be used.
		///
		/// Please note that this parameter doesn't affect updates created before the call to the <see cref="GetUpdatesAsync"/>, so unwanted updates may be received for a short period of time.
		/// </param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <remarks>
		/// 1. This method will not work if an outgoing webhook is set up.
		/// 2. In order to avoid getting duplicate updates, recalculate offset after each server response.
		///
		/// Telegram Docs <see href="https://core.telegram.org/bots/api#getupdates"/>
		/// </remarks>
		/// <returns>An Array of <see cref="Update"/> is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#getupdates"/>
		Task<Update[]> GetUpdatesAsync(
			int offset = default,
			int limit = default,
			int timeout = default,
			IEnumerable<UpdateType> allowedUpdates = default,
			CancellationToken cancellationToken = default);


#pragma warning disable CS1587 // XML comment is not placed on a valid language element
		/// <summary>
		/// Use this method to specify a url and receive incoming updates via an outgoing webhook.
		/// Whenever there is an <see cref="Update"/> for the bot, we will send an HTTPS POST request to the specified url,
		/// containing a JSON-serialized Update. In case of an unsuccessful request, we will give up after a reasonable
		/// amount of attempts.
		///
		/// If you'd like to make sure that the Webhook request comes from Telegram, we recommend using a secret path
		/// in the URL, e.g. https://www.example.com/&lt;token&gt;. Since nobody else knows your bot's token, you can be
		/// pretty sure it's us.
		/// </summary>
		/// <param name="url">HTTPS url to send updates to. Use an empty string to remove webhook integration</param>
		/// <param name="certificate">
		/// Upload your public key certificate so that the root certificate in use can be checked.
		/// See the <see href="https://core.telegram.org/bots/self-signed">self-signed guide</see> for details.
		/// </param>
		/// <param name="maxConnections">Maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery, 1-100. Defaults to 40. Use lower values to limit the load on your bot's server, and higher values to increase your bot's throughput.</param>
		/// <param name="allowedUpdates">
		/// List the <see cref="UpdateType"/> of updates you want your bot to receive. See <see cref="UpdateType"/> for a complete list of available update types. Specify an empty list to receive all updates regardless of type (default).
		/// If not specified, the previous setting will be used.
		///
		/// Please note that this parameter doesn't affect updates created before the call to the <see cref="GetUpdatesAsync"/>, so unwanted updates may be received for a short period of time.
		/// </param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns><c>true</c></returns>
		/// <remarks>
		/// 1. You will not be able to receive updates using getUpdates for as long as an outgoing webhook is set up.
		/// 2. We currently do not support self-signed certificates.
		/// 3. For the moment, the only supported port for Webhooks is 443. We may support additional ports later.
		///
		/// If you're having any trouble setting up webhooks, please check out this <see href="https://core.telegram.org/bots/webhooks">amazing guide to Webhooks</see>.
		/// </remarks>
		/// <see href="https://core.telegram.org/bots/api#setwebhook"/>

		#endregion Getting updates

		#region Available methods

		/// <summary>
		/// A simple method for testing your bot's auth token.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns basic information about the bot in form of <see cref="User"/> object</returns>
		/// <see href="https://core.telegram.org/bots/api#getme"/>
		Task<User> GetMeAsync(CancellationToken cancellationToken = default);
#pragma warning restore CS1587 // XML comment is not placed on a valid language element

		/// <summary>
		/// Use this method to send text messages. On success, the sent Description is returned.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="text">Text of the message to be sent</param>
		/// <param name="parseMode">Change, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.</param>
		/// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the sent Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendmessage"/>
		Task<Message> SendTextMessageAsync(
			ChatId chatId,
			string text,
			ParseMode parseMode = default,
			bool disableWebPagePreview = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to send text messages. On success, the sent Description is returned.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="text">Text of the message to be sent</param>        
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the sent Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendmessage"/>
		Task<Message> SendTextMessageAsync(
			ChatId chatId,
			string text,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to forward messages of any kind. On success, the sent Description is returned.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="fromChatId"><see cref="ChatId"/> for the chat where the original message was sent</param>
		/// <param name="messageId">Unique message identifier</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the sent Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#forwardmessage"/>
		Task<Message> ForwardMessageAsync(
			ChatId chatId,
			ChatId fromChatId,
			int messageId,
			bool disableNotification = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to send photos. On success, the sent Description is returned.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="photo">Photo to send.</param>
		/// <param name="caption">Photo caption (may also be used when resending photos by file_id).</param>
		/// <param name="parseMode">Change, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the sent Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendphoto"/>
		Task<Message> SendPhotoAsync(
			ChatId chatId,
			InputOnlineFile photo,
			string caption = default,
			ParseMode parseMode = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to send audio files, if you want Telegram clients to display them in the music player. Your
		/// audio must be in the .mp3 format. On success, the sent Description is returned. Bots can currently send
		/// audio files of up to 50 MB in size, this limit may be changed in the future.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="audio">Audio file to send.</param>
		/// <param name="caption">Audio caption, 0-1024 characters</param>
		/// <param name="parseMode">Change, if you want Telegram apps to show bold, italic, fixed-width text or inline
		/// URLs in your bot's message.</param>
		/// <param name="duration">Duration of the audio in seconds</param>
		/// <param name="performer">Performer</param>
		/// <param name="title">Track name</param>
		/// <param name="thumb">Thumbnail of the file sent. The thumbnail should be in JPEG format and less than 200 kB
		/// in size. A thumbnail's width and height should not exceed 90. Thumbnails can't be reused and can be only
		/// uploaded as a new file.</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification,
		/// Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard,
		/// instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.</param>
		/// <returns>On success, the sent Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendaudio"/>
		Task<Message> SendAudioAsync(
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
			InputMedia thumb = default,
			CancellationToken cancellationToken = default); // ToDo inconsistent order of parameters

		/// <summary>
		/// Use this method to send general files. On success, the sent Description is returned. Bots can send files of
		/// any type of up to 50 MB in size.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="document">File to send.</param>
		/// <param name="thumb">Thumbnail of the file sent. The thumbnail should be in JPEG format and less than 200 kB
		/// in size. A thumbnail's width and height should not exceed 90. Thumbnails can't be reused and can be only
		/// uploaded as a new file.</param>
		/// <param name="caption">Document caption</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification,
		/// Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard,
		/// instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.</param>
		/// <param name="parseMode">Change, if you want Telegram apps to show bold, italic, fixed-width text or inline
		/// URLs in your bot's message.</param>
		/// <returns>On success, the sent Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#senddocument"/>
		Task<Message> SendDocumentAsync(
			ChatId chatId,
			InputOnlineFile document,
			string caption = default,
			ParseMode parseMode = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			InputMedia thumb = default,
			CancellationToken cancellationToken = default); // ToDo inconsistent order of parameters

		/// <summary>
		/// Use this method to send .webp stickers. On success, the sent Description is returned.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="sticker">Sticker to send.</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the sent Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendsticker"/>
		Task<Message> SendStickerAsync(
			ChatId chatId,
			InputOnlineFile sticker,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to send video files, Telegram clients support mp4 videos (other formats may be sent as
		/// Document). On success, the sent Description is returned. Bots can send video files of up to 50 MB in size.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="video">Video to send.</param>
		/// <param name="duration">Duration of sent video in seconds</param>
		/// <param name="width">Video width</param>
		/// <param name="height">Video height</param>
		/// <param name="thumb">Thumbnail of the file sent. The thumbnail should be in JPEG format and less than 200 kB
		/// in size. A thumbnail's width and height should not exceed 90. Thumbnails can't be reused and can be only
		/// uploaded as a new file.</param>
		/// <param name="caption">Video caption</param>
		/// <param name="parseMode">Change, if you want Telegram apps to show bold, italic, fixed-width text or inline
		/// URLs in your bot's message.</param>
		/// <param name="supportsStreaming">Pass True, if the uploaded video is suitable for streaming</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification,
		/// Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard,
		/// instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.</param>
		/// <returns>On success, the sent Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendvideo"/>
		Task<Message> SendVideoAsync(
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
			InputMedia thumb = default,
			CancellationToken cancellationToken = default); // ToDo inconsistent order of parameters

		/// <summary>
		/// Use this method to send animation files (GIF or H.264/MPEG-4 AVC video without sound). On success, the sent
		/// Message is returned. Bots can currently send animation files of up to 50 MB in size, this limit may be
		/// changed in the future.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="animation">Animation to send</param>
		/// <param name="duration">Duration of sent animation in seconds</param>
		/// <param name="width">Animation width</param>
		/// <param name="height">Animation height</param>
		/// <param name="thumb">Thumbnail of the file sent. The thumbnail should be in JPEG format and less than 200 kB
		/// in size. A thumbnail's width and height should not exceed 90. Thumbnails can't be reused and can be only
		/// uploaded as a new file.</param>
		/// <param name="caption">Animation caption</param>
		/// <param name="parseMode">Change, if you want Telegram apps to show bold, italic, fixed-width text or inline
		/// URLs in your bot's message.</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification,
		/// Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard,
		/// instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.</param>
		/// <returns>On success, the sent Message is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendanimation"/>
		Task<Message> SendAnimationAsync(
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
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .ogg file encoded with OPUS (other formats may be sent as Audio or Document). On success, the sent Description is returned. Bots can currently send voice messages of up to 50 MB in size, this limit may be changed in the future.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="voice">Audio file to send.</param>
		/// <param name="caption">Voice message caption, 0-1024 characters</param>
		/// <param name="parseMode">Change, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.</param>
		/// <param name="duration">Duration of sent audio in seconds</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the sent Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendvoice"/>
		Task<Message> SendVoiceAsync(
			ChatId chatId,
			InputOnlineFile voice,
			string caption = default,
			ParseMode parseMode = default,
			int duration = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// As of v.4.0, Telegram clients support rounded square mp4 videos of up to 1 minute long. Use this method to
		/// send video messages.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="videoNote">Video note to send.</param>
		/// <param name="thumb">Thumbnail of the file sent. The thumbnail should be in JPEG format and less than 200 kB
		/// in size. A thumbnail's width and height should not exceed 90. Thumbnails can't be reused and can be only
		/// uploaded as a new file.</param>
		/// <param name="duration">Duration of sent video in seconds</param>
		/// <param name="length">Video width and height</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification,
		/// Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard,
		/// instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.</param>
		/// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendvideonote"/>
		Task<Message> SendVideoNoteAsync(
			ChatId chatId,
			InputTelegramFile videoNote,
			int duration = default,
			int length = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			InputMedia thumb = default,
			CancellationToken cancellationToken = default); // ToDo inconsistent order of parameters


		/// <summary>
		/// Use this method to send a group of photos or videos as an album. On success, an array of the sent Messages is returned.
		/// </summary>
		/// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format @channelusername)</param>
		/// <param name="inputMedia">A JSON-serialized array describing photos and videos to be sent, must include 2–10 items</param>
		/// <param name="disableNotification">Sends the messages silently. Users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, an array of the sent <see cref="Message"/>s is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendmediagroup"/>
		Task<Message[]> SendMediaGroupAsync(
			IEnumerable<IAlbumInputMedia> inputMedia, // ToDo: Parameter is called "media" on API docs
			ChatId chatId, // ToDo: Should be the 1st parameter
			bool disableNotification = default,
			int replyToMessageId = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to send point on the map. On success, the sent Description is returned.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="latitude">Latitude of location</param>
		/// <param name="longitude">Longitude of location</param>
		/// <param name="livePeriod">Period in seconds for which the location will be updated. Should be between 60 and 86400.</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the sent Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendlocation"/>
		Task<Message> SendLocationAsync(
			ChatId chatId,
			float latitude,
			float longitude,
			int livePeriod = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to send information about a venue.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="latitude">Latitude of the venue</param>
		/// <param name="longitude">Longitude of the venue</param>
		/// <param name="title">Name of the venue</param>
		/// <param name="address">Address of the venue</param>
		/// <param name="foursquareId">Foursquare identifier of the venue</param>
		/// <param name="foursquareType">Foursquare type of the venue, if known. (For example,
		/// "arts_entertainment/default", "arts_entertainment/aquarium" or "food/icecream".) </param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification,
		/// Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply
		/// keyboard, instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.</param>
		/// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#sendvenue"/>
		Task<Message> SendVenueAsync(
			ChatId chatId,
			float latitude,
			float longitude,
			string title,
			string address,
			string foursquareId = default,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			string foursquareType = default,
			CancellationToken cancellationToken = default); // ToDo inconsistent order of parameters


		/// <summary>
		/// Use this request to send a dice, which will have a random value from 1 to 6. On success, the sent <see cref="Message"/> is returned
		/// </summary>
		/// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
		/// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
		/// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
		/// <param name="replyMarkup">Additional interface options. A JSON-serialized object for an inline keyboard, custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
		/// <param name="emoji">Emoji on which the dice throw animation is based</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#senddice"/>
		// TODO Fix parameter order in vnext
		Task<Message> SendDiceAsync(
			ChatId chatId,
			bool disableNotification = default,
			int replyToMessageId = default,
			IReplyMarkup replyMarkup = default,
			Emoji? emoji = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method when you need to tell the user that something is happening on the bot's side. The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="chatAction">Type of action to broadcast. Choose one, depending on what the user is about to receive.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <remarks>We only recommend using this method when a response from the bot will take a noticeable amount of time to arrive.</remarks>
		/// <see href="https://core.telegram.org/bots/api#sendchataction"/>
		Task SendChatActionAsync(
			ChatId chatId,
			ChatAction chatAction,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to get a list of profile pictures for a user. Returns a UserProfilePhotos object.
		/// </summary>
		/// <param name="userId">Unique identifier of the target user</param>
		/// <param name="offset">Sequential number of the first photo to be returned. By default, all photos are returned.</param>
		/// <param name="limit">Limits the number of photos to be retrieved. Values between 1-100 are accepted. Defaults to 100.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns a <see cref="UserProfilePhotos"/> object</returns>
		/// <see href="https://core.telegram.org/bots/api#getuserprofilephotos"/>
		Task<UserProfilePhotos> GetUserProfilePhotosAsync(
			int userId,
			int offset = default,
			int limit = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to get information about a file. For the moment, bots can download files of up to 20MB in size.
		/// </summary>
		/// <param name="fileId">File identifier</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>The File object</returns>
		/// <see href="https://core.telegram.org/bots/api#getfile"/>
		Task<File> GetFileAsync(
			string fileId,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to download a file. Get <paramref name="filePath"/> by calling <see cref="GetFileAsync"/>
		/// </summary>
		/// <param name="filePath">Path to file on server</param>
		/// <param name="destination">Destination stream to write file to</param>
		/// <param name="cancellationToken">The cancellation token to cancel operation</param>
		Task DownloadFileAsync(
			string filePath,
			Stream destination,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to get basic info about a file and download it.
		/// </summary>
		/// <param name="fileId">File identifier to get info about</param>
		/// <param name="destination">Destination stream to write file to</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>File info</returns>
		Task<File> GetInfoAndDownloadFileAsync(
			string fileId,
			Stream destination,
			CancellationToken cancellationToken = default);


		/// <summary>
		/// Use this method to get up to date information about the chat (current name of the user for one-on-one conversations, current username of a user, group or channel, etc.).
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns a Chat object on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#getchat"/>
		Task<Chat> GetChatAsync(
			ChatId chatId,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to send answers to callback queries sent from inline keyboards. The answer will be displayed to the user as a notification at the top of the chat screen or as an alert.
		/// </summary>
		/// <param name="callbackQueryId">Unique identifier for the query to be answered</param>
		/// <param name="text">Text of the notification. If not specified, nothing will be shown to the user</param>
		/// <param name="showAlert">If true, an alert will be shown by the client instead of a notification at the top of the chat screen. Defaults to false.</param>
		/// <param name="url">
		/// URL that will be opened by the user's client. If you have created a Game and accepted the conditions via @Botfather, specify the URL that opens your game — note that this will only work if the query comes from a callback_game button.
		/// Otherwise, you may use links like telegram.me/your_bot? start = XXXX that open your bot with a parameter.
		/// </param>
		/// <param name="cacheTime">The maximum amount of time in seconds that the result of the callback query may be cached client-side. Telegram apps will support caching starting in version 3.14.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, <c>true</c> is returned.</returns>
		/// <remarks>
		/// Alternatively, the user can be redirected to the specified Game URL. For this option to work, you must first create a game for your bot via BotFather and accept the terms.
		/// Otherwise, you may use links like telegram.me/your_bot?start=XXXX that open your bot with a parameter.
		/// </remarks>
		/// <see href="https://core.telegram.org/bots/api#answercallbackquery"/>
		Task AnswerCallbackQueryAsync(
			string callbackQueryId,
			string text = default,
			bool showAlert = default,
			string url = default,
			int cacheTime = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// <inheritdoc cref="Telegram.Bot.Requests.SetChatAdministratorCustomTitleRequest"/>
		/// </summary>
		/// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
		/// <param name="userId">Unique identifier of the target user</param>
		/// <param name="customTitle">New custom title for the administrator; 0-16 characters, emoji are not allowed</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns True on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#setchatadministratorcustomtitle"/>
		Task SetChatAdministratorCustomTitleAsync(
			ChatId chatId,
			int userId,
			string customTitle,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to get the current list of the bot's commands
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns>Array of <see cref="BotCommand"/> on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#getmycommands"/>
		Task<BotCommand[]> GetMyCommandsAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to change the list of the bot's commands. Returns True on success.
		/// </summary>
		/// <param name="commands">A list of bot commands to be set</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns><c>true</c> on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#setmycommands"/>
		Task SetMyCommandsAsync(
			IEnumerable<BotCommand> commands,
			CancellationToken cancellationToken = default);

		#endregion Available methods`

		#region Updating messages

		/// <summary>
		/// Use this method to edit text messages sent by the bot or via the bot (for inline bots).
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="messageId">Unique identifier of the sent message</param>
		/// <param name="text">New text of the message</param>
		/// <param name="parseMode">Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.</param>
		/// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the edited Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#editmessagetext"/>
		Task<Message> EditMessageTextAsync(
			ChatId chatId,
			int messageId,
			string text,
			ParseMode parseMode = default,
			bool disableWebPagePreview = default,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to edit text messages sent by the bot or via the bot (for inline bots).
		/// </summary>
		/// <param name="inlineMessageId">Identifier of the inline message</param>
		/// <param name="text">New text of the message</param>
		/// <param name="parseMode">Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.</param>
		/// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns><c>true</c> on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#editmessagetext"/>
		Task EditMessageTextAsync(
			string inlineMessageId,
			string text,
			ParseMode parseMode = default,
			bool disableWebPagePreview = default,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to stop updating a live location message sent by the bot before live_period expires.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="messageId">Unique identifier of the sent message</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success the sent <see cref="Message"/> is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#stopmessagelivelocation"/>
		Task<Message> StopMessageLiveLocationAsync(
			ChatId chatId,
			int messageId,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to stop updating a live location message sent via the bot (for inline bots) before live_period expires.
		/// </summary>
		/// <param name="inlineMessageId">Identifier of the inline message</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns><c>true</c> on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#stopmessagelivelocation"/>
		Task StopMessageLiveLocationAsync(
			string inlineMessageId,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to edit captions of messages sent by the bot or via the bot (for inline bots).
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="messageId">Unique identifier of the sent message</param>
		/// <param name="caption">New caption of the message</param>
		/// <param name="parseMode">Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in the media caption.</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the edited Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#editmessagecaption"/>
		Task<Message> EditMessageCaptionAsync(
			ChatId chatId,
			int messageId,
			string caption,
			InlineKeyboardMarkup replyMarkup = default,
			ParseMode parseMode = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to edit captions of messages sent by the bot or via the bot (for inline bots).
		/// </summary>
		/// <param name="inlineMessageId">Unique identifier of the sent message</param>
		/// <param name="caption">New caption of the message</param>
		/// <param name="parseMode">Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in the media caption.</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the edited Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#editmessagecaption"/>
		Task EditMessageCaptionAsync(
			string inlineMessageId,
			string caption,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default,
			ParseMode parseMode = default);

		/// <summary>
		/// Use this method to edit audio, document, photo, or video messages.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="messageId">Unique identifier of the sent message</param>
		/// <param name="media">A JSON-serialized object for a new media content of the message</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success the edited <see cref="Message"/> is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#editmessagemedia"/>
		Task<Message> EditMessageMediaAsync(
			ChatId chatId,
			int messageId,
			InputMediaBase media,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to edit audio, document, photo, or video inline messages.
		/// </summary>
		/// <param name="inlineMessageId">Unique identifier of the sent message</param>
		/// <param name="media">A JSON-serialized object for a new media content of the message</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns><c>true</c> on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#editmessagemedia"/>
		Task EditMessageMediaAsync(
			string inlineMessageId,
			InputMediaBase media,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to edit only the reply markup of messages sent by the bot or via the bot (for inline bots).
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="messageId">Unique identifier of the sent message</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, the edited Description is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#editmessagereplymarkup"/>
		Task<Message> EditMessageReplyMarkupAsync(
			ChatId chatId,
			int messageId,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to edit only the reply markup of messages sent by the bot or via the bot (for inline bots).
		/// </summary>
		/// <param name="inlineMessageId">Unique identifier of the sent message</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns><c>true</c> on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#editmessagereplymarkup"/>
		Task EditMessageReplyMarkupAsync(
			string inlineMessageId,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to edit live location messages sent by the bot.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
		/// <param name="messageId">Unique identifier of the sent message</param>
		/// <param name="latitude">Latitude of location</param>
		/// <param name="longitude">Longitude of location</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success the edited <see cref="Message"/> is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#editmessagelivelocation"/>
		Task<Message> EditMessageLiveLocationAsync(
			ChatId chatId,
			int messageId,
			float latitude,
			float longitude,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to edit live location messages sent via the bot (for inline bots).
		/// </summary>
		/// <param name="inlineMessageId">Unique identifier of the sent message</param>
		/// <param name="latitude">Latitude of location</param>
		/// <param name="longitude">Longitude of location</param>
		/// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns><c>true</c> on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#editmessagelivelocation"/>
		Task EditMessageLiveLocationAsync(
			string inlineMessageId,
			float latitude,
			float longitude,
			InlineKeyboardMarkup replyMarkup = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to delete a message. A message can only be deleted if it was sent less than 48 hours ago. Any such recently sent outgoing message may be deleted. Additionally, if the bot is an administrator in a group chat, it can delete any message. If the bot is an administrator in a supergroup, it can delete messages from any other user and service messages about people joining or leaving the group (other types of service messages may only be removed by the group creator). In channels, bots can only remove their own messages.
		/// </summary>
		/// <param name="chatId"><see cref="ChatId"/> Unique identifier for the target chat or username of the target channel (in the format @channelusername)</param>
		/// <param name="messageId">Unique identifier of the message to delete</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns><c>true</c> on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#deletemessage"/>
		Task DeleteMessageAsync(
			ChatId chatId,
			int messageId,
			CancellationToken cancellationToken = default);

		#endregion Updating messages

		#region Inline mode

		/// <summary>
		/// Use this method to send answers to an inline query.
		/// </summary>
		/// <param name="inlineQueryId">Unique identifier for answered query</param>
		/// <param name="results">A array of results for the inline query</param>
		/// <param name="cacheTime">The maximum amount of time in seconds the result of the inline query may be cached on the server</param>
		/// <param name="isPersonal">Pass <c>true</c>, if results may be cached on the server side only for the user that sent the query. By default, results may be returned to any user who sends the same query</param>
		/// <param name="nextOffset">Pass the offset that a client should send in the next query with the same text to receive more results. Pass an empty string if there are no more results or if you don't support pagination. Offset length can't exceed 64 bytes.</param>
		/// <param name="switchPmText">If passed, clients will display a button with specified text that switches the user to a private chat with the bot and sends the bot a start message with the parameter switch_pm_parameter</param>
		/// <param name="switchPmParameter">Parameter for the start message sent to the bot when user presses the switch button</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, <c>true</c> is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#answerinlinequery"/>
		Task AnswerInlineQueryAsync(
			string inlineQueryId,
			IEnumerable<InlineQueryResultBase> results,
			int? cacheTime = default,
			bool isPersonal = default,
			string nextOffset = default,
			string switchPmText = default,
			string switchPmParameter = default,
			CancellationToken cancellationToken = default);

		#endregion Inline mode

		#region Stickers

		/// <summary>
		/// Use this method to get a sticker set.
		/// </summary>
		/// <param name="name">Short name of the sticker set that is used in t.me/addstickers/ URLs (e.g., animals)</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>On success, a StickerSet object is returned.</returns>
		/// <see href="https://core.telegram.org/bots/api#getstickerset"/>
		Task<StickerSet> GetStickerSetAsync(
			string name,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to upload a .png file with a sticker for later use in createNewStickerSet and addStickerToSet methods (can be used multiple times).
		/// </summary>
		/// <param name="userId">User identifier of sticker file owner</param>
		/// <param name="pngSticker">Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns the uploaded File on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#uploadstickerfile"/>
		Task<File> UploadStickerFileAsync(
			int userId,
			InputFileStream pngSticker,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to create new sticker set owned by a user. The bot will be able to edit the created sticker set.
		/// </summary>
		/// <param name="userId">User identifier of created sticker set owner</param>
		/// <param name="name">Short name of sticker set, to be used in t.me/addstickers/ URLs (e.g., animals). Can contain only English letters, digits and underscores. Must begin with a letter, can't contain consecutive underscores and must end in “_by_&lt;bot_username&gt;”. &lt;bot_username&gt; is case insensitive. 1-64 characters.</param>
		/// <param name="title">Sticker set title, 1-64 characters</param>
		/// <param name="pngSticker">Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.</param>
		/// <param name="emojis">One or more emoji corresponding to the sticker</param>
		/// <param name="isMasks">Pass True, if a set of mask stickers should be created</param>
		/// <param name="maskPosition">Position where the mask should be placed on faces</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns True on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#createnewstickerset"/>
		Task CreateNewStickerSetAsync(
			int userId,
			string name,
			string title,
			InputOnlineFile pngSticker,
			string emojis,
			bool isMasks = default,
			MaskPosition maskPosition = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to add a new sticker to a set created by the bot.
		/// </summary>
		/// <param name="userId">User identifier of sticker set owner</param>
		/// <param name="name">Sticker set name</param>
		/// <param name="pngSticker">Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.</param>
		/// <param name="emojis">One or more emoji corresponding to the sticker</param>
		/// <param name="maskPosition">Position where the mask should be placed on faces</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>True on success</returns>
		/// <see href="https://core.telegram.org/bots/api#addstickertoset"/>
		Task AddStickerToSetAsync(
			int userId,
			string name,
			InputOnlineFile pngSticker,
			string emojis,
			MaskPosition maskPosition = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to create new sticker set owned by a user. The bot will be able to edit the created sticker set.
		/// </summary>
		/// <param name="userId">User identifier of created sticker set owner</param>
		/// <param name="name">Short name of sticker set, to be used in t.me/addstickers/ URLs (e.g., animals). Can contain only English letters, digits and underscores. Must begin with a letter, can't contain consecutive underscores and must end in “_by_&lt;bot_username&gt;”. &lt;bot_username&gt; is case insensitive. 1-64 characters.</param>
		/// <param name="title">Sticker set title, 1-64 characters</param>
		/// <param name="tgsSticker">Tgs animation with the sticker</param>
		/// <param name="emojis">One or more emoji corresponding to the sticker</param>
		/// <param name="isMasks">Pass True, if a set of mask stickers should be created</param>
		/// <param name="maskPosition">Position where the mask should be placed on faces</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns True on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#createnewstickerset"/>
		Task CreateNewAnimatedStickerSetAsync(
			int userId,
			string name,
			string title,
			InputFileStream tgsSticker,
			string emojis,
			bool isMasks = default,
			MaskPosition maskPosition = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to add a new sticker to a set created by the bot.
		/// </summary>
		/// <param name="userId">User identifier of sticker set owner</param>
		/// <param name="name">Sticker set name</param>
		/// <param name="tgsSticker">Tgs animation with the sticker</param>
		/// <param name="emojis">One or more emoji corresponding to the sticker</param>
		/// <param name="maskPosition">Position where the mask should be placed on faces</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>True on success</returns>
		/// <see href="https://core.telegram.org/bots/api#addstickertoset"/>
		Task AddAnimatedStickerToSetAsync(
			int userId,
			string name,
			InputFileStream tgsSticker,
			string emojis,
			MaskPosition maskPosition = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to move a sticker in a set created by the bot to a specific position.
		/// </summary>
		/// <param name="sticker">File identifier of the sticker</param>
		/// <param name="position">New sticker position in the set, zero-based</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>True on success</returns>
		/// <see href="https://core.telegram.org/bots/api#setstickerpositioninset"/>
		Task SetStickerPositionInSetAsync(
			string sticker,
			int position,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to delete a sticker from a set created by the bot.
		/// </summary>
		/// <param name="sticker">File identifier of the sticker</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns True on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#deletestickerfromset"/>
		Task DeleteStickerFromSetAsync(
			string sticker,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to set the thumbnail of a sticker set. Animated thumbnails can be set for animated sticker sets only. Returns True on success.
		/// </summary>
		/// <param name="name">Sticker set name</param>
		/// <param name="userId">User identifier of the sticker set owner</param>
		/// <param name="thumb">A PNG image or a TGS animation with the thumbnail</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns True on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#setstickersetthumb"/>
		Task SetStickerSetThumbAsync(
			string name,
			int userId,
			InputOnlineFile thumb = default,
			CancellationToken cancellationToken = default);

		#endregion

		#region Group and channel management


		/// <summary>
		/// Use this method to change the title of a chat. Titles can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
		/// </summary>
		/// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
		/// <param name="title">New chat title, 1-255 characters</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns true on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#setchattitle"/>
		Task SetChatTitleAsync(
			ChatId chatId,
			string title,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to change the description of a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
		/// </summary>
		/// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
		/// <param name="description">New chat description, 0-255 characters. Defaults to an empty string, which would clear the description.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns true on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#setchatdescription"/>
		Task SetChatDescriptionAsync(
			ChatId chatId,
			string description = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to pin a message in a supergroup. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
		/// </summary>
		/// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
		/// <param name="messageId">Identifier of a message to pin</param>
		/// <param name="disableNotification">Pass True, if it is not necessary to send a notification to all group members about the new pinned message</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns true on success.</returns>
		/// <see href="https://core.telegram.org/bots/api#pinchatmessage"/>
		Task PinChatMessageAsync(
			ChatId chatId,
			int messageId,
			bool disableNotification = default,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Use this method to unpin a message in a supergroup chat. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
		/// </summary>
		/// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>Returns true on success</returns>
		/// <see href="https://core.telegram.org/bots/api#unpinchatmessage"/>
		Task UnpinChatMessageAsync(ChatId chatId, CancellationToken cancellationToken = default);


		#endregion
	}
}

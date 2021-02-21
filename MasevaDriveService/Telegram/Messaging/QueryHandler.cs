using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MasevaDriveService
{
	public abstract class QueryHandler
	{
		protected CallbackQuery query {get;set;}
		protected TelegramBotClient Owner { get; set; }		
		internal protected RequestData Data { get; set; }
		public ChatId ChatID => query?.Message.Chat.Id;
		public  ChatId QueryID => query?.Id;
		protected int MessageID => query.Message.MessageId;
		string innerError;
		public QueryHandler()
		{
		}

		public virtual string AnswerMessage() => null;

		public virtual Task RaiseError(string errorMessage)
		{
			return Task.Factory.StartNew(() =>
			{
				innerError = errorMessage;
			});
		}

		public abstract Task Handle();

		public async Task ReplyToRequest()
		{
			try
			{
				await Handle();				
				if (innerError != null)
					await Owner.AnswerCallbackQueryAsync(QueryID, innerError, true);
				else
					if (AnswerMessage() is string message && message != null)
						await Owner.SendTextMessageAsync(ChatID, message);
			}
			catch (Exception innerError)
			{
				try
				{
					await Owner.AnswerCallbackQueryAsync(QueryID, innerError.ToString(), true);
				}
				catch (Exception outerError)
				{
					Owner.RaiseError(outerError.ToString());
				}
			}				
		}


		public static QueryHandler InitializeMessage(Type concreteType, CallbackQuery query, TelegramBotClient owner)
		{
			var result = Activator.CreateInstance(concreteType) as QueryHandler;
			result.Owner = owner;
			result.query = query;			
			return result;
		}
	}

	public class NotValidDataMessage : QueryHandler
	{
		public NotValidDataMessage(CallbackQuery query, TelegramBotClient owner)
		{
			this.query = query;
			this.Owner = owner;
		}

		public override string AnswerMessage()
		{
			return "Received not valid callback data. Parse error.";
		}
		public override Task Handle()
		{
			return null;
		}
		/*public override Task Handle()
		{
			return null;//return Owner.AnswerCallbackQueryAsync(query.Id, , true);			
		}*/
	}

	public class MissingInfoForBuildMessage : QueryHandler
	{
		private RequestKind Action;
		public MissingInfoForBuildMessage(RequestKind action, CallbackQuery query, TelegramBotClient owner)
		{
			this.Action = action;
			this.query = query;
			this.Owner = owner;
		}

		public override string AnswerMessage()
		{
			return "Nucleo storage does not contain info for '" + Action + " action'. Parse error.";
		}

		public override Task Handle()
		{
			return null;
		}
	}
	public static class TelegramExtensions
	{		
		static readonly Dictionary<RequestKind, Type> nucleoData = new Dictionary<RequestKind, Type>();
		static TelegramExtensions()
		{
			var declareHandlers = CallbackQueryHandlerAttribute.GetAllDefined(typeof(QueryHandler));
			foreach (var handler in declareHandlers)
				nucleoData.Add(handler.Action, handler.TypeOfHandler);
		}
		public static QueryHandler Parse(this CallbackQuery query, TelegramBotClient owner)
		{
			QueryHandler result;
			var queryData = RequestData.Parse(query.Data);
			if (queryData.IsParseError)
				return new NotValidDataMessage(query, owner);
						
			if (nucleoData.ContainsKey(queryData.Action))
				result = QueryHandler.InitializeMessage(nucleoData[queryData.Action],query, owner);
			else
				result = new MissingInfoForBuildMessage(queryData.Action, query, owner);

			result.Data = queryData;
			return result;
		}

		public static void RaiseError(this TelegramBotClient owner, string message)
		{
			TelegramClient.Instance.RaiseError(message);
		}
	}


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MasevaDriveService
{
	public abstract class GMessage
	{
		private CallbackQuery _query;
		protected CallbackQuery query 
		{
			get { return _query; }
			set 
			{ 
				_query = value;
				Data = CallbackData.Parse(query.Data);
			} 
		}
		protected TelegramBotClient Owner { get; set; }		
		protected CallbackData Data { get; set; }
		protected ChatId ChatID => query?.Message.Chat.Id;
		protected ChatId QueryID => query?.Id;
		protected int MessageID => query.Message.MessageId;
		public GMessage()
		{
		}

		public virtual void Handle()
		{
		}
		public abstract Task Replay();

		public static GMessage InitializeMesage(Type concreteType, CallbackQuery query, TelegramBotClient owner)
		{
			var result = Activator.CreateInstance(concreteType) as GMessage;
			result.Owner = owner;
			result.query = query;
			return result;
		}
	}

	public class NotValidDataMessage : GMessage
	{
		public NotValidDataMessage(CallbackQuery query, TelegramBotClient owner)
		{
			this.query = query;
			this.Owner = owner;
		}

		public override Task Replay()
		{
			return Owner.AnswerCallbackQueryAsync(query.Id, "Received not valid callback data. Parse error.", true);			
		}
	}

	public class MissingInfoForBuildMessage : GMessage
	{
		private string seed;
		public MissingInfoForBuildMessage(string seed, CallbackQuery query, TelegramBotClient owner)
		{
			this.seed = seed;
			this.query = query;
			this.Owner = owner;
		}
		public override Task Replay()
		{
			return Owner.AnswerCallbackQueryAsync(query.Id, "Nucleo storage does not contain info for seed '"+seed+"'. Parse error.", true);
		}
	}
	public static class TelegramExtensions
	{		
		static readonly Dictionary<string, Type> nucleoData = new Dictionary<string, Type>();
		static TelegramExtensions()
		{
			var declareHandlers = TelegramMessageHandlerAttribute.GetAllDefined(typeof(GMessage));
			foreach (var handler in declareHandlers)
				nucleoData.Add(handler.Id, handler.TypeOfHandler);

		}
		public static GMessage Parse(this CallbackQuery query, TelegramBotClient owner)
		{
			var queryData = CallbackData.Parse(query.Data);
			if (queryData.IsParseError)
				return new NotValidDataMessage(query, owner);
						
			if (nucleoData.ContainsKey(queryData.Code))
				return GMessage.InitializeMesage(nucleoData[queryData.Code],query, owner);
			else
				return new MissingInfoForBuildMessage(queryData.Code, query, owner);
		}		
	}


}

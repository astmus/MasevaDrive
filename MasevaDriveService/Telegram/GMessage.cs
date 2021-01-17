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
		protected CallbackQuery query { get; set; }
		protected TelegramBotClient owner { get; set; }		

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
			result.owner = owner;
			result.query = query;
			return result;
		}
	}

	public class NotValidDataMessage : GMessage
	{
		public NotValidDataMessage(CallbackQuery query, TelegramBotClient owner)
		{
			this.query = query;
			this.owner = owner;
		}

		public override Task Replay()
		{
			return owner.AnswerCallbackQueryAsync(query.Id, "Received not valid callback data. Parse error.", true);			
		}
	}

	public class MissingInfoForBuildMessage : GMessage
	{
		private string seed;
		public MissingInfoForBuildMessage(string seed, CallbackQuery query, TelegramBotClient owner)
		{
			this.seed = seed;
			this.query = query;
			this.owner = owner;
		}
		public override Task Replay()
		{
			return owner.AnswerCallbackQueryAsync(query.Id, "Nucleo storage does not contain info for seed '"+seed+"'. Parse error.", true);
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

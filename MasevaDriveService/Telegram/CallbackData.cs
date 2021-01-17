using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MasevaDriveService
{
	public enum MesssageAction : byte
	{
		[Description("from User: pressed cancel; to User: offer cancel")]
		Cancel = 0,
		[Description("from User: pressed delete; to User: offer delete")]
		Delete = 1,
		[Description("from User: delete confirmed; to User: ask to confirm delete")]
		ConfirmDelete = 2
	}

	public class CallbackData
	{
		public byte ActionCode { get; set; }
		public MesssageAction Type => (MesssageAction)ActionCode;
		public string FileHash { get; set; }
		public string Code => ActionCode.ToString();

		public bool IsParseError { get; private set; } = false;

		//Tuple<string> d;
		const char HASH = '|';
		const char PARAMS = ':';
		private static readonly CallbackData PARSE_ERROR = new CallbackData() { IsParseError = true };
		public static CallbackData Build(MesssageAction action, string fileHash)
		{
			return new CallbackData() { ActionCode = (byte)action, FileHash = fileHash };
		}				

		public static CallbackData Parse(string data)
		{
			var splitted = data.Split(HASH);
			if (splitted.Length != 2)
				return PARSE_ERROR;
			byte action;
			if (byte.TryParse(splitted[0],out action) == false)
				return PARSE_ERROR;

			return new CallbackData() {ActionCode = action, FileHash = splitted[1] };
		}

		public override string ToString() => string.Format("{0}{1}{2}",ActionCode,HASH,FileHash);

		public static implicit operator string(CallbackData data) => data.ToString();
		public static explicit operator CallbackData(string b) => CallbackData.Parse(b);
	}

	/*
	 public static class TelegramExtensions
	{
		static readonly Dictionary<string, Func<CallbackQuery, TelegramBotClient, GMessage>> nucleoData = new Dictionary<string, Func<CallbackQuery, TelegramBotClient, GMessage>>();
		static TelegramExtensions()
		{
			throw new NotImplementedException("need fill up nucleo storage");
		}
		public static GMessage Parse(this CallbackQuery query, TelegramBotClient owner)
		{
			var typeParamsOfContexObject = query.Data.Split(';').ToList();
			if (typeParamsOfContexObject.Count != 2)
				return new NotValidDataMessage(query, owner);
			//typeParamsOfContexObject[0] split by : for params of instance
			string messageType = typeParamsOfContexObject[0];
			if (nucleoData.ContainsKey(messageType))
				return nucleoData[messageType](query, owner);
			else
				return new MissingInfoForBuildMessage(messageType, query, owner);
		}
	}
	 
	 */
}

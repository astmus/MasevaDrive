using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MasevaDriveService
{
	public enum RequestKind : short
	{
		Cancel,
		Delete,
		ConfirmDelete,
		MainMenu,
		LastPhotos,
		LastVideos,
		LastFiles,
		MainMenuFiles,
		Next,
		Prev,
		Back
	}

	class TelegramContext
	{
		internal protected RequestData Data { get; set; } = new RequestData();
		
		private static TelegramContext current = new TelegramContext();
		public static TelegramContext Current
		{
			get
			{
				return (TelegramContext)current.MemberwiseClone();
			}

			internal protected set
			{
				current = value;
			}
		}

		public static TelegramContext WithFileHash(string fileHash)
		{
			current = new TelegramContext();
			current.Data.FileHash = fileHash;
			return current;
		}

	}

	public class RequestFormatting
	{
		[JsonProperty("A",Order =1)]
		public RequestKind Action { get; set; }
		[JsonProperty("F",Order =3)]
		public string FileHash { get; set; }
		[JsonProperty("P",Order = 2)]
		[JsonConverter(typeof(ParametersConverter))]
		public string[] Parameters { get; protected set; }

		public class ParametersConverter : JsonConverter
		{
			private readonly char[] paramDelim = new char[] { ':' };
			public override bool CanConvert(Type objectType) => objectType == typeof(string[]) || objectType == typeof(string);

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				if (objectType != typeof(string[]) || reader.Value == null)
					return new string[0];

				return (reader.Value as string).Split(paramDelim);
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				writer.WriteValue(string.Join(":", (value as IEnumerable<string>)));
			}
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RequestData : RequestFormatting
	{		
		public bool IsParseError { get; private set; } = false;
		public bool IsParsed { get; private set; } = false;

		private static readonly RequestData PARSE_ERROR_INSTANCE = new RequestData() { IsParseError = true };
		
		public RequestData()
		{
		}

		public int? GetIntParam(int index)
		{
			if (Parameters.Length > index)
				return int.Parse(Parameters[index]);
			else return null;
		}				

		public static RequestData Parse(string data)
		{
			try
			{				
				RequestData callBack = JsonConvert.DeserializeObject<RequestData>(data);
				callBack.IsParsed = true;
				TelegramContext.Current = new TelegramContext() { Data = callBack };
				return callBack;
			}
			catch
			{
				return PARSE_ERROR_INSTANCE;
			}				
		}

		public override string ToString()
		{
			return string.Format("{0};{1};{2}", Action, string.Join("|", Parameters), FileHash);
		}
		private static JsonSerializerSettings settings = new JsonSerializerSettings()
		{
			NullValueHandling = NullValueHandling.Ignore
		};
		public static implicit operator string(RequestData data) => JsonConvert.SerializeObject(data, Formatting.None, settings);
		public static explicit operator RequestData(RequestKind action) => new RequestData() { Action = action};

		
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

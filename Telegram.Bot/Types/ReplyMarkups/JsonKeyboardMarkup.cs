using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Telegram.Bot.Types.ReplyMarkups
{
	class JsonKeyboardMarkup : IJsonMarkup
	{
		[JsonIgnore]
		string innerValue;
		public JsonKeyboardMarkup(string json)
		{
			innerValue = json;
		}

		public override string ToString()
		{
			return innerValue;
		}
	}

}

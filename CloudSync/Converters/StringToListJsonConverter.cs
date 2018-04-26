using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CloudSync.Converters
{
	public class StringToListJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(string[]);
		}		

		public override bool CanRead => true;
		public override bool CanWrite => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (objectType != typeof(string []))
				throw new ArgumentException();

			return (reader.Value as String).Split(new char[] {' '});
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue(String.Join(" ", (value as IEnumerable<string>)));
		}
	}
}

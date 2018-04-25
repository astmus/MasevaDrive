using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync.Converters
{
	public class TimeSpanJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(TimeSpan);
		}

		public override bool CanRead => true;
		public override bool CanWrite => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (objectType != typeof(TimeSpan))
				throw new ArgumentException();

			return TimeSpan.FromSeconds((long)reader.Value);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var duration = (TimeSpan)value;
			writer.WriteValue(duration.TotalSeconds.ToString());
		}
	}
}

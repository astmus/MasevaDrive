﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using Medallion.Shell;

namespace DriveApi.Model
{
	public partial class MediaFileInfo
	{
		[JsonProperty("streams")]
		public MediaStream[] Streams { get; set; }

		[JsonProperty("format")]
		public MediaFormat Format { get; set; }

		public static MediaFileInfo FromJson(string json) => JsonConvert.DeserializeObject<MediaFileInfo>(json, Converter.Settings);
	}

	public static class MediaFileInfoExtension
	{
		public static MediaFileInfo GetMediaInfo(this FileInfo info)
		{
			var cmd = Command.Run(@"ffprobe", null, options => options.StartInfo(i => i.Arguments = string.Format("-i \"{0}\" -v error -print_format json -show_format -show_streams", info.FullName)));
			cmd.Wait();
			var res = MediaFileInfo.FromJson(cmd.StandardOutput.ReadToEnd());
			return res;
		}
	}

	public partial class MediaFormat
	{
		[JsonProperty("filename")]
		public string Filename { get; set; }

		[JsonProperty("nb_streams")]
		public long NbStreams { get; set; }

		[JsonProperty("nb_programs")]
		public long NbPrograms { get; set; }

		[JsonProperty("format_name")]
		public string FormatName { get; set; }

		[JsonProperty("format_long_name")]
		public string FormatLongName { get; set; }

		[JsonProperty("start_time")]
		public string StartTime { get; set; }

		[JsonProperty("duration")]
		public double Duration { get; set; }

		[JsonProperty("size")]
		[JsonConverter(typeof(PurpleParseStringConverter))]
		public long Size { get; set; }

		[JsonProperty("bit_rate")]
		[JsonConverter(typeof(PurpleParseStringConverter))]
		public long BitRate { get; set; }

		[JsonProperty("probe_score")]
		public long ProbeScore { get; set; }

		[JsonProperty("tags")]
		public MediaFormatTags Tags { get; set; }
	}

	public partial class MediaFormatTags
	{
		[JsonProperty("major_brand")]
		public string MajorBrand { get; set; }

		[JsonProperty("minor_version")]
		[JsonConverter(typeof(PurpleParseStringConverter))]
		public long MinorVersion { get; set; }

		[JsonProperty("compatible_brands")]
		public string CompatibleBrands { get; set; }

		[JsonProperty("creation_time")]
		public DateTimeOffset CreationTime { get; set; }
	}

	public class MediaStream
	{
		[JsonProperty("index")]
		public long Index { get; set; }

		[JsonProperty("codec_name")]
		public string CodecName { get; set; }

		[JsonProperty("codec_long_name")]
		public string CodecLongName { get; set; }

		[JsonProperty("profile")]
		public string Profile { get; set; }

		[JsonProperty("codec_type")]
		public string CodecType { get; set; }

		[JsonProperty("codec_time_base")]
		public string CodecTimeBase { get; set; }

		[JsonProperty("codec_tag_string")]
		public string CodecTagString { get; set; }

		[JsonProperty("codec_tag")]
		public string CodecTag { get; set; }

		[JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
		public long? Width { get; set; }

		[JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
		public long? Height { get; set; }

		[JsonProperty("coded_width", NullValueHandling = NullValueHandling.Ignore)]
		public long? CodedWidth { get; set; }

		[JsonProperty("coded_height", NullValueHandling = NullValueHandling.Ignore)]
		public long? CodedHeight { get; set; }

		[JsonProperty("has_b_frames", NullValueHandling = NullValueHandling.Ignore)]
		public long? HasBFrames { get; set; }

		[JsonProperty("sample_aspect_ratio", NullValueHandling = NullValueHandling.Ignore)]
		public string SampleAspectRatio { get; set; }

		[JsonProperty("display_aspect_ratio", NullValueHandling = NullValueHandling.Ignore)]
		public string DisplayAspectRatio { get; set; }

		[JsonProperty("pix_fmt", NullValueHandling = NullValueHandling.Ignore)]
		public string PixFmt { get; set; }

		[JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
		public long? Level { get; set; }

		[JsonProperty("color_range", NullValueHandling = NullValueHandling.Ignore)]
		public string ColorRange { get; set; }

		[JsonProperty("color_space", NullValueHandling = NullValueHandling.Ignore)]
		public string ColorSpace { get; set; }

		[JsonProperty("color_transfer", NullValueHandling = NullValueHandling.Ignore)]
		public string ColorTransfer { get; set; }

		[JsonProperty("color_primaries", NullValueHandling = NullValueHandling.Ignore)]
		public string ColorPrimaries { get; set; }

		[JsonProperty("chroma_location", NullValueHandling = NullValueHandling.Ignore)]
		public string ChromaLocation { get; set; }

		[JsonProperty("refs", NullValueHandling = NullValueHandling.Ignore)]
		public long? Refs { get; set; }

		[JsonProperty("is_avc", NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(FluffyParseStringConverter))]
		public bool? IsAvc { get; set; }

		[JsonProperty("nal_length_size", NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(PurpleParseStringConverter))]
		public long? NalLengthSize { get; set; }

		[JsonProperty("r_frame_rate")]
		public string RFrameRate { get; set; }

		[JsonProperty("avg_frame_rate")]
		public string AvgFrameRate { get; set; }

		[JsonProperty("time_base")]
		public string TimeBase { get; set; }

		[JsonProperty("start_pts")]
		public long StartPts { get; set; }

		[JsonProperty("start_time")]
		public string StartTime { get; set; }

		[JsonProperty("duration_ts")]
		public long DurationTs { get; set; }

		[JsonProperty("duration")]
		public string Duration { get; set; }

		[JsonProperty("bit_rate")]
		[JsonConverter(typeof(PurpleParseStringConverter))]
		public long BitRate { get; set; }

		[JsonProperty("bits_per_raw_sample", NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(PurpleParseStringConverter))]
		public long? BitsPerRawSample { get; set; }

		[JsonProperty("nb_frames")]
		[JsonConverter(typeof(PurpleParseStringConverter))]
		public long NbFrames { get; set; }

		[JsonProperty("disposition")]
		public Dictionary<string, long> Disposition { get; set; }

		[JsonProperty("tags")]
		public MediaStreamTags Tags { get; set; }

		[JsonProperty("sample_fmt", NullValueHandling = NullValueHandling.Ignore)]
		public string SampleFmt { get; set; }

		[JsonProperty("sample_rate", NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(PurpleParseStringConverter))]
		public long? SampleRate { get; set; }

		[JsonProperty("channels", NullValueHandling = NullValueHandling.Ignore)]
		public long? Channels { get; set; }

		[JsonProperty("channel_layout", NullValueHandling = NullValueHandling.Ignore)]
		public string ChannelLayout { get; set; }

		[JsonProperty("bits_per_sample", NullValueHandling = NullValueHandling.Ignore)]
		public long? BitsPerSample { get; set; }

		[JsonProperty("max_bit_rate", NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(PurpleParseStringConverter))]
		public long? MaxBitRate { get; set; }
	}

	public class MediaStreamTags
	{
		[JsonProperty("creation_time")]
		public DateTimeOffset CreationTime { get; set; }

		[JsonProperty("language")]
		public string Language { get; set; }

		[JsonProperty("handler_name")]
		public string HandlerName { get; set; }

		[JsonProperty("encoder", NullValueHandling = NullValueHandling.Ignore)]
		public string Encoder { get; set; }
	}	

	public static class Serialize
	{
		public static string ToJson(this MediaFileInfo self) => JsonConvert.SerializeObject(self, Converter.Settings);
	}

	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}

	internal class PurpleParseStringConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			long l;
			if (Int64.TryParse(value, out l))
			{
				return l;
			}
			throw new Exception("Cannot unmarshal type long");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (long)untypedValue;
			serializer.Serialize(writer, value.ToString());
			return;
		}

		public static readonly PurpleParseStringConverter Singleton = new PurpleParseStringConverter();
	}

	internal class FluffyParseStringConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			bool b;
			if (Boolean.TryParse(value, out b))
			{
				return b;
			}
			throw new Exception("Cannot unmarshal type bool");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (bool)untypedValue;
			var boolString = value ? "true" : "false";
			serializer.Serialize(writer, boolString);
			return;
		}

		public static readonly FluffyParseStringConverter Singleton = new FluffyParseStringConverter();
	}
}

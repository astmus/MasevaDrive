using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Audio
    {
        public long AudioId { get; set; }
        public string AudioUrl { get; set; }
        public long AudioSampleRate { get; set; }
        public long AudioChannelCount { get; set; }
        public double AudioIntegratedLufs { get; set; }
        public byte[] AudioWindowInfos { get; set; }
        public long AudioDurationPerWindow { get; set; }
    }
}

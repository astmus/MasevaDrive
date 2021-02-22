using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Audio")]
    [Index(nameof(AudioUrl), Name = "Audio_Url", IsUnique = true)]
    public partial class Audio
    {
        [Key]
        [Column("Audio_Id")]
        public long AudioId { get; set; }
        [Required]
        [Column("Audio_Url")]
        public string AudioUrl { get; set; }
        [Column("Audio_SampleRate")]
        public long AudioSampleRate { get; set; }
        [Column("Audio_ChannelCount")]
        public long AudioChannelCount { get; set; }
        [Column("Audio_IntegratedLUFS")]
        public double AudioIntegratedLufs { get; set; }
        [Required]
        [Column("Audio_WindowInfos")]
        public byte[] AudioWindowInfos { get; set; }
        [Column("Audio_DurationPerWindow")]
        public long AudioDurationPerWindow { get; set; }
    }
}

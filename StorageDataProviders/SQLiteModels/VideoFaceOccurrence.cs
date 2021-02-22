using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("VideoFaceOccurrence")]
    [Index(nameof(VideoFaceOccurrenceFaceId), Name = "VideoFaceOccurrence_FaceId")]
    public partial class VideoFaceOccurrence
    {
        [Key]
        [Column("VideoFaceOccurrence_Id")]
        public long VideoFaceOccurrenceId { get; set; }
        [Column("VideoFaceOccurrence_FaceId")]
        public long VideoFaceOccurrenceFaceId { get; set; }
        [Column("VideoFaceOccurrence_BeginFrame")]
        public long VideoFaceOccurrenceBeginFrame { get; set; }
        [Column("VideoFaceOccurrence_EndFrame")]
        public long VideoFaceOccurrenceEndFrame { get; set; }
        [Column("VideoFaceOccurrence_FaceFrame")]
        public long VideoFaceOccurrenceFaceFrame { get; set; }

        [ForeignKey(nameof(VideoFaceOccurrenceFaceId))]
        [InverseProperty(nameof(Face.VideoFaceOccurrences))]
        public virtual Face VideoFaceOccurrenceFace { get; set; }
    }
}

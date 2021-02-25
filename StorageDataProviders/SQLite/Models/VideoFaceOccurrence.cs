using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class VideoFaceOccurrence
    {
        public long VideoFaceOccurrenceId { get; set; }
        public long VideoFaceOccurrenceFaceId { get; set; }
        public long VideoFaceOccurrenceBeginFrame { get; set; }
        public long VideoFaceOccurrenceEndFrame { get; set; }
        public long VideoFaceOccurrenceFaceFrame { get; set; }

        public virtual Face VideoFaceOccurrenceFace { get; set; }
    }
}

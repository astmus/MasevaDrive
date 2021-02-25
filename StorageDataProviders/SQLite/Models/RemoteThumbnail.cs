using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class RemoteThumbnail
    {
        public long RemoteThumbnailItemId { get; set; }
        public long? RemoteThumbnailWidth { get; set; }
        public long? RemoteThumbnailHeight { get; set; }
        public string RemoteThumbnailUrl { get; set; }

        public virtual Item RemoteThumbnailItem { get; set; }
    }
}

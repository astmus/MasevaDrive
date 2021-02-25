using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class PendingCloudAlbumDelete
    {
        public long PendingCloudAlbumDeleteId { get; set; }
        public string PendingCloudAlbumDeletePhotosCloudId { get; set; }
        public long? PendingCloudAlbumDeleteSourceId { get; set; }

        public virtual Source PendingCloudAlbumDeleteSource { get; set; }
    }
}

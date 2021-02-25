using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Source
    {
        public Source()
        {
            Albums = new HashSet<Album>();
            Folders = new HashSet<Folder>();
            Items = new HashSet<Item>();
            PendingCloudAlbumDeletes = new HashSet<PendingCloudAlbumDelete>();
        }

        public long SourceId { get; set; }
        public long? SourceType { get; set; }
        public string SourceUserId { get; set; }
        public string SourceUserName { get; set; }
        public long? SourceStatus { get; set; }
        public long? SourceUserEnabled { get; set; }
        public string SourcePhotosCloudUserId { get; set; }
        public string SourceDeltaSyncToken { get; set; }
        public long? SourceFullSyncCompleted { get; set; }
        public long? SourceItemsResyncing { get; set; }
        public long? SourceSignOutTime { get; set; }
        public long? SourceOdsyncThrottleStartTime { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Folder> Folders { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<PendingCloudAlbumDelete> PendingCloudAlbumDeletes { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class PendingUploadItem
    {
        public long PendingUploadItemItemId { get; set; }
        public long PendingUploadItemAlbumId { get; set; }
        public long? PendingUploadItemResult { get; set; }
        public string PendingUploadItemResourceId { get; set; }
        public long? PendingUploadItemSourceId { get; set; }
        public long? PendingUploadItemType { get; set; }
        public long? PendingUploadItemNeedsAeupload { get; set; }
        public long? PendingUploadItemActionAfterUpload { get; set; }
        public string PendingUploadItemAlbumRemoteId { get; set; }
        public long? PendingUploadItemResourceIdSourceType { get; set; }
        public string PendingUploadItemUploadSessionUrl { get; set; }
        public string PendingUploadItemResumableUploadUrl { get; set; }

        public virtual Album PendingUploadItemAlbum { get; set; }
        public virtual Item PendingUploadItemItem { get; set; }
        public virtual Source PendingUploadItemSource { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class RemoteItem
    {
        public string RemoteItemRemoteId { get; set; }
        public string RemoteItemRemoteParentId { get; set; }
        public long? RemoteItemItemId { get; set; }
        public long? RemoteItemFolderId { get; set; }
        public string RemoteItemDownloadUrl { get; set; }
        public long? RemoteItemPresentAtSync { get; set; }
        public string RemoteItemPhotosCloudId { get; set; }

        public virtual Folder RemoteItemFolder { get; set; }
        public virtual Item RemoteItemItem { get; set; }
    }
}

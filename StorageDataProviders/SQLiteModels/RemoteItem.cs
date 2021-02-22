using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    [Table("RemoteItem")]
    [Index(nameof(RemoteItemFolderId), Name = "RemoteItem_FolderId", IsUnique = true)]
    [Index(nameof(RemoteItemItemId), Name = "RemoteItem_ItemId", IsUnique = true)]
    [Index(nameof(RemoteItemRemoteId), Name = "RemoteItem_RemoteId")]
    public partial class RemoteItem
    {
        [Column("RemoteItem_RemoteId")]
        public string RemoteItemRemoteId { get; set; }
        [Column("RemoteItem_RemoteParentId")]
        public string RemoteItemRemoteParentId { get; set; }
        [Column("RemoteItem_ItemId")]
        public long? RemoteItemItemId { get; set; }
        [Column("RemoteItem_FolderId")]
        public long? RemoteItemFolderId { get; set; }
        [Column("RemoteItem_DownloadUrl")]
        public string RemoteItemDownloadUrl { get; set; }
        [Column("RemoteItem_PresentAtSync")]
        public long? RemoteItemPresentAtSync { get; set; }
        [Column("RemoteItem_PhotosCloudId")]
        public string RemoteItemPhotosCloudId { get; set; }

        [ForeignKey(nameof(RemoteItemFolderId))]
        public virtual Folder RemoteItemFolder { get; set; }
        [ForeignKey(nameof(RemoteItemItemId))]
        public virtual Item RemoteItemItem { get; set; }
    }
}

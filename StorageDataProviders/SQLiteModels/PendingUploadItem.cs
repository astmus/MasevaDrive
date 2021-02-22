using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    [Table("PendingUploadItem")]
    [Index(nameof(PendingUploadItemAlbumId), Name = "PendingUploadItem_AlbumId")]
    [Index(nameof(PendingUploadItemItemId), Name = "PendingUploadItem_ItemAlbum")]
    [Index(nameof(PendingUploadItemSourceId), Name = "PendingUploadItem_SourceId")]
    public partial class PendingUploadItem
    {
        [Column("PendingUploadItem_ItemId")]
        public long PendingUploadItemItemId { get; set; }
        [Column("PendingUploadItem_AlbumId")]
        public long PendingUploadItemAlbumId { get; set; }
        [Column("PendingUploadItem_Result")]
        public long? PendingUploadItemResult { get; set; }
        [Column("PendingUploadItem_ResourceId")]
        public string PendingUploadItemResourceId { get; set; }
        [Column("PendingUploadItem_SourceId")]
        public long? PendingUploadItemSourceId { get; set; }
        [Column("PendingUploadItem_Type")]
        public long? PendingUploadItemType { get; set; }
        [Column("PendingUploadItem_NeedsAEUpload")]
        public long? PendingUploadItemNeedsAeupload { get; set; }
        [Column("PendingUploadItem_ActionAfterUpload")]
        public long? PendingUploadItemActionAfterUpload { get; set; }
        [Column("PendingUploadItem_AlbumRemoteId")]
        public string PendingUploadItemAlbumRemoteId { get; set; }
        [Column("PendingUploadItem_ResourceIdSourceType")]
        public long? PendingUploadItemResourceIdSourceType { get; set; }
        [Column("PendingUploadItem_UploadSessionUrl")]
        public string PendingUploadItemUploadSessionUrl { get; set; }
        [Column("PendingUploadItem_ResumableUploadUrl")]
        public string PendingUploadItemResumableUploadUrl { get; set; }

        [ForeignKey(nameof(PendingUploadItemAlbumId))]
        public virtual Album PendingUploadItemAlbum { get; set; }
        [ForeignKey(nameof(PendingUploadItemItemId))]
        public virtual Item PendingUploadItemItem { get; set; }
        [ForeignKey(nameof(PendingUploadItemSourceId))]
        public virtual Source PendingUploadItemSource { get; set; }
    }
}

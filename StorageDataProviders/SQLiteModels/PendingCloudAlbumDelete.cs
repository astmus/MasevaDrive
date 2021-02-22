using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("PendingCloudAlbumDelete")]
    [Index(nameof(PendingCloudAlbumDeletePhotosCloudId), nameof(PendingCloudAlbumDeleteSourceId), Name = "PendingCloudAlbumDelete_PhotosCloudIdSourceId", IsUnique = true)]
    [Index(nameof(PendingCloudAlbumDeleteSourceId), Name = "PendingCloudAlbumDelete_SourceId")]
    public partial class PendingCloudAlbumDelete
    {
        [Key]
        [Column("PendingCloudAlbumDelete_Id")]
        public long PendingCloudAlbumDeleteId { get; set; }
        [Column("PendingCloudAlbumDelete_PhotosCloudId")]
        public string PendingCloudAlbumDeletePhotosCloudId { get; set; }
        [Column("PendingCloudAlbumDelete_SourceId")]
        public long? PendingCloudAlbumDeleteSourceId { get; set; }

        [ForeignKey(nameof(PendingCloudAlbumDeleteSourceId))]
        [InverseProperty(nameof(Source.PendingCloudAlbumDeletes))]
        public virtual Source PendingCloudAlbumDeleteSource { get; set; }
    }
}

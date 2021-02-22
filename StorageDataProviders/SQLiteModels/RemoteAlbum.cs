using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("RemoteAlbum")]
    [Index(nameof(RemoteAlbumPhotosCloudId), Name = "RemoteAlbum_PhotosCloudId")]
    public partial class RemoteAlbum
    {
        [Key]
        [Column("RemoteAlbum_AlbumId")]
        public long RemoteAlbumAlbumId { get; set; }
        [Column("RemoteAlbum_RemoteId")]
        public string RemoteAlbumRemoteId { get; set; }
        [Column("RemoteAlbum_PresentAtSync")]
        public long? RemoteAlbumPresentAtSync { get; set; }
        [Column("RemoteAlbum_GenericViewUrl")]
        public string RemoteAlbumGenericViewUrl { get; set; }
        [Column("RemoteAlbum_CoverDuringUpload")]
        public string RemoteAlbumCoverDuringUpload { get; set; }
        [Column("RemoteAlbum_AlbumType")]
        public long? RemoteAlbumAlbumType { get; set; }
        [Column("RemoteAlbum_PhotosCloudId")]
        public string RemoteAlbumPhotosCloudId { get; set; }
        [Column("RemoteAlbum_ETag")]
        public string RemoteAlbumEtag { get; set; }

        [ForeignKey(nameof(RemoteAlbumAlbumId))]
        [InverseProperty(nameof(Album.RemoteAlbum))]
        public virtual Album RemoteAlbumAlbum { get; set; }
    }
}

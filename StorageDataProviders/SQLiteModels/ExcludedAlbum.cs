using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ExcludedAlbum")]
    [Index(nameof(ExcludedAlbumAlbumId), Name = "ExcludedAlbum_AlbumId")]
    [Index(nameof(ExcludedAlbumExcludedForUse), Name = "ExcludedAlbum_ExcludedForUse")]
    public partial class ExcludedAlbum
    {
        [Key]
        [Column("ExcludedAlbum_Id")]
        public long ExcludedAlbumId { get; set; }
        [Column("ExcludedAlbum_AlbumId")]
        public long ExcludedAlbumAlbumId { get; set; }
        [Column("ExcludedAlbum_ExcludedForUse")]
        public long ExcludedAlbumExcludedForUse { get; set; }
        [Column("ExcludedAlbum_ExcludedDate")]
        public long ExcludedAlbumExcludedDate { get; set; }

        [ForeignKey(nameof(ExcludedAlbumAlbumId))]
        [InverseProperty(nameof(Album.ExcludedAlbums))]
        public virtual Album ExcludedAlbumAlbum { get; set; }
    }
}

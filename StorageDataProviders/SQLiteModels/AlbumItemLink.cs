using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    [Table("AlbumItemLink")]
    [Index(nameof(AlbumItemLinkAlbumId), nameof(AlbumItemLinkItemId), Name = "AlbumItemLink_AlbumIdItemId", IsUnique = true)]
    [Index(nameof(AlbumItemLinkItemId), nameof(AlbumItemLinkAlbumId), Name = "AlbumItemLink_ItemIdAlbumId")]
    public partial class AlbumItemLink
    {
        [Column("AlbumItemLink_AlbumId")]
        public long AlbumItemLinkAlbumId { get; set; }
        [Column("AlbumItemLink_ItemId")]
        public long AlbumItemLinkItemId { get; set; }
        [Column("AlbumItemLink_Order")]
        public long? AlbumItemLinkOrder { get; set; }
        [Column("AlbumItemLink_ItemPhotosCloudId")]
        public string AlbumItemLinkItemPhotosCloudId { get; set; }

        [ForeignKey(nameof(AlbumItemLinkAlbumId))]
        public virtual Album AlbumItemLinkAlbum { get; set; }
        [ForeignKey(nameof(AlbumItemLinkItemId))]
        public virtual Item AlbumItemLinkItem { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class AlbumItemLink
    {
        public long AlbumItemLinkAlbumId { get; set; }
        public long AlbumItemLinkItemId { get; set; }
        public long? AlbumItemLinkOrder { get; set; }
        public string AlbumItemLinkItemPhotosCloudId { get; set; }

        public virtual Album AlbumItemLinkAlbum { get; set; }
        public virtual Item AlbumItemLinkItem { get; set; }
    }
}

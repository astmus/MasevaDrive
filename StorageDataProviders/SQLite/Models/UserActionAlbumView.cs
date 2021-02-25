using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class UserActionAlbumView
    {
        public long UserActionAlbumViewId { get; set; }
        public long UserActionAlbumViewDate { get; set; }
        public long? UserActionAlbumViewAlbumId { get; set; }
        public long UserActionAlbumViewActionOrigin { get; set; }

        public virtual Album UserActionAlbumViewAlbum { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ExcludedAlbum
    {
        public long ExcludedAlbumId { get; set; }
        public long ExcludedAlbumAlbumId { get; set; }
        public long ExcludedAlbumExcludedForUse { get; set; }
        public long ExcludedAlbumExcludedDate { get; set; }

        public virtual Album ExcludedAlbumAlbum { get; set; }
    }
}

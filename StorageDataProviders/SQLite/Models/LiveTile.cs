using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class LiveTile
    {
        public long LiveTileItemId { get; set; }

        public virtual Item LiveTileItem { get; set; }
    }
}

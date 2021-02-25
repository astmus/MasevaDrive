using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class LocationRegionFtsSegment
    {
        public long Blockid { get; set; }
        public byte[] Block { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ItemEngineExemplar
    {
        public long ItemEngineExemplarId { get; set; }
        public long ItemEngineExemplarItemId { get; set; }
        public byte[] ItemEngineExemplarExemplar { get; set; }

        public virtual Item ItemEngineExemplarItem { get; set; }
    }
}

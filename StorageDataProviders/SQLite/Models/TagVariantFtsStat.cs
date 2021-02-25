using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class TagVariantFtsStat
    {
        public long Id { get; set; }
        public byte[] Value { get; set; }
    }
}

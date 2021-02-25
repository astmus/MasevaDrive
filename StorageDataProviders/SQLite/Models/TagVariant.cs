using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class TagVariant
    {
        public long TagVariantId { get; set; }
        public long? TagVariantTagResourceId { get; set; }
        public string TagVariantText { get; set; }
        public long? TagVariantIsPrimary { get; set; }
    }
}

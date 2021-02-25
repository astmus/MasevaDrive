using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ItemVideoQuality
    {
        public long ItemVideoQualityId { get; set; }
        public long ItemVideoQualityItemId { get; set; }
        public double? ItemVideoQualityQuality { get; set; }
        public long? ItemVideoQualityQualityLevel { get; set; }
        public long ItemVideoQualityBeginFrame { get; set; }
        public long ItemVideoQualityEndFrame { get; set; }

        public virtual Item ItemVideoQualityItem { get; set; }
    }
}

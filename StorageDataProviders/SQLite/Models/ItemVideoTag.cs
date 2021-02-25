using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ItemVideoTag
    {
        public long ItemVideoTagsId { get; set; }
        public long ItemVideoTagsItemTagsId { get; set; }
        public double? ItemVideoTagsConfidence { get; set; }
        public long ItemVideoTagsBeginFrame { get; set; }
        public long ItemVideoTagsEndFrame { get; set; }

        public virtual ItemTag ItemVideoTagsItemTags { get; set; }
    }
}

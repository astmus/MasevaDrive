using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ItemTag
    {
        public ItemTag()
        {
            ItemVideoTags = new HashSet<ItemVideoTag>();
        }

        public long ItemTagsId { get; set; }
        public long ItemTagsItemId { get; set; }
        public long ItemTagsTagId { get; set; }
        public double? ItemTagsConfidence { get; set; }

        public virtual Item ItemTagsItem { get; set; }
        public virtual Tag ItemTagsTag { get; set; }
        public virtual ICollection<ItemVideoTag> ItemVideoTags { get; set; }
    }
}

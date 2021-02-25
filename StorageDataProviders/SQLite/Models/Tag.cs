using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Tag
    {
        public Tag()
        {
            ExcludedItemTags = new HashSet<ExcludedItemTag>();
            ExcludedTags = new HashSet<ExcludedTag>();
            ItemTags = new HashSet<ItemTag>();
        }

        public long TagId { get; set; }
        public long? TagResourceId { get; set; }
        public long? TagCreatedDate { get; set; }

        public virtual ICollection<ExcludedItemTag> ExcludedItemTags { get; set; }
        public virtual ICollection<ExcludedTag> ExcludedTags { get; set; }
        public virtual ICollection<ItemTag> ItemTags { get; set; }
    }
}

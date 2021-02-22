using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Tag")]
    [Index(nameof(TagResourceId), Name = "Tag_ResourceId")]
    public partial class Tag
    {
        public Tag()
        {
            ExcludedItemTags = new HashSet<ExcludedItemTag>();
            ExcludedTags = new HashSet<ExcludedTag>();
            ItemTags = new HashSet<ItemTag>();
        }

        [Key]
        [Column("Tag_Id")]
        public long TagId { get; set; }
        [Column("Tag_ResourceId")]
        public long? TagResourceId { get; set; }
        [Column("Tag_CreatedDate")]
        public long? TagCreatedDate { get; set; }

        [InverseProperty(nameof(ExcludedItemTag.ExcludedItemTagTag))]
        public virtual ICollection<ExcludedItemTag> ExcludedItemTags { get; set; }
        [InverseProperty(nameof(ExcludedTag.ExcludedTagTag))]
        public virtual ICollection<ExcludedTag> ExcludedTags { get; set; }
        [InverseProperty(nameof(ItemTag.ItemTagsTag))]
        public virtual ICollection<ItemTag> ItemTags { get; set; }
    }
}

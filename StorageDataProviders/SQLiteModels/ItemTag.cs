using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Index(nameof(ItemTagsItemId), Name = "ItemTags_ItemId")]
    [Index(nameof(ItemTagsItemId), nameof(ItemTagsTagId), Name = "ItemTags_ItemId_TagId", IsUnique = true)]
    [Index(nameof(ItemTagsTagId), Name = "ItemTags_TagId")]
    public partial class ItemTag
    {
        public ItemTag()
        {
            ItemVideoTags = new HashSet<ItemVideoTag>();
        }

        [Key]
        [Column("ItemTags_Id")]
        public long ItemTagsId { get; set; }
        [Column("ItemTags_ItemId")]
        public long ItemTagsItemId { get; set; }
        [Column("ItemTags_TagId")]
        public long ItemTagsTagId { get; set; }
        [Column("ItemTags_Confidence")]
        public double? ItemTagsConfidence { get; set; }

        [ForeignKey(nameof(ItemTagsItemId))]
        [InverseProperty(nameof(Item.ItemTags))]
        public virtual Item ItemTagsItem { get; set; }
        [ForeignKey(nameof(ItemTagsTagId))]
        [InverseProperty(nameof(Tag.ItemTags))]
        public virtual Tag ItemTagsTag { get; set; }
        [InverseProperty(nameof(ItemVideoTag.ItemVideoTagsItemTags))]
        public virtual ICollection<ItemVideoTag> ItemVideoTags { get; set; }
    }
}

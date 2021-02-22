using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Index(nameof(ItemVideoTagsItemTagsId), Name = "ItemVideoTags_ItemTagsId")]
    public partial class ItemVideoTag
    {
        [Key]
        [Column("ItemVideoTags_Id")]
        public long ItemVideoTagsId { get; set; }
        [Column("ItemVideoTags_ItemTagsId")]
        public long ItemVideoTagsItemTagsId { get; set; }
        [Column("ItemVideoTags_Confidence")]
        public double? ItemVideoTagsConfidence { get; set; }
        [Column("ItemVideoTags_BeginFrame")]
        public long ItemVideoTagsBeginFrame { get; set; }
        [Column("ItemVideoTags_EndFrame")]
        public long ItemVideoTagsEndFrame { get; set; }

        [ForeignKey(nameof(ItemVideoTagsItemTagsId))]
        [InverseProperty(nameof(ItemTag.ItemVideoTags))]
        public virtual ItemTag ItemVideoTagsItemTags { get; set; }
    }
}

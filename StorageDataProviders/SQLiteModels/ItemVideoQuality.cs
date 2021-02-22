using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ItemVideoQuality")]
    [Index(nameof(ItemVideoQualityItemId), Name = "ItemVideoQuality_ItemId")]
    public partial class ItemVideoQuality
    {
        [Key]
        [Column("ItemVideoQuality_Id")]
        public long ItemVideoQualityId { get; set; }
        [Column("ItemVideoQuality_ItemId")]
        public long ItemVideoQualityItemId { get; set; }
        [Column("ItemVideoQuality_Quality")]
        public double? ItemVideoQualityQuality { get; set; }
        [Column("ItemVideoQuality_QualityLevel")]
        public long? ItemVideoQualityQualityLevel { get; set; }
        [Column("ItemVideoQuality_BeginFrame")]
        public long ItemVideoQualityBeginFrame { get; set; }
        [Column("ItemVideoQuality_EndFrame")]
        public long ItemVideoQualityEndFrame { get; set; }

        [ForeignKey(nameof(ItemVideoQualityItemId))]
        [InverseProperty(nameof(Item.ItemVideoQualities))]
        public virtual Item ItemVideoQualityItem { get; set; }
    }
}

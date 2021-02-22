using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ItemDateTaken")]
    [Index(nameof(ItemDateTakenDay), Name = "ItemDateTaken_Day")]
    [Index(nameof(ItemDateTakenDayOfWeek), Name = "ItemDateTaken_DayOfWeek")]
    [Index(nameof(ItemDateTakenMonth), Name = "ItemDateTaken_Month")]
    [Index(nameof(ItemDateTakenYear), Name = "ItemDateTaken_Year")]
    public partial class ItemDateTaken
    {
        [Key]
        [Column("ItemDateTaken_ItemId")]
        public long ItemDateTakenItemId { get; set; }
        [Column("ItemDateTaken_Year")]
        public long? ItemDateTakenYear { get; set; }
        [Column("ItemDateTaken_Month")]
        public long? ItemDateTakenMonth { get; set; }
        [Column("ItemDateTaken_Day")]
        public long? ItemDateTakenDay { get; set; }
        [Column("ItemDateTaken_DayOfWeek")]
        public long? ItemDateTakenDayOfWeek { get; set; }
    }
}

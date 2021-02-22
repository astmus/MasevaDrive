using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    [Table("ItemEdit")]
    [Index(nameof(ItemEditItemId), Name = "ItemEdit_ItemId")]
    public partial class ItemEdit
    {
        [Column("ItemEdit_ItemId")]
        public long ItemEditItemId { get; set; }
        [Column("ItemEdit_EditTypeId")]
        public long? ItemEditEditTypeId { get; set; }
        [Column("ItemEdit_EditDate")]
        public long? ItemEditEditDate { get; set; }

        [ForeignKey(nameof(ItemEditItemId))]
        public virtual Item ItemEditItem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ItemEngineExemplar")]
    [Index(nameof(ItemEngineExemplarItemId), Name = "ItemEngineExemplar_ItemId", IsUnique = true)]
    public partial class ItemEngineExemplar
    {
        [Key]
        [Column("ItemEngineExemplar_Id")]
        public long ItemEngineExemplarId { get; set; }
        [Column("ItemEngineExemplar_ItemId")]
        public long ItemEngineExemplarItemId { get; set; }
        [Column("ItemEngineExemplar_Exemplar")]
        public byte[] ItemEngineExemplarExemplar { get; set; }

        [ForeignKey(nameof(ItemEngineExemplarItemId))]
        [InverseProperty(nameof(Item.ItemEngineExemplar))]
        public virtual Item ItemEngineExemplarItem { get; set; }
    }
}

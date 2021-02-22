using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("LiveTile")]
    public partial class LiveTile
    {
        [Key]
        [Column("LiveTile_ItemId")]
        public long LiveTileItemId { get; set; }

        [ForeignKey(nameof(LiveTileItemId))]
        [InverseProperty(nameof(Item.LiveTile))]
        public virtual Item LiveTileItem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("UserActionPrint")]
    [Index(nameof(UserActionPrintItemId), Name = "UserActionPrint_ItemId")]
    public partial class UserActionPrint
    {
        [Key]
        [Column("UserActionPrint_Id")]
        public long UserActionPrintId { get; set; }
        [Column("UserActionPrint_Date")]
        public long UserActionPrintDate { get; set; }
        [Column("UserActionPrint_ItemId")]
        public long? UserActionPrintItemId { get; set; }
        [Column("UserActionPrint_ActionOrigin")]
        public long UserActionPrintActionOrigin { get; set; }

        [ForeignKey(nameof(UserActionPrintItemId))]
        [InverseProperty(nameof(Item.UserActionPrints))]
        public virtual Item UserActionPrintItem { get; set; }
    }
}

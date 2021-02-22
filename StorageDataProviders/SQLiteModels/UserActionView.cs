using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("UserActionView")]
    [Index(nameof(UserActionViewItemId), Name = "UserActionView_ItemId")]
    public partial class UserActionView
    {
        [Key]
        [Column("UserActionView_Id")]
        public long UserActionViewId { get; set; }
        [Column("UserActionView_Date")]
        public long UserActionViewDate { get; set; }
        [Column("UserActionView_ItemId")]
        public long? UserActionViewItemId { get; set; }
        [Column("UserActionView_ActionOrigin")]
        public long UserActionViewActionOrigin { get; set; }

        [ForeignKey(nameof(UserActionViewItemId))]
        [InverseProperty(nameof(Item.UserActionViews))]
        public virtual Item UserActionViewItem { get; set; }
    }
}

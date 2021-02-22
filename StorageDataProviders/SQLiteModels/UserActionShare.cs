using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("UserActionShare")]
    [Index(nameof(UserActionShareItemId), Name = "UserActionShare_ItemId")]
    public partial class UserActionShare
    {
        [Key]
        [Column("UserActionShare_Id")]
        public long UserActionShareId { get; set; }
        [Column("UserActionShare_Date")]
        public long UserActionShareDate { get; set; }
        [Column("UserActionShare_ItemId")]
        public long? UserActionShareItemId { get; set; }
        [Column("UserActionShare_ActionOrigin")]
        public long UserActionShareActionOrigin { get; set; }
        [Required]
        [Column("UserActionShare_Target")]
        public string UserActionShareTarget { get; set; }
        [Column("UserActionShare_Result")]
        public long UserActionShareResult { get; set; }

        [ForeignKey(nameof(UserActionShareItemId))]
        [InverseProperty(nameof(Item.UserActionShares))]
        public virtual Item UserActionShareItem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("UserActionAlbumView")]
    [Index(nameof(UserActionAlbumViewAlbumId), Name = "UserActionAlbumView_AlbumId")]
    public partial class UserActionAlbumView
    {
        [Key]
        [Column("UserActionAlbumView_Id")]
        public long UserActionAlbumViewId { get; set; }
        [Column("UserActionAlbumView_Date")]
        public long UserActionAlbumViewDate { get; set; }
        [Column("UserActionAlbumView_AlbumId")]
        public long? UserActionAlbumViewAlbumId { get; set; }
        [Column("UserActionAlbumView_ActionOrigin")]
        public long UserActionAlbumViewActionOrigin { get; set; }

        [ForeignKey(nameof(UserActionAlbumViewAlbumId))]
        [InverseProperty(nameof(Album.UserActionAlbumViews))]
        public virtual Album UserActionAlbumViewAlbum { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("UserActionSlideshow")]
    [Index(nameof(UserActionSlideshowAlbumId), Name = "UserActionSlideshow_AlbumId")]
    [Index(nameof(UserActionSlideshowItemId), Name = "UserActionSlideshow_ItemId")]
    public partial class UserActionSlideshow
    {
        [Key]
        [Column("UserActionSlideshow_Id")]
        public long UserActionSlideshowId { get; set; }
        [Column("UserActionSlideshow_Date")]
        public long UserActionSlideshowDate { get; set; }
        [Column("UserActionSlideshow_AlbumId")]
        public long? UserActionSlideshowAlbumId { get; set; }
        [Column("UserActionSlideshow_ItemId")]
        public long? UserActionSlideshowItemId { get; set; }
        [Column("UserActionSlideshow_ActionOrigin")]
        public long UserActionSlideshowActionOrigin { get; set; }

        [ForeignKey(nameof(UserActionSlideshowAlbumId))]
        [InverseProperty(nameof(Album.UserActionSlideshows))]
        public virtual Album UserActionSlideshowAlbum { get; set; }
        [ForeignKey(nameof(UserActionSlideshowItemId))]
        [InverseProperty(nameof(Item.UserActionSlideshows))]
        public virtual Item UserActionSlideshowItem { get; set; }
    }
}

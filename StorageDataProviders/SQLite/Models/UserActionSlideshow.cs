using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class UserActionSlideshow
    {
        public long UserActionSlideshowId { get; set; }
        public long UserActionSlideshowDate { get; set; }
        public long? UserActionSlideshowAlbumId { get; set; }
        public long? UserActionSlideshowItemId { get; set; }
        public long UserActionSlideshowActionOrigin { get; set; }

        public virtual Album UserActionSlideshowAlbum { get; set; }
        public virtual Item UserActionSlideshowItem { get; set; }
    }
}

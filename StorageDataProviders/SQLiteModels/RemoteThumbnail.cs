using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    [Table("RemoteThumbnail")]
    [Index(nameof(RemoteThumbnailItemId), Name = "RemoteThumbnail_ItemId")]
    public partial class RemoteThumbnail
    {
        [Column("RemoteThumbnail_ItemId")]
        public long RemoteThumbnailItemId { get; set; }
        [Column("RemoteThumbnail_Width")]
        public long? RemoteThumbnailWidth { get; set; }
        [Column("RemoteThumbnail_Height")]
        public long? RemoteThumbnailHeight { get; set; }
        [Column("RemoteThumbnail_Url")]
        public string RemoteThumbnailUrl { get; set; }

        [ForeignKey(nameof(RemoteThumbnailItemId))]
        public virtual Item RemoteThumbnailItem { get; set; }
    }
}

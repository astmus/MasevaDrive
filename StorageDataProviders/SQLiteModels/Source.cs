using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Source")]
    public partial class Source
    {
        public Source()
        {
            Albums = new HashSet<Album>();
            Folders = new HashSet<Folder>();
            Items = new HashSet<Item>();
            PendingCloudAlbumDeletes = new HashSet<PendingCloudAlbumDelete>();
        }

        [Key]
        [Column("Source_Id")]
        public long SourceId { get; set; }
        [Column("Source_Type")]
        public long? SourceType { get; set; }
        [Column("Source_UserId")]
        public string SourceUserId { get; set; }
        [Column("Source_UserName")]
        public string SourceUserName { get; set; }
        [Column("Source_Status")]
        public long? SourceStatus { get; set; }
        [Column("Source_UserEnabled")]
        public long? SourceUserEnabled { get; set; }
        [Column("Source_PhotosCloudUserId")]
        public string SourcePhotosCloudUserId { get; set; }
        [Column("Source_DeltaSyncToken")]
        public string SourceDeltaSyncToken { get; set; }
        [Column("Source_FullSyncCompleted")]
        public long? SourceFullSyncCompleted { get; set; }
        [Column("Source_ItemsResyncing")]
        public long? SourceItemsResyncing { get; set; }
        [Column("Source_SignOutTime")]
        public long? SourceSignOutTime { get; set; }
        [Column("Source_ODSyncThrottleStartTime")]
        public long? SourceOdsyncThrottleStartTime { get; set; }

        [InverseProperty(nameof(Album.AlbumSourceNavigation))]
        public virtual ICollection<Album> Albums { get; set; }
        [InverseProperty(nameof(Folder.FolderSourceNavigation))]
        public virtual ICollection<Folder> Folders { get; set; }
        [InverseProperty(nameof(Item.ItemSourceNavigation))]
        public virtual ICollection<Item> Items { get; set; }
        [InverseProperty(nameof(PendingCloudAlbumDelete.PendingCloudAlbumDeleteSource))]
        public virtual ICollection<PendingCloudAlbumDelete> PendingCloudAlbumDeletes { get; set; }
    }
}

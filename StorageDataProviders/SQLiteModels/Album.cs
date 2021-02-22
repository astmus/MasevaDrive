using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Album")]
    [Index(nameof(AlbumCoverItemId), Name = "Album_CoverItemId")]
    [Index(nameof(AlbumName), Name = "Album_Name")]
    [Index(nameof(AlbumOrder), Name = "Album_Order")]
    [Index(nameof(AlbumSourceId), Name = "Album_SourceId")]
    [Index(nameof(AlbumState), Name = "Album_State")]
    public partial class Album
    {
        public Album()
        {
            CloudAlbums = new HashSet<CloudAlbum>();
            ExcludedAlbums = new HashSet<ExcludedAlbum>();
            UserActionAlbumViews = new HashSet<UserActionAlbumView>();
            UserActionSlideshows = new HashSet<UserActionSlideshow>();
        }

        [Key]
        [Column("Album_Id")]
        public long AlbumId { get; set; }
        [Column("Album_Name")]
        public string AlbumName { get; set; }
        [Column("Album_Type")]
        public long AlbumType { get; set; }
        [Column("Album_State")]
        public long AlbumState { get; set; }
        [Column("Album_QueryType")]
        public long AlbumQueryType { get; set; }
        [Column("Album_QueryBoundsType")]
        public long AlbumQueryBoundsType { get; set; }
        [Column("Album_Query")]
        public string AlbumQuery { get; set; }
        [Column("Album_DateCreated")]
        public long AlbumDateCreated { get; set; }
        [Column("Album_DateUpdated")]
        public long? AlbumDateUpdated { get; set; }
        [Column("Album_DateUserModified")]
        public long? AlbumDateUserModified { get; set; }
        [Column("Album_DateViewed")]
        public long? AlbumDateViewed { get; set; }
        [Column("Album_DateShared")]
        public long? AlbumDateShared { get; set; }
        [Column("Album_Count")]
        public long AlbumCount { get; set; }
        [Column("Album_CoverItemId")]
        public long? AlbumCoverItemId { get; set; }
        [Column("Album_CoverBoundsLeft")]
        public double? AlbumCoverBoundsLeft { get; set; }
        [Column("Album_CoverBoundsTop")]
        public double? AlbumCoverBoundsTop { get; set; }
        [Column("Album_CoverBoundsRight")]
        public double? AlbumCoverBoundsRight { get; set; }
        [Column("Album_CoverBoundsBottom")]
        public double? AlbumCoverBoundsBottom { get; set; }
        [Column("Album_Visibility")]
        public long AlbumVisibility { get; set; }
        [Column("Album_EventStartDate")]
        public long? AlbumEventStartDate { get; set; }
        [Column("Album_EventEndDate")]
        public long? AlbumEventEndDate { get; set; }
        [Column("Album_SummaryStartDate")]
        public long? AlbumSummaryStartDate { get; set; }
        [Column("Album_SummaryEndDate")]
        public long? AlbumSummaryEndDate { get; set; }
        [Column("Album_Source")]
        public long? AlbumSource { get; set; }
        [Column("Album_SourceId")]
        public long? AlbumSourceId { get; set; }
        [Column("Album_PublishState")]
        public long? AlbumPublishState { get; set; }
        [Column("Album_PendingTelemetryUploadState")]
        public long? AlbumPendingTelemetryUploadState { get; set; }
        [Column("Album_SentTelemetryUploadState")]
        public long? AlbumSentTelemetryUploadState { get; set; }
        [Column("Album_ETag")]
        public string AlbumEtag { get; set; }
        [Column("Album_CreationType")]
        public long? AlbumCreationType { get; set; }
        [Column("Album_Order")]
        public long? AlbumOrder { get; set; }

        [ForeignKey(nameof(AlbumCoverItemId))]
        [InverseProperty(nameof(Item.Albums))]
        public virtual Item AlbumCoverItem { get; set; }
        [ForeignKey(nameof(AlbumSourceId))]
        [InverseProperty(nameof(Source.Albums))]
        public virtual Source AlbumSourceNavigation { get; set; }
        [InverseProperty("ProjectAlbum")]
        public virtual Project Project { get; set; }
        [InverseProperty("RemoteAlbumAlbum")]
        public virtual RemoteAlbum RemoteAlbum { get; set; }
        [InverseProperty(nameof(CloudAlbum.CloudAlbumAlbum))]
        public virtual ICollection<CloudAlbum> CloudAlbums { get; set; }
        [InverseProperty(nameof(ExcludedAlbum.ExcludedAlbumAlbum))]
        public virtual ICollection<ExcludedAlbum> ExcludedAlbums { get; set; }
        [InverseProperty(nameof(UserActionAlbumView.UserActionAlbumViewAlbum))]
        public virtual ICollection<UserActionAlbumView> UserActionAlbumViews { get; set; }
        [InverseProperty(nameof(UserActionSlideshow.UserActionSlideshowAlbum))]
        public virtual ICollection<UserActionSlideshow> UserActionSlideshows { get; set; }
    }
}

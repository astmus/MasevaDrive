using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Album
    {
        public Album()
        {
            CloudAlbums = new HashSet<CloudAlbum>();
            ExcludedAlbums = new HashSet<ExcludedAlbum>();
            UserActionAlbumViews = new HashSet<UserActionAlbumView>();
            UserActionSlideshows = new HashSet<UserActionSlideshow>();
        }

        public long AlbumId { get; set; }
        public string AlbumName { get; set; }
        public long AlbumType { get; set; }
        public long AlbumState { get; set; }
        public long AlbumQueryType { get; set; }
        public long AlbumQueryBoundsType { get; set; }
        public string AlbumQuery { get; set; }
        public long AlbumDateCreated { get; set; }
        public long? AlbumDateUpdated { get; set; }
        public long? AlbumDateUserModified { get; set; }
        public long? AlbumDateViewed { get; set; }
        public long? AlbumDateShared { get; set; }
        public long AlbumCount { get; set; }
        public long? AlbumCoverItemId { get; set; }
        public double? AlbumCoverBoundsLeft { get; set; }
        public double? AlbumCoverBoundsTop { get; set; }
        public double? AlbumCoverBoundsRight { get; set; }
        public double? AlbumCoverBoundsBottom { get; set; }
        public long AlbumVisibility { get; set; }
        public long? AlbumEventStartDate { get; set; }
        public long? AlbumEventEndDate { get; set; }
        public long? AlbumSummaryStartDate { get; set; }
        public long? AlbumSummaryEndDate { get; set; }
        public long? AlbumSource { get; set; }
        public long? AlbumSourceId { get; set; }
        public long? AlbumPublishState { get; set; }
        public long? AlbumPendingTelemetryUploadState { get; set; }
        public long? AlbumSentTelemetryUploadState { get; set; }
        public string AlbumEtag { get; set; }
        public long? AlbumCreationType { get; set; }
        public long? AlbumOrder { get; set; }

        public virtual Item AlbumCoverItem { get; set; }
        public virtual Source AlbumSourceNavigation { get; set; }
        public virtual Project Project { get; set; }
        public virtual RemoteAlbum RemoteAlbum { get; set; }
        public virtual ICollection<CloudAlbum> CloudAlbums { get; set; }
        public virtual ICollection<ExcludedAlbum> ExcludedAlbums { get; set; }
        public virtual ICollection<UserActionAlbumView> UserActionAlbumViews { get; set; }
        public virtual ICollection<UserActionSlideshow> UserActionSlideshows { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class RemoteAlbum
    {
        public long RemoteAlbumAlbumId { get; set; }
        public string RemoteAlbumRemoteId { get; set; }
        public long? RemoteAlbumPresentAtSync { get; set; }
        public string RemoteAlbumGenericViewUrl { get; set; }
        public string RemoteAlbumCoverDuringUpload { get; set; }
        public long? RemoteAlbumAlbumType { get; set; }
        public string RemoteAlbumPhotosCloudId { get; set; }
        public string RemoteAlbumEtag { get; set; }

        public virtual Album RemoteAlbumAlbum { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class CloudAlbum
    {
        public long CloudAlbumId { get; set; }
        public long CloudAlbumAlbumId { get; set; }
        public string CloudAlbumCloudId { get; set; }
        public long? CloudAlbumCloudAlbumDefinitionId { get; set; }

        public virtual Album CloudAlbumAlbum { get; set; }
        public virtual CloudAlbumDefinition CloudAlbumCloudAlbumDefinition { get; set; }
    }
}

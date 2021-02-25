using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class CloudAlbumDefinition
    {
        public CloudAlbumDefinition()
        {
            CloudAlbums = new HashSet<CloudAlbum>();
        }

        public long CloudAlbumDefinitionId { get; set; }
        public string CloudAlbumDefinitionCloudId { get; set; }
        public string CloudAlbumDefinitionCloudQuery { get; set; }
        public string CloudAlbumDefinitionCloudFriendlyName { get; set; }
        public long? CloudAlbumDefinitionDateLastAlbumsMaintenance { get; set; }
        public long? CloudAlbumDefinitionQueryType { get; set; }

        public virtual ICollection<CloudAlbum> CloudAlbums { get; set; }
    }
}

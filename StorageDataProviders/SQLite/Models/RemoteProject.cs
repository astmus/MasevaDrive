using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class RemoteProject
    {
        public long RemoteProjectId { get; set; }
        public string RemoteProjectProjectGuid { get; set; }
        public string RemoteProjectPhotosCloudId { get; set; }
        public long RemoteProjectPublishState { get; set; }
        public long? RemoteProjectDateLastSynced { get; set; }
        public string RemoteProjectEtag { get; set; }
        public long RemoteProjectMigratedFromCloud { get; set; }

        public virtual Project RemoteProjectProjectGu { get; set; }
    }
}

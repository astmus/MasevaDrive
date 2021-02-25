using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Project
    {
        public Project()
        {
            RemoteProjects = new HashSet<RemoteProject>();
        }

        public long ProjectId { get; set; }
        public long? ProjectAlbumId { get; set; }
        public string ProjectGuid { get; set; }
        public string ProjectName { get; set; }
        public string ProjectRpmState { get; set; }
        public string ProjectAgmState { get; set; }
        public long ProjectDateCreated { get; set; }
        public long? ProjectDuration { get; set; }
        public byte[] ProjectStoryBuilderProjectState { get; set; }

        public virtual Album ProjectAlbum { get; set; }
        public virtual ICollection<RemoteProject> RemoteProjects { get; set; }
    }
}

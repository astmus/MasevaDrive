using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class FaceCluster
    {
        public FaceCluster()
        {
            ExcludedFaces = new HashSet<ExcludedFace>();
            Faces = new HashSet<Face>();
        }

        public long FaceClusterId { get; set; }
        public long? FaceClusterPersonId { get; set; }
        public long? FaceClusterBestFaceId { get; set; }

        public virtual Face FaceClusterBestFace { get; set; }
        public virtual Person FaceClusterPerson { get; set; }
        public virtual ICollection<ExcludedFace> ExcludedFaces { get; set; }
        public virtual ICollection<Face> Faces { get; set; }
    }
}

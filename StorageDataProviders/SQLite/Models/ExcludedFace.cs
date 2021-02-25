using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ExcludedFace
    {
        public long ExcludedFaceId { get; set; }
        public long ExcludedFaceFaceClusterId { get; set; }
        public long ExcludedFaceFaceId { get; set; }
        public long ExcludedFaceExcludedForUse { get; set; }
        public long ExcludedFaceExcludedDate { get; set; }

        public virtual Face ExcludedFaceFace { get; set; }
        public virtual FaceCluster ExcludedFaceFaceCluster { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class FaceFeature
    {
        public long FaceFeatureFaceId { get; set; }
        public long FaceFeatureFeatureType { get; set; }
        public double? FaceFeatureX { get; set; }
        public double? FaceFeatureY { get; set; }

        public virtual Face FaceFeatureFace { get; set; }
    }
}

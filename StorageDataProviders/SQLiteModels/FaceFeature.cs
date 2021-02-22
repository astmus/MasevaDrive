using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("FaceFeature")]
    public partial class FaceFeature
    {
        [Key]
        [Column("FaceFeature_FaceId")]
        public long FaceFeatureFaceId { get; set; }
        [Key]
        [Column("FaceFeature_FeatureType")]
        public long FaceFeatureFeatureType { get; set; }
        [Column("FaceFeature_X")]
        public double? FaceFeatureX { get; set; }
        [Column("FaceFeature_Y")]
        public double? FaceFeatureY { get; set; }

        [ForeignKey(nameof(FaceFeatureFaceId))]
        [InverseProperty(nameof(Face.FaceFeatures))]
        public virtual Face FaceFeatureFace { get; set; }
    }
}

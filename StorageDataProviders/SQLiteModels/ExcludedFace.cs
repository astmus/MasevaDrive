using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ExcludedFace")]
    [Index(nameof(ExcludedFaceFaceClusterId), Name = "ExcludedFace_FaceClusterId")]
    [Index(nameof(ExcludedFaceFaceId), Name = "ExcludedFace_FaceId")]
    public partial class ExcludedFace
    {
        [Key]
        [Column("ExcludedFace_Id")]
        public long ExcludedFaceId { get; set; }
        [Column("ExcludedFace_FaceClusterId")]
        public long ExcludedFaceFaceClusterId { get; set; }
        [Column("ExcludedFace_FaceId")]
        public long ExcludedFaceFaceId { get; set; }
        [Column("ExcludedFace_ExcludedForUse")]
        public long ExcludedFaceExcludedForUse { get; set; }
        [Column("ExcludedFace_ExcludedDate")]
        public long ExcludedFaceExcludedDate { get; set; }

        [ForeignKey(nameof(ExcludedFaceFaceId))]
        [InverseProperty(nameof(Face.ExcludedFaces))]
        public virtual Face ExcludedFaceFace { get; set; }
        [ForeignKey(nameof(ExcludedFaceFaceClusterId))]
        [InverseProperty(nameof(FaceCluster.ExcludedFaces))]
        public virtual FaceCluster ExcludedFaceFaceCluster { get; set; }
    }
}

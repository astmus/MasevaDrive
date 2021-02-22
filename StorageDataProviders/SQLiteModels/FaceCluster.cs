using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("FaceCluster")]
    [Index(nameof(FaceClusterBestFaceId), Name = "FaceCluster_BestFaceId")]
    [Index(nameof(FaceClusterPersonId), Name = "FaceCluster_PersonId")]
    public partial class FaceCluster
    {
        public FaceCluster()
        {
            ExcludedFaces = new HashSet<ExcludedFace>();
            Faces = new HashSet<Face>();
        }

        [Key]
        [Column("FaceCluster_Id")]
        public long FaceClusterId { get; set; }
        [Column("FaceCluster_PersonId")]
        public long? FaceClusterPersonId { get; set; }
        [Column("FaceCluster_BestFaceId")]
        public long? FaceClusterBestFaceId { get; set; }

        [ForeignKey(nameof(FaceClusterBestFaceId))]
        [InverseProperty(nameof(Face.FaceClusters))]
        public virtual Face FaceClusterBestFace { get; set; }
        [ForeignKey(nameof(FaceClusterPersonId))]
        [InverseProperty(nameof(Person.FaceClusters))]
        public virtual Person FaceClusterPerson { get; set; }
        [InverseProperty(nameof(ExcludedFace.ExcludedFaceFaceCluster))]
        public virtual ICollection<ExcludedFace> ExcludedFaces { get; set; }
        [InverseProperty(nameof(Face.FaceFaceCluster))]
        public virtual ICollection<Face> Faces { get; set; }
    }
}

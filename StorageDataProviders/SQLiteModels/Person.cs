using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Person")]
    [Index(nameof(PersonBestFaceId), Name = "Person_BestFaceId")]
    [Index(nameof(PersonName), Name = "Person_Name")]
    [Index(nameof(PersonRank), Name = "Person_Rank")]
    [Index(nameof(PersonRecalcBestFace), Name = "Person_RecalcBestFace")]
    [Index(nameof(PersonRecalcRank), Name = "Person_RecalcRank")]
    [Index(nameof(PersonSafeBestFaceId), Name = "Person_SafeBestFaceId")]
    public partial class Person
    {
        public Person()
        {
            ExcludedPeople = new HashSet<ExcludedPerson>();
            FaceClusters = new HashSet<FaceCluster>();
            Faces = new HashSet<Face>();
        }

        [Key]
        [Column("Person_Id")]
        public long PersonId { get; set; }
        [Column("Person_CID")]
        public long? PersonCid { get; set; }
        [Column("Person_BestFaceId")]
        public long? PersonBestFaceId { get; set; }
        [Column("Person_SafeBestFaceId")]
        public long? PersonSafeBestFaceId { get; set; }
        [Column("Person_ServiceId")]
        public long? PersonServiceId { get; set; }
        [Column("Person_Name")]
        public string PersonName { get; set; }
        [Column("Person_SourceAndId")]
        public string PersonSourceAndId { get; set; }
        [Column("Person_ItemCount")]
        public long? PersonItemCount { get; set; }
        [Column("Person_EmailDigest")]
        public byte[] PersonEmailDigest { get; set; }
        [Column("Person_RepresentativeThumbStream")]
        public byte[] PersonRepresentativeThumbStream { get; set; }
        [Column("Person_Rank")]
        public long? PersonRank { get; set; }
        [Column("Person_RecalcBestFace")]
        public long? PersonRecalcBestFace { get; set; }
        [Column("Person_RecalcRank")]
        public long? PersonRecalcRank { get; set; }

        [ForeignKey(nameof(PersonBestFaceId))]
        [InverseProperty(nameof(Face.PersonPersonBestFaces))]
        public virtual Face PersonBestFace { get; set; }
        [ForeignKey(nameof(PersonSafeBestFaceId))]
        [InverseProperty(nameof(Face.PersonPersonSafeBestFaces))]
        public virtual Face PersonSafeBestFace { get; set; }
        [InverseProperty(nameof(ExcludedPerson.ExcludedPersonPerson))]
        public virtual ICollection<ExcludedPerson> ExcludedPeople { get; set; }
        [InverseProperty(nameof(FaceCluster.FaceClusterPerson))]
        public virtual ICollection<FaceCluster> FaceClusters { get; set; }
        [InverseProperty(nameof(Face.FacePerson))]
        public virtual ICollection<Face> Faces { get; set; }
    }
}

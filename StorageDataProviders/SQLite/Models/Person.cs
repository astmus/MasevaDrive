using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Person
    {
        public Person()
        {
            ExcludedPeople = new HashSet<ExcludedPerson>();
            FaceClusters = new HashSet<FaceCluster>();
            Faces = new HashSet<Face>();
        }

        public long PersonId { get; set; }
        public long? PersonCid { get; set; }
        public long? PersonBestFaceId { get; set; }
        public long? PersonSafeBestFaceId { get; set; }
        public long? PersonServiceId { get; set; }
        public string PersonName { get; set; }
        public string PersonSourceAndId { get; set; }
        public long? PersonItemCount { get; set; }
        public byte[] PersonEmailDigest { get; set; }
        public byte[] PersonRepresentativeThumbStream { get; set; }
        public long? PersonRank { get; set; }
        public long? PersonRecalcBestFace { get; set; }
        public long? PersonRecalcRank { get; set; }

        public virtual Face PersonBestFace { get; set; }
        public virtual Face PersonSafeBestFace { get; set; }
        public virtual ICollection<ExcludedPerson> ExcludedPeople { get; set; }
        public virtual ICollection<FaceCluster> FaceClusters { get; set; }
        public virtual ICollection<Face> Faces { get; set; }
    }
}

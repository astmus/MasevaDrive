using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ExcludedPerson")]
    [Index(nameof(ExcludedPersonExcludedForUse), Name = "ExcludedPerson_ExcludedForUse")]
    [Index(nameof(ExcludedPersonPersonId), Name = "ExcludedPerson_PersonId")]
    public partial class ExcludedPerson
    {
        [Key]
        [Column("ExcludedPerson_Id")]
        public long ExcludedPersonId { get; set; }
        [Column("ExcludedPerson_PersonId")]
        public long ExcludedPersonPersonId { get; set; }
        [Column("ExcludedPerson_ExcludedForUse")]
        public long ExcludedPersonExcludedForUse { get; set; }
        [Column("ExcludedPerson_ExcludedDate")]
        public long ExcludedPersonExcludedDate { get; set; }

        [ForeignKey(nameof(ExcludedPersonPersonId))]
        [InverseProperty(nameof(Person.ExcludedPeople))]
        public virtual Person ExcludedPersonPerson { get; set; }
    }
}

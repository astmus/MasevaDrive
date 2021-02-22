using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ExcludedLocation")]
    [Index(nameof(ExcludedLocationExcludedForUse), Name = "ExcludedLocation_ExcludedForUse")]
    [Index(nameof(ExcludedLocationLocationId), Name = "ExcludedLocation_LocationId")]
    public partial class ExcludedLocation
    {
        [Key]
        [Column("ExcludedLocation_Id")]
        public long ExcludedLocationId { get; set; }
        [Column("ExcludedLocation_LocationId")]
        public long ExcludedLocationLocationId { get; set; }
        [Column("ExcludedLocation_ExcludedForUse")]
        public long ExcludedLocationExcludedForUse { get; set; }
        [Column("ExcludedLocation_ExcludedDate")]
        public long ExcludedLocationExcludedDate { get; set; }

        [ForeignKey(nameof(ExcludedLocationLocationId))]
        [InverseProperty(nameof(Location.ExcludedLocations))]
        public virtual Location ExcludedLocationLocation { get; set; }
    }
}

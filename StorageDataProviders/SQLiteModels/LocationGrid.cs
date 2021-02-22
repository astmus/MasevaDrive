using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("LocationGrid")]
    [Index(nameof(LocationGridLatitude), nameof(LocationGridLongitude), Name = "LocationGrid_LatLong")]
    public partial class LocationGrid
    {
        [Key]
        [Column("LocationGrid_Id")]
        public long LocationGridId { get; set; }
        [Column("LocationGrid_Latitude")]
        public double? LocationGridLatitude { get; set; }
        [Column("LocationGrid_Longitude")]
        public double? LocationGridLongitude { get; set; }
        [Column("LocationGrid_LocationId")]
        public long? LocationGridLocationId { get; set; }
        [Column("LocationGrid_ErrorCount")]
        public long? LocationGridErrorCount { get; set; }
        [Column("LocationGrid_LastRun")]
        public long? LocationGridLastRun { get; set; }

        [ForeignKey(nameof(LocationGridLocationId))]
        [InverseProperty(nameof(Location.LocationGrids))]
        public virtual Location LocationGridLocation { get; set; }
    }
}

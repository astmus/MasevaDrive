using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("LocationCountry")]
    [Index(nameof(LocationCountryName), Name = "LocationCountry_Name")]
    public partial class LocationCountry
    {
        public LocationCountry()
        {
            LocationRegions = new HashSet<LocationRegion>();
            Locations = new HashSet<Location>();
        }

        [Key]
        [Column("LocationCountry_Id")]
        public long LocationCountryId { get; set; }
        [Column("LocationCountry_Name")]
        public string LocationCountryName { get; set; }

        [InverseProperty(nameof(LocationRegion.LocationRegionLocationCountry))]
        public virtual ICollection<LocationRegion> LocationRegions { get; set; }
        [InverseProperty(nameof(Location.LocationLocationCountry))]
        public virtual ICollection<Location> Locations { get; set; }
    }
}

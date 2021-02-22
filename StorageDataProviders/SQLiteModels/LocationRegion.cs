using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("LocationRegion")]
    [Index(nameof(LocationRegionName), Name = "LocationRegion_Name")]
    public partial class LocationRegion
    {
        public LocationRegion()
        {
            LocationDistricts = new HashSet<LocationDistrict>();
            Locations = new HashSet<Location>();
        }

        [Key]
        [Column("LocationRegion_Id")]
        public long LocationRegionId { get; set; }
        [Column("LocationRegion_Name")]
        public string LocationRegionName { get; set; }
        [Column("LocationRegion_LocationCountryId")]
        public long? LocationRegionLocationCountryId { get; set; }

        [ForeignKey(nameof(LocationRegionLocationCountryId))]
        [InverseProperty(nameof(LocationCountry.LocationRegions))]
        public virtual LocationCountry LocationRegionLocationCountry { get; set; }
        [InverseProperty(nameof(LocationDistrict.LocationDistrictLocationRegion))]
        public virtual ICollection<LocationDistrict> LocationDistricts { get; set; }
        [InverseProperty(nameof(Location.LocationLocationRegion))]
        public virtual ICollection<Location> Locations { get; set; }
    }
}

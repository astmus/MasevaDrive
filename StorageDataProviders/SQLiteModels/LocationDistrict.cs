using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("LocationDistrict")]
    [Index(nameof(LocationDistrictName), Name = "LocationDistrict_Name")]
    public partial class LocationDistrict
    {
        public LocationDistrict()
        {
            Locations = new HashSet<Location>();
        }

        [Key]
        [Column("LocationDistrict_Id")]
        public long LocationDistrictId { get; set; }
        [Column("LocationDistrict_Name")]
        public string LocationDistrictName { get; set; }
        [Column("LocationDistrict_LocationRegionId")]
        public long? LocationDistrictLocationRegionId { get; set; }

        [ForeignKey(nameof(LocationDistrictLocationRegionId))]
        [InverseProperty(nameof(LocationRegion.LocationDistricts))]
        public virtual LocationRegion LocationDistrictLocationRegion { get; set; }
        [InverseProperty(nameof(Location.LocationLocationDistrict))]
        public virtual ICollection<Location> Locations { get; set; }
    }
}

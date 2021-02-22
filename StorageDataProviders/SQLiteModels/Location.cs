using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Location")]
    [Index(nameof(LocationCoverItemId), Name = "Location_CoverItemId")]
    [Index(nameof(LocationItemsCountExcDupes), Name = "Location_ItemsCountExcDupes")]
    [Index(nameof(LocationLocationCountryId), Name = "Location_LocationCountryId")]
    [Index(nameof(LocationLocationDistrictId), Name = "Location_LocationDistrictId")]
    [Index(nameof(LocationLocationRegionId), Name = "Location_LocationRegionId")]
    [Index(nameof(LocationName), Name = "Location_Name")]
    public partial class Location
    {
        public Location()
        {
            ExcludedLocations = new HashSet<ExcludedLocation>();
            Items = new HashSet<Item>();
            LocationGrids = new HashSet<LocationGrid>();
        }

        [Key]
        [Column("Location_Id")]
        public long LocationId { get; set; }
        [Column("Location_Name")]
        public string LocationName { get; set; }
        [Column("Location_LocationRegionId")]
        public long? LocationLocationRegionId { get; set; }
        [Column("Location_LocationDistrictId")]
        public long? LocationLocationDistrictId { get; set; }
        [Column("Location_LocationCountryId")]
        public long? LocationLocationCountryId { get; set; }
        [Column("Location_ItemsCountExcDupes")]
        public long? LocationItemsCountExcDupes { get; set; }
        [Column("Location_CoverItemId")]
        public long? LocationCoverItemId { get; set; }

        [ForeignKey(nameof(LocationCoverItemId))]
        [InverseProperty(nameof(Item.Locations))]
        public virtual Item LocationCoverItem { get; set; }
        [ForeignKey(nameof(LocationLocationCountryId))]
        [InverseProperty(nameof(LocationCountry.Locations))]
        public virtual LocationCountry LocationLocationCountry { get; set; }
        [ForeignKey(nameof(LocationLocationDistrictId))]
        [InverseProperty(nameof(LocationDistrict.Locations))]
        public virtual LocationDistrict LocationLocationDistrict { get; set; }
        [ForeignKey(nameof(LocationLocationRegionId))]
        [InverseProperty(nameof(LocationRegion.Locations))]
        public virtual LocationRegion LocationLocationRegion { get; set; }
        [InverseProperty(nameof(ExcludedLocation.ExcludedLocationLocation))]
        public virtual ICollection<ExcludedLocation> ExcludedLocations { get; set; }
        [InverseProperty(nameof(Item.ItemLocation))]
        public virtual ICollection<Item> Items { get; set; }
        [InverseProperty(nameof(LocationGrid.LocationGridLocation))]
        public virtual ICollection<LocationGrid> LocationGrids { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Location
    {
        public Location()
        {
            ExcludedLocations = new HashSet<ExcludedLocation>();
            Items = new HashSet<Item>();
            LocationGrids = new HashSet<LocationGrid>();
        }

        public long LocationId { get; set; }
        public string LocationName { get; set; }
        public long? LocationLocationRegionId { get; set; }
        public long? LocationLocationDistrictId { get; set; }
        public long? LocationLocationCountryId { get; set; }
        public long? LocationItemsCountExcDupes { get; set; }
        public long? LocationCoverItemId { get; set; }

        public virtual Item LocationCoverItem { get; set; }
        public virtual LocationCountry LocationLocationCountry { get; set; }
        public virtual LocationDistrict LocationLocationDistrict { get; set; }
        public virtual LocationRegion LocationLocationRegion { get; set; }
        public virtual ICollection<ExcludedLocation> ExcludedLocations { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<LocationGrid> LocationGrids { get; set; }
    }
}

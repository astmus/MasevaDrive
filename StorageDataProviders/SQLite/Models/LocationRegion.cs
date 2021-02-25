using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class LocationRegion
    {
        public LocationRegion()
        {
            LocationDistricts = new HashSet<LocationDistrict>();
            Locations = new HashSet<Location>();
        }

        public long LocationRegionId { get; set; }
        public string LocationRegionName { get; set; }
        public long? LocationRegionLocationCountryId { get; set; }

        public virtual LocationCountry LocationRegionLocationCountry { get; set; }
        public virtual ICollection<LocationDistrict> LocationDistricts { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}

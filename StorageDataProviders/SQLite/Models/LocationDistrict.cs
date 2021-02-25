using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class LocationDistrict
    {
        public LocationDistrict()
        {
            Locations = new HashSet<Location>();
        }

        public long LocationDistrictId { get; set; }
        public string LocationDistrictName { get; set; }
        public long? LocationDistrictLocationRegionId { get; set; }

        public virtual LocationRegion LocationDistrictLocationRegion { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}

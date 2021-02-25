using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class LocationCountry
    {
        public LocationCountry()
        {
            LocationRegions = new HashSet<LocationRegion>();
            Locations = new HashSet<Location>();
        }

        public long LocationCountryId { get; set; }
        public string LocationCountryName { get; set; }

        public virtual ICollection<LocationRegion> LocationRegions { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}

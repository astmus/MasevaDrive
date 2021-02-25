using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class LocationGrid
    {
        public long LocationGridId { get; set; }
        public double? LocationGridLatitude { get; set; }
        public double? LocationGridLongitude { get; set; }
        public long? LocationGridLocationId { get; set; }
        public long? LocationGridErrorCount { get; set; }
        public long? LocationGridLastRun { get; set; }

        public virtual Location LocationGridLocation { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ExcludedLocation
    {
        public long ExcludedLocationId { get; set; }
        public long ExcludedLocationLocationId { get; set; }
        public long ExcludedLocationExcludedForUse { get; set; }
        public long ExcludedLocationExcludedDate { get; set; }

        public virtual Location ExcludedLocationLocation { get; set; }
    }
}

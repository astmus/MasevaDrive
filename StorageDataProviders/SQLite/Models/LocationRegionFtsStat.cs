﻿using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class LocationRegionFtsStat
    {
        public long Id { get; set; }
        public byte[] Value { get; set; }
    }
}

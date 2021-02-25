﻿using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class PersonFtsSegdir
    {
        public long Level { get; set; }
        public long Idx { get; set; }
        public long? StartBlock { get; set; }
        public long? LeavesEndBlock { get; set; }
        public long? EndBlock { get; set; }
        public byte[] Root { get; set; }
    }
}

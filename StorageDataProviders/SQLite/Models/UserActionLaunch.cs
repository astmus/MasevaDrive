using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class UserActionLaunch
    {
        public long UserActionLaunchId { get; set; }
        public long UserActionLaunchDate { get; set; }
        public long UserActionLaunchEntryPoint { get; set; }
    }
}

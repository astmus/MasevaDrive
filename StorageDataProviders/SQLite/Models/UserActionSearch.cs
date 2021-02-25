using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class UserActionSearch
    {
        public long UserActionSearchId { get; set; }
        public long UserActionSearchDate { get; set; }
        public string UserActionSearchJson { get; set; }
        public string UserActionSearchTextbox { get; set; }
        public long UserActionSearchActionOrigin { get; set; }
        public long UserActionSearchRequestOrigin { get; set; }
        public long UserActionSearchNumberOfResults { get; set; }
        public long UserActionSearchIndexingWasComplete { get; set; }
    }
}

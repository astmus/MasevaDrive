using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class UserActionImport
    {
        public long UserActionImportId { get; set; }
        public long UserActionImportDate { get; set; }
        public long UserActionImportSessionId { get; set; }
        public string UserActionImportDestination { get; set; }
        public long UserActionImportActionOrigin { get; set; }
        public string UserActionImportManufacturer { get; set; }
        public string UserActionImportModel { get; set; }
        public long UserActionImportTotalCount { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ExcludedImport
    {
        public long ExcludedImportId { get; set; }
        public long ExcludedImportImportId { get; set; }
        public long ExcludedImportExcludedForUse { get; set; }
        public long ExcludedImportExcludedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ExcludedTag
    {
        public long ExcludedTagId { get; set; }
        public long ExcludedTagTagId { get; set; }
        public long ExcludedTagExcludedForUse { get; set; }
        public long ExcludedTagExcludedDate { get; set; }

        public virtual Tag ExcludedTagTag { get; set; }
    }
}

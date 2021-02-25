using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ExcludedItemTag
    {
        public long ExcludedItemTagId { get; set; }
        public long ExcludedItemTagItemId { get; set; }
        public long ExcludedItemTagTagId { get; set; }
        public long ExcludedItemTagExcludedForUse { get; set; }
        public long ExcludedItemTagExcludedDate { get; set; }
        public long? ExcludedItemTagConceptModelVersion { get; set; }
        public long? ExcludedItemTagUploadState { get; set; }
        public long? ExcludedItemTagUploadAttempts { get; set; }
        public long? ExcludedItemTagUploadDateLastAttempt { get; set; }

        public virtual Item ExcludedItemTagItem { get; set; }
        public virtual Tag ExcludedItemTagTag { get; set; }
    }
}

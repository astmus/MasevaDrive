using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class SearchAnalysisItemPriority
    {
        public long SearchAnalysisItemPriorityId { get; set; }
        public long SearchAnalysisItemPriorityItemId { get; set; }
        public long SearchAnalysisItemPriorityPriority { get; set; }

        public virtual Item SearchAnalysisItemPriorityItem { get; set; }
    }
}

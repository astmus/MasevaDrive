using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class PinnedSearch
    {
        public long PinnedSearchId { get; set; }
        public long PinnedSearchPinnedDate { get; set; }
        public string PinnedSearchSearchText { get; set; }
    }
}

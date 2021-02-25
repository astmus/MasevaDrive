using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Cache
    {
        public long CacheId { get; set; }
        public long? CacheItemId { get; set; }
        public string CacheFilename { get; set; }
        public long? CacheDateAccessed { get; set; }
        public long? CacheModificationVersion { get; set; }

        public virtual Item CacheItem { get; set; }
    }
}

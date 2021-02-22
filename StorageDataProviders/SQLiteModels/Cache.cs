using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Cache")]
    [Index(nameof(CacheItemId), IsUnique = true)]
    [Index(nameof(CacheDateAccessed), Name = "Cache_DateAccessed")]
    [Index(nameof(CacheFilename), Name = "Cache_Filename")]
    [Index(nameof(CacheItemId), Name = "Cache_ItemId")]
    public partial class Cache
    {
        [Key]
        [Column("Cache_Id")]
        public long CacheId { get; set; }
        [Column("Cache_ItemId")]
        public long? CacheItemId { get; set; }
        [Column("Cache_Filename")]
        public string CacheFilename { get; set; }
        [Column("Cache_DateAccessed")]
        public long? CacheDateAccessed { get; set; }
        [Column("Cache_ModificationVersion")]
        public long? CacheModificationVersion { get; set; }

        [ForeignKey(nameof(CacheItemId))]
        [InverseProperty(nameof(Item.Cache))]
        public virtual Item CacheItem { get; set; }
    }
}

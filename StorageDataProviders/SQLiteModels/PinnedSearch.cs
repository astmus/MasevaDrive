using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("PinnedSearch")]
    [Index(nameof(PinnedSearchPinnedDate), Name = "PinnedSearch_PinnedDate")]
    public partial class PinnedSearch
    {
        [Key]
        [Column("PinnedSearch_Id")]
        public long PinnedSearchId { get; set; }
        [Column("PinnedSearch_PinnedDate")]
        public long PinnedSearchPinnedDate { get; set; }
        [Column("PinnedSearch_SearchText")]
        public string PinnedSearchSearchText { get; set; }
    }
}

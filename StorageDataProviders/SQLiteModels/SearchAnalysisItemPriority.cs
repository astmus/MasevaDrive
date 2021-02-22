using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("SearchAnalysisItemPriority")]
    [Index(nameof(SearchAnalysisItemPriorityItemId), Name = "SearchAnalysisItemPriority_ItemId", IsUnique = true)]
    public partial class SearchAnalysisItemPriority
    {
        [Key]
        [Column("SearchAnalysisItemPriority_Id")]
        public long SearchAnalysisItemPriorityId { get; set; }
        [Column("SearchAnalysisItemPriority_ItemId")]
        public long SearchAnalysisItemPriorityItemId { get; set; }
        [Column("SearchAnalysisItemPriority_Priority")]
        public long SearchAnalysisItemPriorityPriority { get; set; }

        [ForeignKey(nameof(SearchAnalysisItemPriorityItemId))]
        [InverseProperty(nameof(Item.SearchAnalysisItemPriority))]
        public virtual Item SearchAnalysisItemPriorityItem { get; set; }
    }
}

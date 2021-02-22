using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ItemEngineStatus")]
    [Index(nameof(ItemEngineStatusItemId), Name = "ItemEngineStatus_ItemId", IsUnique = true)]
    [Index(nameof(ItemEngineStatusLastRun), Name = "ItemEngineStatus_LastRun")]
    public partial class ItemEngineStatus
    {
        [Key]
        [Column("ItemEngineStatus_Id")]
        public long ItemEngineStatusId { get; set; }
        [Column("ItemEngineStatus_ItemId")]
        public long ItemEngineStatusItemId { get; set; }
        [Column("ItemEngineStatus_Status")]
        public long? ItemEngineStatusStatus { get; set; }
        [Column("ItemEngineStatus_ErrorCode")]
        public long? ItemEngineStatusErrorCode { get; set; }
        [Column("ItemEngineStatus_ErrorString")]
        public string ItemEngineStatusErrorString { get; set; }
        [Column("ItemEngineStatus_RetryCount")]
        public long? ItemEngineStatusRetryCount { get; set; }
        [Column("ItemEngineStatus_Version")]
        public string ItemEngineStatusVersion { get; set; }
        [Column("ItemEngineStatus_LastRun")]
        public long? ItemEngineStatusLastRun { get; set; }
        [Column("ItemEngineStatus_LastFrameAnalyzed")]
        public long? ItemEngineStatusLastFrameAnalyzed { get; set; }
        [Column("ItemEngineStatus_PartialVideoVersion")]
        public string ItemEngineStatusPartialVideoVersion { get; set; }
        [Column("ItemEngineStatus_AnalysisDone")]
        public long ItemEngineStatusAnalysisDone { get; set; }

        [ForeignKey(nameof(ItemEngineStatusItemId))]
        [InverseProperty(nameof(Item.ItemEngineStatus))]
        public virtual Item ItemEngineStatusItem { get; set; }
    }
}

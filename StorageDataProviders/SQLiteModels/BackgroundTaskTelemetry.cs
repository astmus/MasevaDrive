using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    [Table("BackgroundTaskTelemetry")]
    public partial class BackgroundTaskTelemetry
    {
        [Column("BackgroundTaskTelemetry_Id")]
        public long BackgroundTaskTelemetryId { get; set; }
        [Column("BackgroundTaskTelemetry_State")]
        public long BackgroundTaskTelemetryState { get; set; }
        [Column("BackgroundTaskTelemetry_Reason")]
        public long BackgroundTaskTelemetryReason { get; set; }
        [Column("BackgroundTaskTelemetry_Count")]
        public long BackgroundTaskTelemetryCount { get; set; }
        [Column("BackgroundTaskTelemetry_TotalTime")]
        public long? BackgroundTaskTelemetryTotalTime { get; set; }
        [Column("BackgroundTaskTelemetry_MinTime")]
        public long? BackgroundTaskTelemetryMinTime { get; set; }
        [Column("BackgroundTaskTelemetry_MaxTime")]
        public long? BackgroundTaskTelemetryMaxTime { get; set; }
        [Required]
        [Column("BackgroundTaskTelemetry_CorrelationGuid")]
        public string BackgroundTaskTelemetryCorrelationGuid { get; set; }
    }
}

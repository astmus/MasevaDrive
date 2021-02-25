using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class BackgroundTaskTelemetry
    {
        public long BackgroundTaskTelemetryId { get; set; }
        public long BackgroundTaskTelemetryState { get; set; }
        public long BackgroundTaskTelemetryReason { get; set; }
        public long BackgroundTaskTelemetryCount { get; set; }
        public long? BackgroundTaskTelemetryTotalTime { get; set; }
        public long? BackgroundTaskTelemetryMinTime { get; set; }
        public long? BackgroundTaskTelemetryMaxTime { get; set; }
        public string BackgroundTaskTelemetryCorrelationGuid { get; set; }
    }
}

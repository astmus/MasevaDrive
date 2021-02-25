using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class NetworkTelemetry
    {
        public long NetworkTelemetrySource { get; set; }
        public long NetworkTelemetryRequestType { get; set; }
        public long NetworkTelemetryIsBackgroundTaskHost { get; set; }
        public long NetworkTelemetryTotalCount { get; set; }
        public long NetworkTelemetryByteSize { get; set; }
    }
}

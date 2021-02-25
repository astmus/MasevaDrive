using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class AppTelemetryState
    {
        public string AppTelemetryStateEventName { get; set; }
        public long? AppTelemetryStateEventFireTime { get; set; }
    }
}

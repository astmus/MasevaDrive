using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    [Table("AppTelemetryState")]
    public partial class AppTelemetryState
    {
        [Column("AppTelemetryState_EventName")]
        public string AppTelemetryStateEventName { get; set; }
        [Column("AppTelemetryState_EventFireTime")]
        public long? AppTelemetryStateEventFireTime { get; set; }
    }
}

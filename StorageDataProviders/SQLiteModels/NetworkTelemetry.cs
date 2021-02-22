using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("NetworkTelemetry")]
    public partial class NetworkTelemetry
    {
        [Key]
        [Column("NetworkTelemetry_Source")]
        public long NetworkTelemetrySource { get; set; }
        [Key]
        [Column("NetworkTelemetry_RequestType")]
        public long NetworkTelemetryRequestType { get; set; }
        [Key]
        [Column("NetworkTelemetry_IsBackgroundTaskHost")]
        public long NetworkTelemetryIsBackgroundTaskHost { get; set; }
        [Column("NetworkTelemetry_TotalCount")]
        public long NetworkTelemetryTotalCount { get; set; }
        [Column("NetworkTelemetry_ByteSize")]
        public long NetworkTelemetryByteSize { get; set; }
    }
}

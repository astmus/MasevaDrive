using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class DbRecoveryTaskState
    {
        public long DbRecoveryTaskStateId { get; set; }
        public string DbRecoveryTaskStateTaskName { get; set; }
        public long DbRecoveryTaskStateLastRun { get; set; }
        public string DbRecoveryTaskStateStatePayload { get; set; }
    }
}

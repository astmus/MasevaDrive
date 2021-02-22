using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("DbRecoveryTaskState")]
    public partial class DbRecoveryTaskState
    {
        [Key]
        [Column("DbRecoveryTaskState_Id")]
        public long DbRecoveryTaskStateId { get; set; }
        [Required]
        [Column("DbRecoveryTaskState_TaskName")]
        public string DbRecoveryTaskStateTaskName { get; set; }
        [Column("DbRecoveryTaskState_LastRun")]
        public long DbRecoveryTaskStateLastRun { get; set; }
        [Column("DbRecoveryTaskState_StatePayload")]
        public string DbRecoveryTaskStateStatePayload { get; set; }
    }
}

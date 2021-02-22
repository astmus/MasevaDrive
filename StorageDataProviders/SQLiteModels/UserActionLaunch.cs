using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("UserActionLaunch")]
    public partial class UserActionLaunch
    {
        [Key]
        [Column("UserActionLaunch_Id")]
        public long UserActionLaunchId { get; set; }
        [Column("UserActionLaunch_Date")]
        public long UserActionLaunchDate { get; set; }
        [Column("UserActionLaunch_EntryPoint")]
        public long UserActionLaunchEntryPoint { get; set; }
    }
}

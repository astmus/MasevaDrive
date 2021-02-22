using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("UserActionImport")]
    public partial class UserActionImport
    {
        [Key]
        [Column("UserActionImport_Id")]
        public long UserActionImportId { get; set; }
        [Column("UserActionImport_Date")]
        public long UserActionImportDate { get; set; }
        [Column("UserActionImport_SessionId")]
        public long UserActionImportSessionId { get; set; }
        [Required]
        [Column("UserActionImport_Destination")]
        public string UserActionImportDestination { get; set; }
        [Column("UserActionImport_ActionOrigin")]
        public long UserActionImportActionOrigin { get; set; }
        [Required]
        [Column("UserActionImport_Manufacturer")]
        public string UserActionImportManufacturer { get; set; }
        [Required]
        [Column("UserActionImport_Model")]
        public string UserActionImportModel { get; set; }
        [Column("UserActionImport_TotalCount")]
        public long UserActionImportTotalCount { get; set; }
    }
}

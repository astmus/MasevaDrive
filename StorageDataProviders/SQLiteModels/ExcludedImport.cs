using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ExcludedImport")]
    [Index(nameof(ExcludedImportExcludedForUse), Name = "ExcludedImport_ExcludedForUse")]
    public partial class ExcludedImport
    {
        [Key]
        [Column("ExcludedImport_Id")]
        public long ExcludedImportId { get; set; }
        [Column("ExcludedImport_ImportId")]
        public long ExcludedImportImportId { get; set; }
        [Column("ExcludedImport_ExcludedForUse")]
        public long ExcludedImportExcludedForUse { get; set; }
        [Column("ExcludedImport_ExcludedDate")]
        public long ExcludedImportExcludedDate { get; set; }
    }
}

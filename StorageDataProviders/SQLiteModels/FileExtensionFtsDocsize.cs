using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("FileExtensionFts_docsize")]
    public partial class FileExtensionFtsDocsize
    {
        [Key]
        [Column("docid")]
        public long Docid { get; set; }
        [Column("size")]
        public byte[] Size { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("FilenameFts_segdir")]
    public partial class FilenameFtsSegdir
    {
        [Key]
        [Column("level")]
        public long Level { get; set; }
        [Key]
        [Column("idx")]
        public long Idx { get; set; }
        [Column("start_block")]
        public long? StartBlock { get; set; }
        [Column("leaves_end_block")]
        public long? LeavesEndBlock { get; set; }
        [Column("end_block")]
        public long? EndBlock { get; set; }
        [Column("root")]
        public byte[] Root { get; set; }
    }
}

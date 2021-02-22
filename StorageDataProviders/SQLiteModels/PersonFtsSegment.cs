using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("PersonFts_segments")]
    public partial class PersonFtsSegment
    {
        [Key]
        [Column("blockid")]
        public long Blockid { get; set; }
        [Column("block")]
        public byte[] Block { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("LocationRegionFts_segments")]
    public partial class LocationRegionFtsSegment
    {
        [Key]
        [Column("blockid")]
        public long Blockid { get; set; }
        [Column("block")]
        public byte[] Block { get; set; }
    }
}
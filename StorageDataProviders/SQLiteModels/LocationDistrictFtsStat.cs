using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("LocationDistrictFts_stat")]
    public partial class LocationDistrictFtsStat
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("value")]
        public byte[] Value { get; set; }
    }
}

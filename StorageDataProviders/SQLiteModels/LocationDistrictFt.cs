using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    public partial class LocationDistrictFt
    {
        [Column("LocationDistrict_Name")]
        public byte[] LocationDistrictName { get; set; }
    }
}

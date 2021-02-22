using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    public partial class LocationCountryFt
    {
        [Column("LocationCountry_Name")]
        public byte[] LocationCountryName { get; set; }
    }
}

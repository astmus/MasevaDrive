using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    public partial class PersonFt
    {
        [Column("Person_Name")]
        public byte[] PersonName { get; set; }
    }
}

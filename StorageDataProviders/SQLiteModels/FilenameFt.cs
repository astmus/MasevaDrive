using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    public partial class FilenameFt
    {
        [Column("Item_Filename")]
        public byte[] ItemFilename { get; set; }
    }
}

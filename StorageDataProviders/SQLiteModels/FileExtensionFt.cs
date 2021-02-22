using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    public partial class FileExtensionFt
    {
        [Column("Item_FileExtension")]
        public byte[] ItemFileExtension { get; set; }
    }
}

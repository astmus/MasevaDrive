using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    public partial class FolderNameFt
    {
        [Column("Folder_DisplayName")]
        public byte[] FolderDisplayName { get; set; }
    }
}

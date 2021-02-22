using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    [Table("OCRItemTextViewFts")]
    public partial class OcritemTextViewFt
    {
        [Column("OCRItemTextView_Text")]
        public byte[] OcritemTextViewText { get; set; }
    }
}

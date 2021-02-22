using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    public partial class OcritemTextView
    {
        [Column("rowid")]
        public long? Rowid { get; set; }
        [Column("OCRItemTextView_Text")]
        public byte[] OcritemTextViewText { get; set; }
    }
}

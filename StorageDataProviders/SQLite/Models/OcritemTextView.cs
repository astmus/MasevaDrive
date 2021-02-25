using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class OcritemTextView
    {
        public long? Rowid { get; set; }
        public byte[] OcritemTextViewText { get; set; }
    }
}

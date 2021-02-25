using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class OcritemTextViewFtsDocsize
    {
        public long Docid { get; set; }
        public byte[] Size { get; set; }
    }
}

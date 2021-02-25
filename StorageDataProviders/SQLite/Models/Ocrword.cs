using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Ocrword
    {
        public long OcrwordId { get; set; }
        public long OcrwordOcrlineId { get; set; }
        public long OcrwordIndexOnLine { get; set; }
        public string OcrwordText { get; set; }
        public double OcrwordHeight { get; set; }
        public double OcrwordWidth { get; set; }
        public double OcrwordX { get; set; }
        public double OcrwordY { get; set; }

        public virtual Ocrline OcrwordOcrline { get; set; }
    }
}

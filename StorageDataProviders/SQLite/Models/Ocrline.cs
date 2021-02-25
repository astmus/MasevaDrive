using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Ocrline
    {
        public Ocrline()
        {
            Ocrwords = new HashSet<Ocrword>();
        }

        public long OcrlineId { get; set; }
        public long OcrlineOcritemId { get; set; }
        public long OcrlineIndexOnItem { get; set; }

        public virtual Ocritem OcrlineOcritem { get; set; }
        public virtual ICollection<Ocrword> Ocrwords { get; set; }
    }
}

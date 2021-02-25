using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Ocritem
    {
        public Ocritem()
        {
            Ocrlines = new HashSet<Ocrline>();
        }

        public long OcritemId { get; set; }
        public long OcritemItemId { get; set; }
        public double OcritemTextAngle { get; set; }

        public virtual Item OcritemItem { get; set; }
        public virtual ICollection<Ocrline> Ocrlines { get; set; }
    }
}

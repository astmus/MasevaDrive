using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class SalientRect
    {
        public long SalientRectId { get; set; }
        public long SalientRectItemId { get; set; }
        public double? SalientRectRectLeft { get; set; }
        public double? SalientRectRectTop { get; set; }
        public double? SalientRectRectWidth { get; set; }
        public double? SalientRectRectHeight { get; set; }
        public double? SalientRectSharpness { get; set; }
        public long? SalientRectContainsFaces { get; set; }
        public long? SalientRectIsFaceUnionRect { get; set; }

        public virtual Item SalientRectItem { get; set; }
    }
}

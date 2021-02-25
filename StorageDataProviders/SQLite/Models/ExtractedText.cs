using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ExtractedText
    {
        public long ExtractedTextId { get; set; }
        public long ExtractedTextItemId { get; set; }
        public string ExtractedTextText { get; set; }

        public virtual Item ExtractedTextItem { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class UserActionPrint
    {
        public long UserActionPrintId { get; set; }
        public long UserActionPrintDate { get; set; }
        public long? UserActionPrintItemId { get; set; }
        public long UserActionPrintActionOrigin { get; set; }

        public virtual Item UserActionPrintItem { get; set; }
    }
}

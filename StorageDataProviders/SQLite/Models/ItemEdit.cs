using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ItemEdit
    {
        public long ItemEditItemId { get; set; }
        public long? ItemEditEditTypeId { get; set; }
        public long? ItemEditEditDate { get; set; }

        public virtual Item ItemEditItem { get; set; }
    }
}

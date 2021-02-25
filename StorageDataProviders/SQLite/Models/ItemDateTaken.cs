using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ItemDateTaken
    {
        public long ItemDateTakenItemId { get; set; }
        public long? ItemDateTakenYear { get; set; }
        public long? ItemDateTakenMonth { get; set; }
        public long? ItemDateTakenDay { get; set; }
        public long? ItemDateTakenDayOfWeek { get; set; }
    }
}

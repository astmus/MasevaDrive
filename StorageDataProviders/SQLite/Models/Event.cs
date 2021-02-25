using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Event
    {
        public Event()
        {
            Items = new HashSet<Item>();
        }

        public long EventId { get; set; }
        public long? EventStartDate { get; set; }
        public long? EventEndDate { get; set; }
        public long? EventSize { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}

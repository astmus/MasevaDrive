using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ApplicationName
    {
        public ApplicationName()
        {
            Items = new HashSet<Item>();
        }

        public long ApplicationNameId { get; set; }
        public string ApplicationNameText { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}

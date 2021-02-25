using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class CameraModel
    {
        public CameraModel()
        {
            Items = new HashSet<Item>();
        }

        public long CameraModelId { get; set; }
        public string CameraModelText { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}

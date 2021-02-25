using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class CameraManufacturer
    {
        public CameraManufacturer()
        {
            Items = new HashSet<Item>();
        }

        public long CameraManufacturerId { get; set; }
        public string CameraManufacturerText { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}

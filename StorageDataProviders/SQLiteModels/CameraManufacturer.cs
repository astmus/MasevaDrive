using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("CameraManufacturer")]
    [Index(nameof(CameraManufacturerText), Name = "CameraManufacturer_Text")]
    public partial class CameraManufacturer
    {
        public CameraManufacturer()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        [Column("CameraManufacturer_Id")]
        public long CameraManufacturerId { get; set; }
        [Column("CameraManufacturer_Text")]
        public string CameraManufacturerText { get; set; }

        [InverseProperty(nameof(Item.ItemCameraManufacturer))]
        public virtual ICollection<Item> Items { get; set; }
    }
}

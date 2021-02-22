using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("CameraModel")]
    [Index(nameof(CameraModelText), Name = "CameraModel_Text")]
    public partial class CameraModel
    {
        public CameraModel()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        [Column("CameraModel_Id")]
        public long CameraModelId { get; set; }
        [Column("CameraModel_Text")]
        public string CameraModelText { get; set; }

        [InverseProperty(nameof(Item.ItemCameraModel))]
        public virtual ICollection<Item> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ApplicationName")]
    [Index(nameof(ApplicationNameText), Name = "ApplicationName_Text")]
    public partial class ApplicationName
    {
        public ApplicationName()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        [Column("ApplicationName_Id")]
        public long ApplicationNameId { get; set; }
        [Column("ApplicationName_Text")]
        public string ApplicationNameText { get; set; }

        [InverseProperty(nameof(Item.ItemApplicationName))]
        public virtual ICollection<Item> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Event")]
    [Index(nameof(EventStartDate), Name = "Event_StartDate", IsUnique = true)]
    public partial class Event
    {
        public Event()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        [Column("Event_Id")]
        public long EventId { get; set; }
        [Column("Event_StartDate")]
        public long? EventStartDate { get; set; }
        [Column("Event_EndDate")]
        public long? EventEndDate { get; set; }
        [Column("Event_Size")]
        public long? EventSize { get; set; }

        [InverseProperty(nameof(Item.ItemEvent))]
        public virtual ICollection<Item> Items { get; set; }
    }
}

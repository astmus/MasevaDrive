using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("OCRItem")]
    [Index(nameof(OcritemItemId), Name = "OCRItem_ItemId")]
    public partial class Ocritem
    {
        public Ocritem()
        {
            Ocrlines = new HashSet<Ocrline>();
        }

        [Key]
        [Column("OCRItem_Id")]
        public long OcritemId { get; set; }
        [Column("OCRItem_ItemId")]
        public long OcritemItemId { get; set; }
        [Column("OCRItem_TextAngle")]
        public double OcritemTextAngle { get; set; }

        [ForeignKey(nameof(OcritemItemId))]
        [InverseProperty(nameof(Item.Ocritems))]
        public virtual Item OcritemItem { get; set; }
        [InverseProperty(nameof(Ocrline.OcrlineOcritem))]
        public virtual ICollection<Ocrline> Ocrlines { get; set; }
    }
}

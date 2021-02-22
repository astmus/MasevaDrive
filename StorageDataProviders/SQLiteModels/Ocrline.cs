using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("OCRLine")]
    [Index(nameof(OcrlineOcritemId), Name = "OCRLine_OCRItemId")]
    public partial class Ocrline
    {
        public Ocrline()
        {
            Ocrwords = new HashSet<Ocrword>();
        }

        [Key]
        [Column("OCRLine_Id")]
        public long OcrlineId { get; set; }
        [Column("OCRLine_OCRItemId")]
        public long OcrlineOcritemId { get; set; }
        [Column("OCRLine_IndexOnItem")]
        public long OcrlineIndexOnItem { get; set; }

        [ForeignKey(nameof(OcrlineOcritemId))]
        [InverseProperty(nameof(Ocritem.Ocrlines))]
        public virtual Ocritem OcrlineOcritem { get; set; }
        [InverseProperty(nameof(Ocrword.OcrwordOcrline))]
        public virtual ICollection<Ocrword> Ocrwords { get; set; }
    }
}

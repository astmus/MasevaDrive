using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("OCRWord")]
    [Index(nameof(OcrwordOcrlineId), Name = "OCRWord_OCRLineId")]
    [Index(nameof(OcrwordText), Name = "OCRWord_Text")]
    public partial class Ocrword
    {
        [Key]
        [Column("OCRWord_Id")]
        public long OcrwordId { get; set; }
        [Column("OCRWord_OCRLineId")]
        public long OcrwordOcrlineId { get; set; }
        [Column("OCRWord_IndexOnLine")]
        public long OcrwordIndexOnLine { get; set; }
        [Required]
        [Column("OCRWord_Text")]
        public string OcrwordText { get; set; }
        [Column("OCRWord_Height")]
        public double OcrwordHeight { get; set; }
        [Column("OCRWord_Width")]
        public double OcrwordWidth { get; set; }
        [Column("OCRWord_X")]
        public double OcrwordX { get; set; }
        [Column("OCRWord_Y")]
        public double OcrwordY { get; set; }

        [ForeignKey(nameof(OcrwordOcrlineId))]
        [InverseProperty(nameof(Ocrline.Ocrwords))]
        public virtual Ocrline OcrwordOcrline { get; set; }
    }
}

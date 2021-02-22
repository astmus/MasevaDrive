using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("SalientRect")]
    [Index(nameof(SalientRectItemId), Name = "SalientRect_ItemId")]
    public partial class SalientRect
    {
        [Key]
        [Column("SalientRect_Id")]
        public long SalientRectId { get; set; }
        [Column("SalientRect_ItemId")]
        public long SalientRectItemId { get; set; }
        [Column("SalientRect_Rect_Left")]
        public double? SalientRectRectLeft { get; set; }
        [Column("SalientRect_Rect_Top")]
        public double? SalientRectRectTop { get; set; }
        [Column("SalientRect_Rect_Width")]
        public double? SalientRectRectWidth { get; set; }
        [Column("SalientRect_Rect_Height")]
        public double? SalientRectRectHeight { get; set; }
        [Column("SalientRect_Sharpness")]
        public double? SalientRectSharpness { get; set; }
        [Column("SalientRect_ContainsFaces")]
        public long? SalientRectContainsFaces { get; set; }
        [Column("SalientRect_IsFaceUnionRect")]
        public long? SalientRectIsFaceUnionRect { get; set; }

        [ForeignKey(nameof(SalientRectItemId))]
        [InverseProperty(nameof(Item.SalientRects))]
        public virtual Item SalientRectItem { get; set; }
    }
}

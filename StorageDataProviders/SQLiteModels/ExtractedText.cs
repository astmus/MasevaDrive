using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ExtractedText")]
    [Index(nameof(ExtractedTextItemId), Name = "ExtractedText_ItemId")]
    public partial class ExtractedText
    {
        [Key]
        [Column("ExtractedText_Id")]
        public long ExtractedTextId { get; set; }
        [Column("ExtractedText_ItemId")]
        public long ExtractedTextItemId { get; set; }
        [Column("ExtractedText_Text")]
        public string ExtractedTextText { get; set; }

        [ForeignKey(nameof(ExtractedTextItemId))]
        [InverseProperty(nameof(Item.ExtractedTexts))]
        public virtual Item ExtractedTextItem { get; set; }
    }
}

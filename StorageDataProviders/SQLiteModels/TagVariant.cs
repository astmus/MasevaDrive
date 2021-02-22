using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("TagVariant")]
    [Index(nameof(TagVariantTagResourceId), Name = "TagVariant_TagResourceId")]
    [Index(nameof(TagVariantText), Name = "TagVariant_Text")]
    public partial class TagVariant
    {
        [Key]
        [Column("TagVariant_Id")]
        public long TagVariantId { get; set; }
        [Column("TagVariant_TagResourceId")]
        public long? TagVariantTagResourceId { get; set; }
        [Column("TagVariant_Text")]
        public string TagVariantText { get; set; }
        [Column("TagVariant_IsPrimary")]
        public long? TagVariantIsPrimary { get; set; }
    }
}

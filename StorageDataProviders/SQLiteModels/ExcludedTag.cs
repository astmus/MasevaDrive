using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ExcludedTag")]
    [Index(nameof(ExcludedTagExcludedForUse), Name = "ExcludedTag_ExcludedForUse")]
    [Index(nameof(ExcludedTagTagId), Name = "ExcludedTag_TagId")]
    public partial class ExcludedTag
    {
        [Key]
        [Column("ExcludedTag_Id")]
        public long ExcludedTagId { get; set; }
        [Column("ExcludedTag_TagId")]
        public long ExcludedTagTagId { get; set; }
        [Column("ExcludedTag_ExcludedForUse")]
        public long ExcludedTagExcludedForUse { get; set; }
        [Column("ExcludedTag_ExcludedDate")]
        public long ExcludedTagExcludedDate { get; set; }

        [ForeignKey(nameof(ExcludedTagTagId))]
        [InverseProperty(nameof(Tag.ExcludedTags))]
        public virtual Tag ExcludedTagTag { get; set; }
    }
}

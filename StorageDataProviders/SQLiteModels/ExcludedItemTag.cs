using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ExcludedItemTag")]
    [Index(nameof(ExcludedItemTagItemId), Name = "ExcludedItemTag_ItemId")]
    [Index(nameof(ExcludedItemTagTagId), Name = "ExcludedItemTag_TagId")]
    public partial class ExcludedItemTag
    {
        [Key]
        [Column("ExcludedItemTag_Id")]
        public long ExcludedItemTagId { get; set; }
        [Column("ExcludedItemTag_ItemId")]
        public long ExcludedItemTagItemId { get; set; }
        [Column("ExcludedItemTag_TagId")]
        public long ExcludedItemTagTagId { get; set; }
        [Column("ExcludedItemTag_ExcludedForUse")]
        public long ExcludedItemTagExcludedForUse { get; set; }
        [Column("ExcludedItemTag_ExcludedDate")]
        public long ExcludedItemTagExcludedDate { get; set; }
        [Column("ExcludedItemTag_ConceptModelVersion")]
        public long? ExcludedItemTagConceptModelVersion { get; set; }
        [Column("ExcludedItemTag_UploadState")]
        public long? ExcludedItemTagUploadState { get; set; }
        [Column("ExcludedItemTag_UploadAttempts")]
        public long? ExcludedItemTagUploadAttempts { get; set; }
        [Column("ExcludedItemTag_UploadDateLastAttempt")]
        public long? ExcludedItemTagUploadDateLastAttempt { get; set; }

        [ForeignKey(nameof(ExcludedItemTagItemId))]
        [InverseProperty(nameof(Item.ExcludedItemTags))]
        public virtual Item ExcludedItemTagItem { get; set; }
        [ForeignKey(nameof(ExcludedItemTagTagId))]
        [InverseProperty(nameof(Tag.ExcludedItemTags))]
        public virtual Tag ExcludedItemTagTag { get; set; }
    }
}

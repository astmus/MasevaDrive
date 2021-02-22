using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    [Table("ConceptTagSuppressedTagList")]
    public partial class ConceptTagSuppressedTagList
    {
        [Column("ConceptTagSuppressedTagList_TagResourceId")]
        public long? ConceptTagSuppressedTagListTagResourceId { get; set; }
    }
}

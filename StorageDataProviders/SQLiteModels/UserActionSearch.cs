using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("UserActionSearch")]
    public partial class UserActionSearch
    {
        [Key]
        [Column("UserActionSearch_Id")]
        public long UserActionSearchId { get; set; }
        [Column("UserActionSearch_Date")]
        public long UserActionSearchDate { get; set; }
        [Required]
        [Column("UserActionSearch_Json")]
        public string UserActionSearchJson { get; set; }
        [Required]
        [Column("UserActionSearch_Textbox")]
        public string UserActionSearchTextbox { get; set; }
        [Column("UserActionSearch_ActionOrigin")]
        public long UserActionSearchActionOrigin { get; set; }
        [Column("UserActionSearch_RequestOrigin")]
        public long UserActionSearchRequestOrigin { get; set; }
        [Column("UserActionSearch_NumberOfResults")]
        public long UserActionSearchNumberOfResults { get; set; }
        [Column("UserActionSearch_IndexingWasComplete")]
        public long UserActionSearchIndexingWasComplete { get; set; }
    }
}

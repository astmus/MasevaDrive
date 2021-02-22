using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("CloudAlbumDefinition")]
    [Index(nameof(CloudAlbumDefinitionCloudId), IsUnique = true)]
    [Index(nameof(CloudAlbumDefinitionCloudId), Name = "CloudAlbumDefinition_CloudId")]
    public partial class CloudAlbumDefinition
    {
        public CloudAlbumDefinition()
        {
            CloudAlbums = new HashSet<CloudAlbum>();
        }

        [Key]
        [Column("CloudAlbumDefinition_Id")]
        public long CloudAlbumDefinitionId { get; set; }
        [Column("CloudAlbumDefinition_CloudId")]
        public string CloudAlbumDefinitionCloudId { get; set; }
        [Column("CloudAlbumDefinition_CloudQuery")]
        public string CloudAlbumDefinitionCloudQuery { get; set; }
        [Column("CloudAlbumDefinition_CloudFriendlyName")]
        public string CloudAlbumDefinitionCloudFriendlyName { get; set; }
        [Column("CloudAlbumDefinition_DateLastAlbumsMaintenance")]
        public long? CloudAlbumDefinitionDateLastAlbumsMaintenance { get; set; }
        [Column("CloudAlbumDefinition_QueryType")]
        public long? CloudAlbumDefinitionQueryType { get; set; }

        [InverseProperty(nameof(CloudAlbum.CloudAlbumCloudAlbumDefinition))]
        public virtual ICollection<CloudAlbum> CloudAlbums { get; set; }
    }
}

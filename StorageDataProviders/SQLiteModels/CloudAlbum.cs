using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("CloudAlbum")]
    [Index(nameof(CloudAlbumAlbumId), Name = "CloudAlbum_AlbumId")]
    [Index(nameof(CloudAlbumCloudAlbumDefinitionId), nameof(CloudAlbumAlbumId), Name = "CloudAlbum_CloudAlbumDefinitionIdAlbumId", IsUnique = true)]
    [Index(nameof(CloudAlbumCloudId), Name = "CloudAlbum_CloudId")]
    public partial class CloudAlbum
    {
        [Key]
        [Column("CloudAlbum_Id")]
        public long CloudAlbumId { get; set; }
        [Column("CloudAlbum_AlbumId")]
        public long CloudAlbumAlbumId { get; set; }
        [Column("CloudAlbum_CloudId")]
        public string CloudAlbumCloudId { get; set; }
        [Column("CloudAlbum_CloudAlbumDefinitionId")]
        public long? CloudAlbumCloudAlbumDefinitionId { get; set; }

        [ForeignKey(nameof(CloudAlbumAlbumId))]
        [InverseProperty(nameof(Album.CloudAlbums))]
        public virtual Album CloudAlbumAlbum { get; set; }
        [ForeignKey(nameof(CloudAlbumCloudAlbumDefinitionId))]
        [InverseProperty(nameof(CloudAlbumDefinition.CloudAlbums))]
        public virtual CloudAlbumDefinition CloudAlbumCloudAlbumDefinition { get; set; }
    }
}

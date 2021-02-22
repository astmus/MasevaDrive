using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("RemoteProject")]
    [Index(nameof(RemoteProjectPhotosCloudId), Name = "RemoteProject_PhotosCloudId")]
    [Index(nameof(RemoteProjectProjectGuid), Name = "RemoteProject_ProjectGuid")]
    public partial class RemoteProject
    {
        [Key]
        [Column("RemoteProject_Id")]
        public long RemoteProjectId { get; set; }
        [Required]
        [Column("RemoteProject_ProjectGuid")]
        public string RemoteProjectProjectGuid { get; set; }
        [Column("RemoteProject_PhotosCloudId")]
        public string RemoteProjectPhotosCloudId { get; set; }
        [Column("RemoteProject_PublishState")]
        public long RemoteProjectPublishState { get; set; }
        [Column("RemoteProject_DateLastSynced")]
        public long? RemoteProjectDateLastSynced { get; set; }
        [Column("RemoteProject_ETag")]
        public string RemoteProjectEtag { get; set; }
        [Column("RemoteProject_MigratedFromCloud")]
        public long RemoteProjectMigratedFromCloud { get; set; }

        public virtual Project RemoteProjectProjectGu { get; set; }
    }
}

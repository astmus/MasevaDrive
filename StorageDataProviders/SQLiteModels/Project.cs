using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Project")]
    [Index(nameof(ProjectAlbumId), Name = "Project_AlbumId", IsUnique = true)]
    [Index(nameof(ProjectGuid), Name = "Project_Guid", IsUnique = true)]
    public partial class Project
    {
        public Project()
        {
            RemoteProjects = new HashSet<RemoteProject>();
        }

        [Key]
        [Column("Project_Id")]
        public long ProjectId { get; set; }
        [Column("Project_AlbumId")]
        public long? ProjectAlbumId { get; set; }
        [Required]
        [Column("Project_Guid")]
        public string ProjectGuid { get; set; }
        [Column("Project_Name")]
        public string ProjectName { get; set; }
        [Column("Project_RpmState")]
        public string ProjectRpmState { get; set; }
        [Column("Project_AgmState")]
        public string ProjectAgmState { get; set; }
        [Column("Project_DateCreated")]
        public long ProjectDateCreated { get; set; }
        [Column("Project_Duration")]
        public long? ProjectDuration { get; set; }
        [Column("Project_StoryBuilderProjectState")]
        public byte[] ProjectStoryBuilderProjectState { get; set; }

        [ForeignKey(nameof(ProjectAlbumId))]
        [InverseProperty(nameof(Album.Project))]
        public virtual Album ProjectAlbum { get; set; }
        public virtual ICollection<RemoteProject> RemoteProjects { get; set; }
    }
}

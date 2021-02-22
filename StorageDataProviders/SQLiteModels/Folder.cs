using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Folder")]
    [Index(nameof(FolderDisplayName), Name = "Folder_DisplayName")]
    [Index(nameof(FolderKnownFolderType), Name = "Folder_KnownFolderType")]
    [Index(nameof(FolderParentFolderId), Name = "Folder_ParentFolderId")]
    [Index(nameof(FolderPath), Name = "Folder_Path", IsUnique = true)]
    [Index(nameof(FolderSource), Name = "Folder_Source")]
    [Index(nameof(FolderSourceId), Name = "Folder_SourceId")]
    [Index(nameof(FolderStorageProviderFileId), Name = "Folder_StorageProviderFileId")]
    [Index(nameof(FolderSyncWith), Name = "Folder_SyncWith")]
    public partial class Folder
    {
        public Folder()
        {
            InverseFolderParentFolder = new HashSet<Folder>();
            Items = new HashSet<Item>();
        }

        [Key]
        [Column("Folder_Id")]
        public long FolderId { get; set; }
        [Column("Folder_ParentFolderId")]
        public long? FolderParentFolderId { get; set; }
        [Column("Folder_LibraryRelationship")]
        public long? FolderLibraryRelationship { get; set; }
        [Column("Folder_Source")]
        public long? FolderSource { get; set; }
        [Column("Folder_SourceId")]
        public long? FolderSourceId { get; set; }
        [Column("Folder_Path")]
        public string FolderPath { get; set; }
        [Column("Folder_DisplayName")]
        public string FolderDisplayName { get; set; }
        [Column("Folder_DateCreated")]
        public long? FolderDateCreated { get; set; }
        [Column("Folder_DateModified")]
        public long? FolderDateModified { get; set; }
        [Column("Folder_KnownFolderType")]
        public long? FolderKnownFolderType { get; set; }
        [Column("Folder_SyncWith")]
        public long? FolderSyncWith { get; set; }
        [Column("Folder_StorageProviderFileId")]
        public string FolderStorageProviderFileId { get; set; }
        [Column("Folder_InOneDrivePicturesScope")]
        public long? FolderInOneDrivePicturesScope { get; set; }
        [Column("Folder_ItemCount")]
        public long? FolderItemCount { get; set; }

        [ForeignKey(nameof(FolderParentFolderId))]
        [InverseProperty(nameof(Folder.InverseFolderParentFolder))]
        public virtual Folder FolderParentFolder { get; set; }
        [ForeignKey(nameof(FolderSourceId))]
        [InverseProperty(nameof(Source.Folders))]
        public virtual Source FolderSourceNavigation { get; set; }
        [InverseProperty(nameof(Folder.FolderParentFolder))]
        public virtual ICollection<Folder> InverseFolderParentFolder { get; set; }
        [InverseProperty(nameof(Item.ItemParentFolder))]
        public virtual ICollection<Item> Items { get; set; }
    }
}

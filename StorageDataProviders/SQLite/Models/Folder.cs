using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Folder
    {
        public Folder()
        {
            InverseFolderParentFolder = new HashSet<Folder>();
            Items = new HashSet<Item>();
        }

        public long FolderId { get; set; }
        public long? FolderParentFolderId { get; set; }
        public long? FolderLibraryRelationship { get; set; }
        public long? FolderSource { get; set; }
        public long? FolderSourceId { get; set; }
        public string FolderPath { get; set; }
        public string FolderDisplayName { get; set; }
        public long? FolderDateCreated { get; set; }
        public long? FolderDateModified { get; set; }
        public long? FolderKnownFolderType { get; set; }
        public long? FolderSyncWith { get; set; }
        public string FolderStorageProviderFileId { get; set; }
        public long? FolderInOneDrivePicturesScope { get; set; }
        public long? FolderItemCount { get; set; }

        public virtual Folder FolderParentFolder { get; set; }
        public virtual Source FolderSourceNavigation { get; set; }
        public virtual ICollection<Folder> InverseFolderParentFolder { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class UserActionShare
    {
        public long UserActionShareId { get; set; }
        public long UserActionShareDate { get; set; }
        public long? UserActionShareItemId { get; set; }
        public long UserActionShareActionOrigin { get; set; }
        public string UserActionShareTarget { get; set; }
        public long UserActionShareResult { get; set; }

        public virtual Item UserActionShareItem { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class UserActionView
    {
        public long UserActionViewId { get; set; }
        public long UserActionViewDate { get; set; }
        public long? UserActionViewItemId { get; set; }
        public long UserActionViewActionOrigin { get; set; }

        public virtual Item UserActionViewItem { get; set; }
    }
}

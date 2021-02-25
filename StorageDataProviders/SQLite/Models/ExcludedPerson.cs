using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ExcludedPerson
    {
        public long ExcludedPersonId { get; set; }
        public long ExcludedPersonPersonId { get; set; }
        public long ExcludedPersonExcludedForUse { get; set; }
        public long ExcludedPersonExcludedDate { get; set; }

        public virtual Person ExcludedPersonPerson { get; set; }
    }
}

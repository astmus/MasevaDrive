using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ItemEngineStatus
    {
        public long ItemEngineStatusId { get; set; }
        public long ItemEngineStatusItemId { get; set; }
        public long? ItemEngineStatusStatus { get; set; }
        public long? ItemEngineStatusErrorCode { get; set; }
        public string ItemEngineStatusErrorString { get; set; }
        public long? ItemEngineStatusRetryCount { get; set; }
        public string ItemEngineStatusVersion { get; set; }
        public long? ItemEngineStatusLastRun { get; set; }
        public long? ItemEngineStatusLastFrameAnalyzed { get; set; }
        public string ItemEngineStatusPartialVideoVersion { get; set; }
        public long ItemEngineStatusAnalysisDone { get; set; }

        public virtual Item ItemEngineStatusItem { get; set; }
    }
}

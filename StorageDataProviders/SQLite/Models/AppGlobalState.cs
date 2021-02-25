using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class AppGlobalState
    {
        public long? AppGlobalStateDeferredUpgradeVersion { get; set; }
        public string AppGlobalStateAnalysisVersion { get; set; }
        public long? AppGlobalStateDateLastLocalReconciled { get; set; }
        public long? AppGlobalStateCountLastReconciliationQueryResults { get; set; }
        public long? AppGlobalStateDateLastAlbumsMaintenance { get; set; }
        public long? AppGlobalStateDateLastTagAlbumsMaintenance { get; set; }
        public long? AppGlobalStateDateLastPetAlbumsMaintenance { get; set; }
        public long? AppGlobalStateDateLastWeddingAlbumsMaintenance { get; set; }
        public long? AppGlobalStateLastDateUsedInWeddingAlbumsMaintenance { get; set; }
        public long? AppGlobalStateDateLastSeasonalAlbumsMaintenance { get; set; }
        public long? AppGlobalStateDateLastSmileAlbumsMaintenance { get; set; }
        public long? AppGlobalStateDateLastCountryTripAlbumsMaintenance { get; set; }
        public long? AppGlobalStateDateLastItemDeleted { get; set; }
        public long? AppGlobalStateDateLastCacheCleaned { get; set; }
        public string AppGlobalStateOneDriveDeltaSyncToken { get; set; }
        public long? AppGlobalStateOneDriveFullSyncCompleted { get; set; }
        public string AppGlobalStateOneDriveAlbumDeltaSyncToken { get; set; }
        public long? AppGlobalStateOneDriveKnownFoldersNeedUpgrade { get; set; }
        public long? AppGlobalStateOneDriveItemsResyncing { get; set; }
        public long? AppGlobalStateOneDriveAlbumsResyncing { get; set; }
        public long? AppGlobalStateTruncateWalfilePending { get; set; }
        public long? AppGlobalStateRichMediaGrovelVersion { get; set; }
        public long? AppGlobalStateCurrentAutoEnhanceEnabledState { get; set; }
        public long AppGlobalStateRunDedupWork { get; set; }
        public long? AppGlobalStateOneDriveIdentifyPicturesScope { get; set; }
        public long? AppGlobalStateCachedLocalCollectionSize { get; set; }
        public long? AppGlobalStateNewAlbumsBadgeCount { get; set; }
        public long? AppGlobalStateImportBadgeDisplayState { get; set; }
        public long? AppGlobalStateDateLastLocationLookupReady { get; set; }
        public long? AppGlobalStateDateLastDbAnalyze { get; set; }
        public long? AppGlobalStateDateLastDbVacuum { get; set; }
        public long? AppGlobalStateXboxLiveItemsResyncing { get; set; }
        public long? AppGlobalStateFaceRecognitionConsentDate { get; set; }
        public long? AppGlobalStateExistingItemsSyncStarted { get; set; }
    }
}

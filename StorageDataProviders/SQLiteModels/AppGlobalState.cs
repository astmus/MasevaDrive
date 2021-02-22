using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Keyless]
    [Table("AppGlobalState")]
    public partial class AppGlobalState
    {
        [Column("AppGlobalState_DeferredUpgradeVersion")]
        public long? AppGlobalStateDeferredUpgradeVersion { get; set; }
        [Column("AppGlobalState_AnalysisVersion")]
        public string AppGlobalStateAnalysisVersion { get; set; }
        [Column("AppGlobalState_DateLastLocalReconciled")]
        public long? AppGlobalStateDateLastLocalReconciled { get; set; }
        [Column("AppGlobalState_CountLastReconciliationQueryResults")]
        public long? AppGlobalStateCountLastReconciliationQueryResults { get; set; }
        [Column("AppGlobalState_DateLastAlbumsMaintenance")]
        public long? AppGlobalStateDateLastAlbumsMaintenance { get; set; }
        [Column("AppGlobalState_DateLastTagAlbumsMaintenance")]
        public long? AppGlobalStateDateLastTagAlbumsMaintenance { get; set; }
        [Column("AppGlobalState_DateLastPetAlbumsMaintenance")]
        public long? AppGlobalStateDateLastPetAlbumsMaintenance { get; set; }
        [Column("AppGlobalState_DateLastWeddingAlbumsMaintenance")]
        public long? AppGlobalStateDateLastWeddingAlbumsMaintenance { get; set; }
        [Column("AppGlobalState_LastDateUsedInWeddingAlbumsMaintenance")]
        public long? AppGlobalStateLastDateUsedInWeddingAlbumsMaintenance { get; set; }
        [Column("AppGlobalState_DateLastSeasonalAlbumsMaintenance")]
        public long? AppGlobalStateDateLastSeasonalAlbumsMaintenance { get; set; }
        [Column("AppGlobalState_DateLastSmileAlbumsMaintenance")]
        public long? AppGlobalStateDateLastSmileAlbumsMaintenance { get; set; }
        [Column("AppGlobalState_DateLastCountryTripAlbumsMaintenance")]
        public long? AppGlobalStateDateLastCountryTripAlbumsMaintenance { get; set; }
        [Column("AppGlobalState_DateLastItemDeleted")]
        public long? AppGlobalStateDateLastItemDeleted { get; set; }
        [Column("AppGlobalState_DateLastCacheCleaned")]
        public long? AppGlobalStateDateLastCacheCleaned { get; set; }
        [Column("AppGlobalState_OneDriveDeltaSyncToken")]
        public string AppGlobalStateOneDriveDeltaSyncToken { get; set; }
        [Column("AppGlobalState_OneDriveFullSyncCompleted")]
        public long? AppGlobalStateOneDriveFullSyncCompleted { get; set; }
        [Column("AppGlobalState_OneDriveAlbumDeltaSyncToken")]
        public string AppGlobalStateOneDriveAlbumDeltaSyncToken { get; set; }
        [Column("AppGlobalState_OneDriveKnownFoldersNeedUpgrade")]
        public long? AppGlobalStateOneDriveKnownFoldersNeedUpgrade { get; set; }
        [Column("AppGlobalState_OneDriveItemsResyncing")]
        public long? AppGlobalStateOneDriveItemsResyncing { get; set; }
        [Column("AppGlobalState_OneDriveAlbumsResyncing")]
        public long? AppGlobalStateOneDriveAlbumsResyncing { get; set; }
        [Column("AppGlobalState_TruncateWALFilePending")]
        public long? AppGlobalStateTruncateWalfilePending { get; set; }
        [Column("AppGlobalState_RichMediaGrovelVersion")]
        public long? AppGlobalStateRichMediaGrovelVersion { get; set; }
        [Column("AppGlobalState_CurrentAutoEnhanceEnabledState")]
        public long? AppGlobalStateCurrentAutoEnhanceEnabledState { get; set; }
        [Column("AppGlobalState_RunDedupWork")]
        public long AppGlobalStateRunDedupWork { get; set; }
        [Column("AppGlobalState_OneDriveIdentifyPicturesScope")]
        public long? AppGlobalStateOneDriveIdentifyPicturesScope { get; set; }
        [Column("AppGlobalState_CachedLocalCollectionSize")]
        public long? AppGlobalStateCachedLocalCollectionSize { get; set; }
        [Column("AppGlobalState_NewAlbumsBadgeCount")]
        public long? AppGlobalStateNewAlbumsBadgeCount { get; set; }
        [Column("AppGlobalState_ImportBadgeDisplayState")]
        public long? AppGlobalStateImportBadgeDisplayState { get; set; }
        [Column("AppGlobalState_DateLastLocationLookupReady")]
        public long? AppGlobalStateDateLastLocationLookupReady { get; set; }
        [Column("AppGlobalState_DateLastDbAnalyze")]
        public long? AppGlobalStateDateLastDbAnalyze { get; set; }
        [Column("AppGlobalState_DateLastDbVacuum")]
        public long? AppGlobalStateDateLastDbVacuum { get; set; }
        [Column("AppGlobalState_XboxLiveItemsResyncing")]
        public long? AppGlobalStateXboxLiveItemsResyncing { get; set; }
        [Column("AppGlobalState_FaceRecognitionConsentDate")]
        public long? AppGlobalStateFaceRecognitionConsentDate { get; set; }
        [Column("AppGlobalState_ExistingItemsSyncStarted")]
        public long? AppGlobalStateExistingItemsSyncStarted { get; set; }
    }
}

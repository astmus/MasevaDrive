using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Item
    {
        public Item()
        {
            Albums = new HashSet<Album>();
            ExcludedItemTags = new HashSet<ExcludedItemTag>();
            ExtractedTexts = new HashSet<ExtractedText>();
            Faces = new HashSet<Face>();
            InverseItemBurstPrevItem = new HashSet<Item>();
            ItemTags = new HashSet<ItemTag>();
            ItemVideoQualities = new HashSet<ItemVideoQuality>();
            Locations = new HashSet<Location>();
            Ocritems = new HashSet<Ocritem>();
            SalientRects = new HashSet<SalientRect>();
            UserActionPrints = new HashSet<UserActionPrint>();
            UserActionShares = new HashSet<UserActionShare>();
            UserActionSlideshows = new HashSet<UserActionSlideshow>();
            UserActionViews = new HashSet<UserActionView>();
        }

        public long ItemId { get; set; }
        public long? ItemLibraryRelationship { get; set; }
        public long? ItemSource { get; set; }
        public long? ItemSourceId { get; set; }
        public long? ItemMediaType { get; set; }
        public long? ItemDateTaken { get; set; }
        public long? ItemWidth { get; set; }
        public long? ItemHeight { get; set; }
        public long? ItemEditState { get; set; }
        public long? ItemDateCreated { get; set; }
        public long? ItemDateModified { get; set; }
        public long? ItemExclusiveDateTaken { get; set; }
        public long? ItemSystemThumbnailCacheId { get; set; }
        public long ItemParentFolderId { get; set; }
        
        public string ItemFileName { get; set; }
        public string ItemFileExtension { get; set; }
        public long? ItemFileSize { get; set; }
        public double? ItemLatitude { get; set; }
        public double? ItemLongitude { get; set; }
        public string ItemCaption { get; set; }
        public long? ItemSimpleRating { get; set; }
        public long? ItemDuration { get; set; }
        public double? ItemQualityScore { get; set; }
        public long? ItemBurstClusterNumber { get; set; }
        public long? ItemDateModifiedAtLastBurstRun { get; set; }
        public long? ItemBurstPrevItemId { get; set; }
        public long? ItemBurstChunk { get; set; }
        public long? ItemLocationId { get; set; }
        public long? ItemFlash { get; set; }
        public long? ItemAnalysisVersion { get; set; }
        public long? ItemAnalysisLastRun { get; set; }
        public long? ItemDateModifiedAtLastAnalysisRun { get; set; }
        public long? ItemAnalysisErrorCode { get; set; }
        public long? ItemAnalysisErrorCount { get; set; }
        public long ItemAnalysisQueueState { get; set; }
        public long? ItemApplicationNameId { get; set; }
        public long? ItemCameraManufacturerId { get; set; }
        public long? ItemCameraModelId { get; set; }
        public long? ItemHasAuthor { get; set; }
        public long? ItemHasCopyright { get; set; }
        public long? ItemHasKeywords { get; set; }
        public long? ItemFileWidth { get; set; }
        public long? ItemFileHeight { get; set; }
        public long? ItemNdewidth { get; set; }
        public long? ItemNdeheight { get; set; }
        public long? ItemHashAtLastNdethumbnailGeneration { get; set; }
        public long? ItemEventId { get; set; }
        public long? ItemMetadataExtractedAsOf { get; set; }
        public long? ItemPendingXmpExtractionMask { get; set; }
        public long ItemDateIngested { get; set; }
        public long? ItemDupFinderVersion { get; set; }
        public long? ItemDateDupFinding { get; set; }
        public long? ItemSameAs { get; set; }
        public long? ItemSyncWith { get; set; }
        public long? ItemHasDup { get; set; }
        public long? ItemDupState { get; set; }
        public long? ItemUserSelectedDupId { get; set; }
        public long? ItemUserUnlink { get; set; }
        public string ItemMetadataHash { get; set; }
        public string ItemPixelHash { get; set; }
        public double? ItemCameraSettingFnumber { get; set; }
        public double? ItemCameraSettingFocalLength { get; set; }
        public long? ItemCameraSettingIsospeed { get; set; }
        public double? ItemCameraSettingExposureTime { get; set; }
        public byte[] ItemEditList { get; set; }
        public long? ItemModificationVersion { get; set; }
        public string ItemRichMediaId { get; set; }
        public string ItemRichMediaAppId { get; set; }
        public long? ItemRichMediaLaunchOptions { get; set; }
        public long? ItemRichMediaSlowGrovelPending { get; set; }
        public long? ItemThumbnailPrecacheAttempted { get; set; }
        public long? ItemPendingTelemetryUploadState { get; set; }
        public long? ItemSentTelemetryUploadState { get; set; }
        public long? ItemInAppRotatePending { get; set; }
        public string ItemStorageProviderFileId { get; set; }
        public long? ItemNdethumbnailGenerationErrorCount { get; set; }
        public double? ItemFrameRate { get; set; }
        public long? ItemImportSession { get; set; }
        public long? ItemPendingNde { get; set; }
        public long? ItemRichMediaFileStatus { get; set; }
        public long? ItemUploadPendingState { get; set; }
        public long? ItemUploadAttempts { get; set; }
        public long? ItemUploadRequestTime { get; set; }
        public long? ItemLastUploadAttemptTime { get; set; }
        public string ItemEtag { get; set; }
        public long ItemRewriteSupplementaryPropertiesNeeded { get; set; }
        public long? ItemLastEditDate { get; set; }
        public long? ItemIsInked { get; set; }
        public long? ItemIsExportedMovie { get; set; }
        public string ItemOnlineContentAttributionString { get; set; }

        public virtual ApplicationName ItemApplicationName { get; set; }
        public virtual Item ItemBurstPrevItem { get; set; }
        public virtual CameraManufacturer ItemCameraManufacturer { get; set; }
        public virtual CameraModel ItemCameraModel { get; set; }
        public virtual Event ItemEvent { get; set; }
        public virtual Location ItemLocation { get; set; }
        public virtual Folder ItemParentFolder { get; set; }
        public virtual Source ItemSourceNavigation { get; set; }
        public virtual Cache Cache { get; set; }
        public virtual ImageAnalysis ImageAnalysis { get; set; }
        public virtual ItemEngineExemplar ItemEngineExemplar { get; set; }
        public virtual ItemEngineStatus ItemEngineStatus { get; set; }
        public virtual LiveTile LiveTile { get; set; }
        public virtual SearchAnalysisItemPriority SearchAnalysisItemPriority { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<ExcludedItemTag> ExcludedItemTags { get; set; }
        public virtual ICollection<ExtractedText> ExtractedTexts { get; set; }
        public virtual ICollection<Face> Faces { get; set; }
        public virtual ICollection<Item> InverseItemBurstPrevItem { get; set; }
        public virtual ICollection<ItemTag> ItemTags { get; set; }
        public virtual ICollection<ItemVideoQuality> ItemVideoQualities { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Ocritem> Ocritems { get; set; }
        public virtual ICollection<SalientRect> SalientRects { get; set; }
        public virtual ICollection<UserActionPrint> UserActionPrints { get; set; }
        public virtual ICollection<UserActionShare> UserActionShares { get; set; }
        public virtual ICollection<UserActionSlideshow> UserActionSlideshows { get; set; }
        public virtual ICollection<UserActionView> UserActionViews { get; set; }
    }
}

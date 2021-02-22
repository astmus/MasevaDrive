using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Item")]
    [Index(nameof(ItemAnalysisQueueState), Name = "Item_AnalysisQueueState")]
    [Index(nameof(ItemBurstChunk), Name = "Item_BurstChunk")]
    [Index(nameof(ItemBurstClusterNumber), Name = "Item_BurstClusterNumber")]
    [Index(nameof(ItemBurstPrevItemId), Name = "Item_BurstPrevItemId")]
    [Index(nameof(ItemDateCreated), Name = "Item_DateCreated")]
    [Index(nameof(ItemDateDupFinding), Name = "Item_DateDupFinding")]
    [Index(nameof(ItemDateIngested), Name = "Item_DateIngested")]
    [Index(nameof(ItemDateTaken), Name = "Item_DateTaken")]
    [Index(nameof(ItemEditState), Name = "Item_EditState")]
    [Index(nameof(ItemEventId), Name = "Item_EventId")]
    [Index(nameof(ItemFileExtension), Name = "Item_FileExtension")]
    [Index(nameof(ItemFileName), Name = "Item_FileName")]
    [Index(nameof(ItemLibraryRelationship), Name = "Item_LibraryRelationship")]
    [Index(nameof(ItemLocationId), Name = "Item_LocationId")]
    [Index(nameof(ItemMediaType), Name = "Item_MediaType")]
    [Index(nameof(ItemMetadataHash), Name = "Item_MetadataHash")]
    [Index(nameof(ItemParentFolderId), nameof(ItemFileName), Name = "Item_ParentFolderIdFileName")]
    [Index(nameof(ItemRewriteSupplementaryPropertiesNeeded), Name = "Item_RewriteSupplementaryPropertiesNeeded")]
    [Index(nameof(ItemRichMediaSlowGrovelPending), Name = "Item_RichMediaSlowGrovelPending")]
    [Index(nameof(ItemSameAs), Name = "Item_SameAs")]
    [Index(nameof(ItemSource), Name = "Item_Source")]
    [Index(nameof(ItemSourceId), Name = "Item_SourceId")]
    [Index(nameof(ItemStorageProviderFileId), Name = "Item_StorageProviderFileId")]
    [Index(nameof(ItemSyncWith), Name = "Item_SyncWith")]
    [Index(nameof(ItemUserSelectedDupId), Name = "Item_UserSelectedDupId")]
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

        [Key]
        [Column("Item_Id")]
        public long ItemId { get; set; }
        [Column("Item_LibraryRelationship")]
        public long? ItemLibraryRelationship { get; set; }
        [Column("Item_Source")]
        public long? ItemSource { get; set; }
        [Column("Item_SourceId")]
        public long? ItemSourceId { get; set; }
        [Column("Item_MediaType")]
        public long? ItemMediaType { get; set; }
        [Column("Item_DateTaken")]
        public long? ItemDateTaken { get; set; }
        [Column("Item_Width")]
        public long? ItemWidth { get; set; }
        [Column("Item_Height")]
        public long? ItemHeight { get; set; }
        [Column("Item_EditState")]
        public long? ItemEditState { get; set; }
        [Column("Item_DateCreated")]
        public long? ItemDateCreated { get; set; }
        [Column("Item_DateModified")]
        public long? ItemDateModified { get; set; }
        [Column("Item_ExclusiveDateTaken")]
        public long? ItemExclusiveDateTaken { get; set; }
        [Column("Item_SystemThumbnailCacheId")]
        public long? ItemSystemThumbnailCacheId { get; set; }
        [Column("Item_ParentFolderId")]
        public long ItemParentFolderId { get; set; }
        [Column("Item_FileName")]
        public string ItemFileName { get; set; }
        [Column("Item_FileExtension")]
        public string ItemFileExtension { get; set; }
        [Column("Item_FileSize")]
        public long? ItemFileSize { get; set; }
        [Column("Item_Latitude")]
        public double? ItemLatitude { get; set; }
        [Column("Item_Longitude")]
        public double? ItemLongitude { get; set; }
        [Column("Item_Caption")]
        public string ItemCaption { get; set; }
        [Column("Item_SimpleRating")]
        public long? ItemSimpleRating { get; set; }
        [Column("Item_Duration")]
        public long? ItemDuration { get; set; }
        [Column("Item_QualityScore")]
        public double? ItemQualityScore { get; set; }
        [Column("Item_BurstClusterNumber")]
        public long? ItemBurstClusterNumber { get; set; }
        [Column("Item_DateModifiedAtLastBurstRun")]
        public long? ItemDateModifiedAtLastBurstRun { get; set; }
        [Column("Item_BurstPrevItemId")]
        public long? ItemBurstPrevItemId { get; set; }
        [Column("Item_BurstChunk")]
        public long? ItemBurstChunk { get; set; }
        [Column("Item_LocationId")]
        public long? ItemLocationId { get; set; }
        [Column("Item_Flash")]
        public long? ItemFlash { get; set; }
        [Column("Item_AnalysisVersion")]
        public long? ItemAnalysisVersion { get; set; }
        [Column("Item_AnalysisLastRun")]
        public long? ItemAnalysisLastRun { get; set; }
        [Column("Item_DateModifiedAtLastAnalysisRun")]
        public long? ItemDateModifiedAtLastAnalysisRun { get; set; }
        [Column("Item_AnalysisErrorCode")]
        public long? ItemAnalysisErrorCode { get; set; }
        [Column("Item_AnalysisErrorCount")]
        public long? ItemAnalysisErrorCount { get; set; }
        [Column("Item_AnalysisQueueState")]
        public long ItemAnalysisQueueState { get; set; }
        [Column("Item_ApplicationNameId")]
        public long? ItemApplicationNameId { get; set; }
        [Column("Item_CameraManufacturerId")]
        public long? ItemCameraManufacturerId { get; set; }
        [Column("Item_CameraModelId")]
        public long? ItemCameraModelId { get; set; }
        [Column("Item_HasAuthor")]
        public long? ItemHasAuthor { get; set; }
        [Column("Item_HasCopyright")]
        public long? ItemHasCopyright { get; set; }
        [Column("Item_HasKeywords")]
        public long? ItemHasKeywords { get; set; }
        [Column("Item_FileWidth")]
        public long? ItemFileWidth { get; set; }
        [Column("Item_FileHeight")]
        public long? ItemFileHeight { get; set; }
        [Column("Item_NDEWidth")]
        public long? ItemNdewidth { get; set; }
        [Column("Item_NDEHeight")]
        public long? ItemNdeheight { get; set; }
        [Column("Item_HashAtLastNDEThumbnailGeneration")]
        public long? ItemHashAtLastNdethumbnailGeneration { get; set; }
        [Column("Item_EventId")]
        public long? ItemEventId { get; set; }
        [Column("Item_MetadataExtractedAsOf")]
        public long? ItemMetadataExtractedAsOf { get; set; }
        [Column("Item_PendingXmpExtractionMask")]
        public long? ItemPendingXmpExtractionMask { get; set; }
        [Column("Item_DateIngested")]
        public long ItemDateIngested { get; set; }
        [Column("Item_DupFinderVersion")]
        public long? ItemDupFinderVersion { get; set; }
        [Column("Item_DateDupFinding")]
        public long? ItemDateDupFinding { get; set; }
        [Column("Item_SameAs")]
        public long? ItemSameAs { get; set; }
        [Column("Item_SyncWith")]
        public long? ItemSyncWith { get; set; }
        [Column("Item_HasDup")]
        public long? ItemHasDup { get; set; }
        [Column("Item_DupState")]
        public long? ItemDupState { get; set; }
        [Column("Item_UserSelectedDupId")]
        public long? ItemUserSelectedDupId { get; set; }
        [Column("Item_UserUnlink")]
        public long? ItemUserUnlink { get; set; }
        [Column("Item_MetadataHash")]
        public string ItemMetadataHash { get; set; }
        [Column("Item_PixelHash")]
        public string ItemPixelHash { get; set; }
        [Column("Item_CameraSettingFNumber")]
        public double? ItemCameraSettingFnumber { get; set; }
        [Column("Item_CameraSettingFocalLength")]
        public double? ItemCameraSettingFocalLength { get; set; }
        [Column("Item_CameraSettingISOSpeed")]
        public long? ItemCameraSettingIsospeed { get; set; }
        [Column("Item_CameraSettingExposureTime")]
        public double? ItemCameraSettingExposureTime { get; set; }
        [Column("Item_EditList")]
        public byte[] ItemEditList { get; set; }
        [Column("Item_ModificationVersion")]
        public long? ItemModificationVersion { get; set; }
        [Column("Item_RichMediaId")]
        public string ItemRichMediaId { get; set; }
        [Column("Item_RichMediaAppId")]
        public string ItemRichMediaAppId { get; set; }
        [Column("Item_RichMediaLaunchOptions")]
        public long? ItemRichMediaLaunchOptions { get; set; }
        [Column("Item_RichMediaSlowGrovelPending")]
        public long? ItemRichMediaSlowGrovelPending { get; set; }
        [Column("Item_ThumbnailPrecacheAttempted")]
        public long? ItemThumbnailPrecacheAttempted { get; set; }
        [Column("Item_PendingTelemetryUploadState")]
        public long? ItemPendingTelemetryUploadState { get; set; }
        [Column("Item_SentTelemetryUploadState")]
        public long? ItemSentTelemetryUploadState { get; set; }
        [Column("Item_InAppRotatePending")]
        public long? ItemInAppRotatePending { get; set; }
        [Column("Item_StorageProviderFileId")]
        public string ItemStorageProviderFileId { get; set; }
        [Column("Item_NDEThumbnailGenerationErrorCount")]
        public long? ItemNdethumbnailGenerationErrorCount { get; set; }
        [Column("Item_FrameRate")]
        public double? ItemFrameRate { get; set; }
        [Column("Item_ImportSession")]
        public long? ItemImportSession { get; set; }
        [Column("Item_PendingNDE")]
        public long? ItemPendingNde { get; set; }
        [Column("Item_RichMediaFileStatus")]
        public long? ItemRichMediaFileStatus { get; set; }
        [Column("Item_UploadPendingState")]
        public long? ItemUploadPendingState { get; set; }
        [Column("Item_UploadAttempts")]
        public long? ItemUploadAttempts { get; set; }
        [Column("Item_UploadRequestTime")]
        public long? ItemUploadRequestTime { get; set; }
        [Column("Item_LastUploadAttemptTime")]
        public long? ItemLastUploadAttemptTime { get; set; }
        [Column("Item_ETag")]
        public string ItemEtag { get; set; }
        [Column("Item_RewriteSupplementaryPropertiesNeeded")]
        public long ItemRewriteSupplementaryPropertiesNeeded { get; set; }
        [Column("Item_LastEditDate")]
        public long? ItemLastEditDate { get; set; }
        [Column("Item_IsInked")]
        public long? ItemIsInked { get; set; }
        [Column("Item_IsExportedMovie")]
        public long? ItemIsExportedMovie { get; set; }
        [Column("Item_OnlineContentAttributionString")]
        public string ItemOnlineContentAttributionString { get; set; }

        [ForeignKey(nameof(ItemApplicationNameId))]
        [InverseProperty(nameof(ApplicationName.Items))]
        public virtual ApplicationName ItemApplicationName { get; set; }
        [ForeignKey(nameof(ItemBurstPrevItemId))]
        [InverseProperty(nameof(Item.InverseItemBurstPrevItem))]
        public virtual Item ItemBurstPrevItem { get; set; }
        [ForeignKey(nameof(ItemCameraManufacturerId))]
        [InverseProperty(nameof(CameraManufacturer.Items))]
        public virtual CameraManufacturer ItemCameraManufacturer { get; set; }
        [ForeignKey(nameof(ItemCameraModelId))]
        [InverseProperty(nameof(CameraModel.Items))]
        public virtual CameraModel ItemCameraModel { get; set; }
        [ForeignKey(nameof(ItemEventId))]
        [InverseProperty(nameof(Event.Items))]
        public virtual Event ItemEvent { get; set; }
        [ForeignKey(nameof(ItemLocationId))]
        [InverseProperty(nameof(Location.Items))]
        public virtual Location ItemLocation { get; set; }
        [ForeignKey(nameof(ItemParentFolderId))]
        [InverseProperty(nameof(Folder.Items))]
        public virtual Folder ItemParentFolder { get; set; }
        [ForeignKey(nameof(ItemSourceId))]
        [InverseProperty(nameof(Source.Items))]
        public virtual Source ItemSourceNavigation { get; set; }
        [InverseProperty("CacheItem")]
        public virtual Cache Cache { get; set; }
        [InverseProperty("ImageAnalysisItem")]
        public virtual ImageAnalysis ImageAnalysis { get; set; }
        [InverseProperty("ItemEngineExemplarItem")]
        public virtual ItemEngineExemplar ItemEngineExemplar { get; set; }
        [InverseProperty("ItemEngineStatusItem")]
        public virtual ItemEngineStatus ItemEngineStatus { get; set; }
        [InverseProperty("LiveTileItem")]
        public virtual LiveTile LiveTile { get; set; }
        [InverseProperty("SearchAnalysisItemPriorityItem")]
        public virtual SearchAnalysisItemPriority SearchAnalysisItemPriority { get; set; }
        [InverseProperty(nameof(Album.AlbumCoverItem))]
        public virtual ICollection<Album> Albums { get; set; }
        [InverseProperty(nameof(ExcludedItemTag.ExcludedItemTagItem))]
        public virtual ICollection<ExcludedItemTag> ExcludedItemTags { get; set; }
        [InverseProperty(nameof(ExtractedText.ExtractedTextItem))]
        public virtual ICollection<ExtractedText> ExtractedTexts { get; set; }
        [InverseProperty(nameof(Face.FaceItem))]
        public virtual ICollection<Face> Faces { get; set; }
        [InverseProperty(nameof(Item.ItemBurstPrevItem))]
        public virtual ICollection<Item> InverseItemBurstPrevItem { get; set; }
        [InverseProperty(nameof(ItemTag.ItemTagsItem))]
        public virtual ICollection<ItemTag> ItemTags { get; set; }
        [InverseProperty(nameof(ItemVideoQuality.ItemVideoQualityItem))]
        public virtual ICollection<ItemVideoQuality> ItemVideoQualities { get; set; }
        [InverseProperty(nameof(Location.LocationCoverItem))]
        public virtual ICollection<Location> Locations { get; set; }
        [InverseProperty(nameof(Ocritem.OcritemItem))]
        public virtual ICollection<Ocritem> Ocritems { get; set; }
        [InverseProperty(nameof(SalientRect.SalientRectItem))]
        public virtual ICollection<SalientRect> SalientRects { get; set; }
        [InverseProperty(nameof(UserActionPrint.UserActionPrintItem))]
        public virtual ICollection<UserActionPrint> UserActionPrints { get; set; }
        [InverseProperty(nameof(UserActionShare.UserActionShareItem))]
        public virtual ICollection<UserActionShare> UserActionShares { get; set; }
        [InverseProperty(nameof(UserActionSlideshow.UserActionSlideshowItem))]
        public virtual ICollection<UserActionSlideshow> UserActionSlideshows { get; set; }
        [InverseProperty(nameof(UserActionView.UserActionViewItem))]
        public virtual ICollection<UserActionView> UserActionViews { get; set; }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using StorageDataProviders.SQLite.Models;

#nullable disable

namespace StorageDataProviders.Win10
{
    public partial class SQLiteContext : DbContext
    {
        public SQLiteContext()
        {
            int i = 0;
        }

        public SQLiteContext(DbContextOptions<SQLiteContext> options)
            : base(options)
        {
            int i = 0;
        }
		#region Tab
		public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<AlbumItemLink> AlbumItemLinks { get; set; }
        public virtual DbSet<AlbumNameFt> AlbumNameFts { get; set; }
        public virtual DbSet<AlbumNameFtsDocsize> AlbumNameFtsDocsizes { get; set; }
        public virtual DbSet<AlbumNameFtsSegdir> AlbumNameFtsSegdirs { get; set; }
        public virtual DbSet<AlbumNameFtsSegment> AlbumNameFtsSegments { get; set; }
        public virtual DbSet<AlbumNameFtsStat> AlbumNameFtsStats { get; set; }
        public virtual DbSet<AppGlobalState> AppGlobalStates { get; set; }
        public virtual DbSet<AppTelemetryState> AppTelemetryStates { get; set; }
        public virtual DbSet<ApplicationName> ApplicationNames { get; set; }
        public virtual DbSet<Audio> Audios { get; set; }
        public virtual DbSet<BackgroundTaskTelemetry> BackgroundTaskTelemetries { get; set; }
        public virtual DbSet<Cache> Caches { get; set; }
        public virtual DbSet<CameraManufacturer> CameraManufacturers { get; set; }
        public virtual DbSet<CameraModel> CameraModels { get; set; }
        public virtual DbSet<CloudAlbum> CloudAlbums { get; set; }
        public virtual DbSet<CloudAlbumDefinition> CloudAlbumDefinitions { get; set; }
        public virtual DbSet<ConceptTagSuppressedTagList> ConceptTagSuppressedTagLists { get; set; }
        public virtual DbSet<DbRecoveryTaskState> DbRecoveryTaskStates { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<ExcludedAlbum> ExcludedAlbums { get; set; }
        public virtual DbSet<ExcludedFace> ExcludedFaces { get; set; }
        public virtual DbSet<ExcludedImport> ExcludedImports { get; set; }
        public virtual DbSet<ExcludedItemTag> ExcludedItemTags { get; set; }
        public virtual DbSet<ExcludedLocation> ExcludedLocations { get; set; }
        public virtual DbSet<ExcludedPerson> ExcludedPeople { get; set; }
        public virtual DbSet<ExcludedTag> ExcludedTags { get; set; }
        public virtual DbSet<ExtractedText> ExtractedTexts { get; set; }
        public virtual DbSet<Face> Faces { get; set; }
        public virtual DbSet<FaceCluster> FaceClusters { get; set; }
        public virtual DbSet<FaceFeature> FaceFeatures { get; set; }
        public virtual DbSet<FileExtensionFt> FileExtensionFts { get; set; }
        public virtual DbSet<FileExtensionFtsDocsize> FileExtensionFtsDocsizes { get; set; }
        public virtual DbSet<FileExtensionFtsSegdir> FileExtensionFtsSegdirs { get; set; }
        public virtual DbSet<FileExtensionFtsSegment> FileExtensionFtsSegments { get; set; }
        public virtual DbSet<FileExtensionFtsStat> FileExtensionFtsStats { get; set; }
        public virtual DbSet<FilenameFt> FilenameFts { get; set; }
        public virtual DbSet<FilenameFtsDocsize> FilenameFtsDocsizes { get; set; }
        public virtual DbSet<FilenameFtsSegdir> FilenameFtsSegdirs { get; set; }
        public virtual DbSet<FilenameFtsSegment> FilenameFtsSegments { get; set; }
        public virtual DbSet<FilenameFtsStat> FilenameFtsStats { get; set; }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<FolderNameFt> FolderNameFts { get; set; }
        public virtual DbSet<FolderNameFtsDocsize> FolderNameFtsDocsizes { get; set; }
        public virtual DbSet<FolderNameFtsSegdir> FolderNameFtsSegdirs { get; set; }
        public virtual DbSet<FolderNameFtsSegment> FolderNameFtsSegments { get; set; }
        public virtual DbSet<FolderNameFtsStat> FolderNameFtsStats { get; set; }
        public virtual DbSet<ImageAnalysis> ImageAnalyses { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemDateTaken> ItemDateTakens { get; set; }
        public virtual DbSet<ItemEdit> ItemEdits { get; set; }
        public virtual DbSet<ItemEngineExemplar> ItemEngineExemplars { get; set; }
        public virtual DbSet<ItemEngineStatus> ItemEngineStatuses { get; set; }
        public virtual DbSet<ItemTag> ItemTags { get; set; }
        public virtual DbSet<ItemVideoQuality> ItemVideoQualities { get; set; }
        public virtual DbSet<ItemVideoTag> ItemVideoTags { get; set; }
        public virtual DbSet<LiveTile> LiveTiles { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationCountry> LocationCountries { get; set; }
        public virtual DbSet<LocationCountryFt> LocationCountryFts { get; set; }
        public virtual DbSet<LocationCountryFtsDocsize> LocationCountryFtsDocsizes { get; set; }
        public virtual DbSet<LocationCountryFtsSegdir> LocationCountryFtsSegdirs { get; set; }
        public virtual DbSet<LocationCountryFtsSegment> LocationCountryFtsSegments { get; set; }
        public virtual DbSet<LocationCountryFtsStat> LocationCountryFtsStats { get; set; }
        public virtual DbSet<LocationDistrict> LocationDistricts { get; set; }
        public virtual DbSet<LocationDistrictFt> LocationDistrictFts { get; set; }
        public virtual DbSet<LocationDistrictFtsDocsize> LocationDistrictFtsDocsizes { get; set; }
        public virtual DbSet<LocationDistrictFtsSegdir> LocationDistrictFtsSegdirs { get; set; }
        public virtual DbSet<LocationDistrictFtsSegment> LocationDistrictFtsSegments { get; set; }
        public virtual DbSet<LocationDistrictFtsStat> LocationDistrictFtsStats { get; set; }
        public virtual DbSet<LocationFt> LocationFts { get; set; }
        public virtual DbSet<LocationFtsDocsize> LocationFtsDocsizes { get; set; }
        public virtual DbSet<LocationFtsSegdir> LocationFtsSegdirs { get; set; }
        public virtual DbSet<LocationFtsSegment> LocationFtsSegments { get; set; }
        public virtual DbSet<LocationFtsStat> LocationFtsStats { get; set; }
        public virtual DbSet<LocationGrid> LocationGrids { get; set; }
        public virtual DbSet<LocationRegion> LocationRegions { get; set; }
        public virtual DbSet<LocationRegionFt> LocationRegionFts { get; set; }
        public virtual DbSet<LocationRegionFtsDocsize> LocationRegionFtsDocsizes { get; set; }
        public virtual DbSet<LocationRegionFtsSegdir> LocationRegionFtsSegdirs { get; set; }
        public virtual DbSet<LocationRegionFtsSegment> LocationRegionFtsSegments { get; set; }
        public virtual DbSet<LocationRegionFtsStat> LocationRegionFtsStats { get; set; }
        public virtual DbSet<NetworkTelemetry> NetworkTelemetries { get; set; }
        public virtual DbSet<Ocritem> Ocritems { get; set; }
        public virtual DbSet<OcritemTextView> OcritemTextViews { get; set; }
        public virtual DbSet<OcritemTextViewFt> OcritemTextViewFts { get; set; }
        public virtual DbSet<OcritemTextViewFtsDocsize> OcritemTextViewFtsDocsizes { get; set; }
        public virtual DbSet<OcritemTextViewFtsSegdir> OcritemTextViewFtsSegdirs { get; set; }
        public virtual DbSet<OcritemTextViewFtsSegment> OcritemTextViewFtsSegments { get; set; }
        public virtual DbSet<OcritemTextViewFtsStat> OcritemTextViewFtsStats { get; set; }
        public virtual DbSet<Ocrline> Ocrlines { get; set; }
        public virtual DbSet<Ocrword> Ocrwords { get; set; }
        public virtual DbSet<OneDriveStorageAndUpsellInfo> OneDriveStorageAndUpsellInfos { get; set; }
        public virtual DbSet<PendingCloudAlbumDelete> PendingCloudAlbumDeletes { get; set; }
        public virtual DbSet<PendingUploadItem> PendingUploadItems { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonFt> PersonFts { get; set; }
        public virtual DbSet<PersonFtsDocsize> PersonFtsDocsizes { get; set; }
        public virtual DbSet<PersonFtsSegdir> PersonFtsSegdirs { get; set; }
        public virtual DbSet<PersonFtsSegment> PersonFtsSegments { get; set; }
        public virtual DbSet<PersonFtsStat> PersonFtsStats { get; set; }
        public virtual DbSet<PinnedSearch> PinnedSearches { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<RemoteAlbum> RemoteAlbums { get; set; }
        public virtual DbSet<RemoteItem> RemoteItems { get; set; }
        public virtual DbSet<RemoteProject> RemoteProjects { get; set; }
        public virtual DbSet<RemoteThumbnail> RemoteThumbnails { get; set; }
        public virtual DbSet<SalientRect> SalientRects { get; set; }
        public virtual DbSet<SearchAnalysisItemPriority> SearchAnalysisItemPriorities { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagVariant> TagVariants { get; set; }
        public virtual DbSet<TagVariantFt> TagVariantFts { get; set; }
        public virtual DbSet<TagVariantFtsDocsize> TagVariantFtsDocsizes { get; set; }
        public virtual DbSet<TagVariantFtsSegdir> TagVariantFtsSegdirs { get; set; }
        public virtual DbSet<TagVariantFtsSegment> TagVariantFtsSegments { get; set; }
        public virtual DbSet<TagVariantFtsStat> TagVariantFtsStats { get; set; }
        public virtual DbSet<UserActionAlbumView> UserActionAlbumViews { get; set; }
        public virtual DbSet<UserActionImport> UserActionImports { get; set; }
        public virtual DbSet<UserActionLaunch> UserActionLaunches { get; set; }
        public virtual DbSet<UserActionPrint> UserActionPrints { get; set; }
        public virtual DbSet<UserActionSearch> UserActionSearches { get; set; }
        public virtual DbSet<UserActionShare> UserActionShares { get; set; }
        public virtual DbSet<UserActionSlideshow> UserActionSlideshows { get; set; }
        public virtual DbSet<UserActionView> UserActionViews { get; set; }
        public virtual DbSet<VideoFaceOccurrence> VideoFaceOccurrences { get; set; }
		#endregion
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("Album");

                entity.HasIndex(e => e.AlbumCoverItemId, "Album_CoverItemId");

                entity.HasIndex(e => e.AlbumName, "Album_Name");

                entity.HasIndex(e => e.AlbumOrder, "Album_Order");

                entity.HasIndex(e => e.AlbumSourceId, "Album_SourceId");

                entity.HasIndex(e => e.AlbumState, "Album_State");

                entity.Property(e => e.AlbumId)
                    .ValueGeneratedNever()
                    .HasColumnName("Album_Id");

                entity.Property(e => e.AlbumCount).HasColumnName("Album_Count");

                entity.Property(e => e.AlbumCoverBoundsBottom).HasColumnName("Album_CoverBoundsBottom");

                entity.Property(e => e.AlbumCoverBoundsLeft).HasColumnName("Album_CoverBoundsLeft");

                entity.Property(e => e.AlbumCoverBoundsRight).HasColumnName("Album_CoverBoundsRight");

                entity.Property(e => e.AlbumCoverBoundsTop).HasColumnName("Album_CoverBoundsTop");

                entity.Property(e => e.AlbumCoverItemId).HasColumnName("Album_CoverItemId");

                entity.Property(e => e.AlbumCreationType)
                    .HasColumnName("Album_CreationType")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AlbumDateCreated).HasColumnName("Album_DateCreated");

                entity.Property(e => e.AlbumDateShared).HasColumnName("Album_DateShared");

                entity.Property(e => e.AlbumDateUpdated).HasColumnName("Album_DateUpdated");

                entity.Property(e => e.AlbumDateUserModified).HasColumnName("Album_DateUserModified");

                entity.Property(e => e.AlbumDateViewed).HasColumnName("Album_DateViewed");

                entity.Property(e => e.AlbumEtag).HasColumnName("Album_ETag");

                entity.Property(e => e.AlbumEventEndDate).HasColumnName("Album_EventEndDate");

                entity.Property(e => e.AlbumEventStartDate).HasColumnName("Album_EventStartDate");

                entity.Property(e => e.AlbumName).HasColumnName("Album_Name");

                entity.Property(e => e.AlbumOrder)
                    .HasColumnName("Album_Order")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AlbumPendingTelemetryUploadState)
                    .HasColumnName("Album_PendingTelemetryUploadState")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AlbumPublishState).HasColumnName("Album_PublishState");

                entity.Property(e => e.AlbumQuery).HasColumnName("Album_Query");

                entity.Property(e => e.AlbumQueryBoundsType).HasColumnName("Album_QueryBoundsType");

                entity.Property(e => e.AlbumQueryType).HasColumnName("Album_QueryType");

                entity.Property(e => e.AlbumSentTelemetryUploadState)
                    .HasColumnName("Album_SentTelemetryUploadState")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AlbumSource).HasColumnName("Album_Source");

                entity.Property(e => e.AlbumSourceId).HasColumnName("Album_SourceId");

                entity.Property(e => e.AlbumState).HasColumnName("Album_State");

                entity.Property(e => e.AlbumSummaryEndDate).HasColumnName("Album_SummaryEndDate");

                entity.Property(e => e.AlbumSummaryStartDate).HasColumnName("Album_SummaryStartDate");

                entity.Property(e => e.AlbumType).HasColumnName("Album_Type");

                entity.Property(e => e.AlbumVisibility).HasColumnName("Album_Visibility");

                entity.HasOne(d => d.AlbumCoverItem)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.AlbumCoverItemId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.AlbumSourceNavigation)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.AlbumSourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AlbumItemLink>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AlbumItemLink");

                entity.HasIndex(e => new { e.AlbumItemLinkAlbumId, e.AlbumItemLinkItemId }, "AlbumItemLink_AlbumIdItemId")
                    .IsUnique();

                entity.HasIndex(e => new { e.AlbumItemLinkItemId, e.AlbumItemLinkAlbumId }, "AlbumItemLink_ItemIdAlbumId");

                entity.Property(e => e.AlbumItemLinkAlbumId).HasColumnName("AlbumItemLink_AlbumId");

                entity.Property(e => e.AlbumItemLinkItemId).HasColumnName("AlbumItemLink_ItemId");

                entity.Property(e => e.AlbumItemLinkItemPhotosCloudId).HasColumnName("AlbumItemLink_ItemPhotosCloudId");

                entity.Property(e => e.AlbumItemLinkOrder).HasColumnName("AlbumItemLink_Order");

                entity.HasOne(d => d.AlbumItemLinkAlbum)
                    .WithMany()
                    .HasForeignKey(d => d.AlbumItemLinkAlbumId);

                entity.HasOne(d => d.AlbumItemLinkItem)
                    .WithMany()
                    .HasForeignKey(d => d.AlbumItemLinkItemId);
            });

            modelBuilder.Entity<AlbumNameFt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AlbumName).HasColumnName("Album_Name");
            });

            modelBuilder.Entity<AlbumNameFtsDocsize>(entity =>
            {
                entity.HasKey(e => e.Docid);

                entity.ToTable("AlbumNameFts_docsize");

                entity.Property(e => e.Docid)
                    .ValueGeneratedNever()
                    .HasColumnName("docid");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<AlbumNameFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });

                entity.ToTable("AlbumNameFts_segdir");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.EndBlock).HasColumnName("end_block");

                entity.Property(e => e.LeavesEndBlock).HasColumnName("leaves_end_block");

                entity.Property(e => e.Root).HasColumnName("root");

                entity.Property(e => e.StartBlock).HasColumnName("start_block");
            });

            modelBuilder.Entity<AlbumNameFtsSegment>(entity =>
            {
                entity.HasKey(e => e.Blockid);

                entity.ToTable("AlbumNameFts_segments");

                entity.Property(e => e.Blockid)
                    .ValueGeneratedNever()
                    .HasColumnName("blockid");

                entity.Property(e => e.Block).HasColumnName("block");
            });

            modelBuilder.Entity<AlbumNameFtsStat>(entity =>
            {
                entity.ToTable("AlbumNameFts_stat");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<AppGlobalState>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AppGlobalState");

                entity.Property(e => e.AppGlobalStateAnalysisVersion).HasColumnName("AppGlobalState_AnalysisVersion");

                entity.Property(e => e.AppGlobalStateCachedLocalCollectionSize)
                    .HasColumnName("AppGlobalState_CachedLocalCollectionSize")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateCountLastReconciliationQueryResults).HasColumnName("AppGlobalState_CountLastReconciliationQueryResults");

                entity.Property(e => e.AppGlobalStateCurrentAutoEnhanceEnabledState).HasColumnName("AppGlobalState_CurrentAutoEnhanceEnabledState");

                entity.Property(e => e.AppGlobalStateDateLastAlbumsMaintenance).HasColumnName("AppGlobalState_DateLastAlbumsMaintenance");

                entity.Property(e => e.AppGlobalStateDateLastCacheCleaned).HasColumnName("AppGlobalState_DateLastCacheCleaned");

                entity.Property(e => e.AppGlobalStateDateLastCountryTripAlbumsMaintenance).HasColumnName("AppGlobalState_DateLastCountryTripAlbumsMaintenance");

                entity.Property(e => e.AppGlobalStateDateLastDbAnalyze).HasColumnName("AppGlobalState_DateLastDbAnalyze");

                entity.Property(e => e.AppGlobalStateDateLastDbVacuum).HasColumnName("AppGlobalState_DateLastDbVacuum");

                entity.Property(e => e.AppGlobalStateDateLastItemDeleted).HasColumnName("AppGlobalState_DateLastItemDeleted");

                entity.Property(e => e.AppGlobalStateDateLastLocalReconciled).HasColumnName("AppGlobalState_DateLastLocalReconciled");

                entity.Property(e => e.AppGlobalStateDateLastLocationLookupReady)
                    .HasColumnName("AppGlobalState_DateLastLocationLookupReady")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateDateLastPetAlbumsMaintenance).HasColumnName("AppGlobalState_DateLastPetAlbumsMaintenance");

                entity.Property(e => e.AppGlobalStateDateLastSeasonalAlbumsMaintenance).HasColumnName("AppGlobalState_DateLastSeasonalAlbumsMaintenance");

                entity.Property(e => e.AppGlobalStateDateLastSmileAlbumsMaintenance).HasColumnName("AppGlobalState_DateLastSmileAlbumsMaintenance");

                entity.Property(e => e.AppGlobalStateDateLastTagAlbumsMaintenance).HasColumnName("AppGlobalState_DateLastTagAlbumsMaintenance");

                entity.Property(e => e.AppGlobalStateDateLastWeddingAlbumsMaintenance).HasColumnName("AppGlobalState_DateLastWeddingAlbumsMaintenance");

                entity.Property(e => e.AppGlobalStateDeferredUpgradeVersion).HasColumnName("AppGlobalState_DeferredUpgradeVersion");

                entity.Property(e => e.AppGlobalStateExistingItemsSyncStarted)
                    .HasColumnName("AppGlobalState_ExistingItemsSyncStarted")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateFaceRecognitionConsentDate)
                    .HasColumnName("AppGlobalState_FaceRecognitionConsentDate")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateImportBadgeDisplayState)
                    .HasColumnName("AppGlobalState_ImportBadgeDisplayState")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateLastDateUsedInWeddingAlbumsMaintenance).HasColumnName("AppGlobalState_LastDateUsedInWeddingAlbumsMaintenance");

                entity.Property(e => e.AppGlobalStateNewAlbumsBadgeCount)
                    .HasColumnName("AppGlobalState_NewAlbumsBadgeCount")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateOneDriveAlbumDeltaSyncToken).HasColumnName("AppGlobalState_OneDriveAlbumDeltaSyncToken");

                entity.Property(e => e.AppGlobalStateOneDriveAlbumsResyncing)
                    .HasColumnName("AppGlobalState_OneDriveAlbumsResyncing")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateOneDriveDeltaSyncToken).HasColumnName("AppGlobalState_OneDriveDeltaSyncToken");

                entity.Property(e => e.AppGlobalStateOneDriveFullSyncCompleted).HasColumnName("AppGlobalState_OneDriveFullSyncCompleted");

                entity.Property(e => e.AppGlobalStateOneDriveIdentifyPicturesScope).HasColumnName("AppGlobalState_OneDriveIdentifyPicturesScope");

                entity.Property(e => e.AppGlobalStateOneDriveItemsResyncing)
                    .HasColumnName("AppGlobalState_OneDriveItemsResyncing")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateOneDriveKnownFoldersNeedUpgrade).HasColumnName("AppGlobalState_OneDriveKnownFoldersNeedUpgrade");

                entity.Property(e => e.AppGlobalStateRichMediaGrovelVersion)
                    .HasColumnName("AppGlobalState_RichMediaGrovelVersion")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateRunDedupWork).HasColumnName("AppGlobalState_RunDedupWork");

                entity.Property(e => e.AppGlobalStateTruncateWalfilePending).HasColumnName("AppGlobalState_TruncateWALFilePending");

                entity.Property(e => e.AppGlobalStateXboxLiveItemsResyncing)
                    .HasColumnName("AppGlobalState_XboxLiveItemsResyncing")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<AppTelemetryState>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AppTelemetryState");

                entity.Property(e => e.AppTelemetryStateEventFireTime).HasColumnName("AppTelemetryState_EventFireTime");

                entity.Property(e => e.AppTelemetryStateEventName).HasColumnName("AppTelemetryState_EventName");
            });

            modelBuilder.Entity<ApplicationName>(entity =>
            {
                entity.ToTable("ApplicationName");

                entity.HasIndex(e => e.ApplicationNameText, "ApplicationName_Text");

                entity.Property(e => e.ApplicationNameId)
                    .ValueGeneratedNever()
                    .HasColumnName("ApplicationName_Id");

                entity.Property(e => e.ApplicationNameText).HasColumnName("ApplicationName_Text");
            });

            modelBuilder.Entity<Audio>(entity =>
            {
                entity.ToTable("Audio");

                entity.HasIndex(e => e.AudioUrl, "Audio_Url")
                    .IsUnique();

                entity.Property(e => e.AudioId)
                    .ValueGeneratedNever()
                    .HasColumnName("Audio_Id");

                entity.Property(e => e.AudioChannelCount).HasColumnName("Audio_ChannelCount");

                entity.Property(e => e.AudioDurationPerWindow).HasColumnName("Audio_DurationPerWindow");

                entity.Property(e => e.AudioIntegratedLufs).HasColumnName("Audio_IntegratedLUFS");

                entity.Property(e => e.AudioSampleRate).HasColumnName("Audio_SampleRate");

                entity.Property(e => e.AudioUrl)
                    .IsRequired()
                    .HasColumnName("Audio_Url");

                entity.Property(e => e.AudioWindowInfos)
                    .IsRequired()
                    .HasColumnName("Audio_WindowInfos");
            });

            modelBuilder.Entity<BackgroundTaskTelemetry>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BackgroundTaskTelemetry");

                entity.Property(e => e.BackgroundTaskTelemetryCorrelationGuid)
                    .IsRequired()
                    .HasColumnName("BackgroundTaskTelemetry_CorrelationGuid");

                entity.Property(e => e.BackgroundTaskTelemetryCount).HasColumnName("BackgroundTaskTelemetry_Count");

                entity.Property(e => e.BackgroundTaskTelemetryId).HasColumnName("BackgroundTaskTelemetry_Id");

                entity.Property(e => e.BackgroundTaskTelemetryMaxTime)
                    .HasColumnName("BackgroundTaskTelemetry_MaxTime")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.BackgroundTaskTelemetryMinTime)
                    .HasColumnName("BackgroundTaskTelemetry_MinTime")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.BackgroundTaskTelemetryReason).HasColumnName("BackgroundTaskTelemetry_Reason");

                entity.Property(e => e.BackgroundTaskTelemetryState).HasColumnName("BackgroundTaskTelemetry_State");

                entity.Property(e => e.BackgroundTaskTelemetryTotalTime)
                    .HasColumnName("BackgroundTaskTelemetry_TotalTime")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Cache>(entity =>
            {
                entity.ToTable("Cache");

                entity.HasIndex(e => e.CacheItemId, "IX_Cache_Cache_ItemId")
                    .IsUnique();

                entity.HasIndex(e => e.CacheDateAccessed, "Cache_DateAccessed");

                entity.HasIndex(e => e.CacheFilename, "Cache_Filename");

                entity.HasIndex(e => e.CacheItemId, "Cache_ItemId");

                entity.Property(e => e.CacheId)
                    .ValueGeneratedNever()
                    .HasColumnName("Cache_Id");

                entity.Property(e => e.CacheDateAccessed).HasColumnName("Cache_DateAccessed");

                entity.Property(e => e.CacheFilename).HasColumnName("Cache_Filename");

                entity.Property(e => e.CacheItemId).HasColumnName("Cache_ItemId");

                entity.Property(e => e.CacheModificationVersion).HasColumnName("Cache_ModificationVersion");

                entity.HasOne(d => d.CacheItem)
                    .WithOne(p => p.Cache)
                    .HasForeignKey<Cache>(d => d.CacheItemId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<CameraManufacturer>(entity =>
            {
                entity.ToTable("CameraManufacturer");

                entity.HasIndex(e => e.CameraManufacturerText, "CameraManufacturer_Text");

                entity.Property(e => e.CameraManufacturerId)
                    .ValueGeneratedNever()
                    .HasColumnName("CameraManufacturer_Id");

                entity.Property(e => e.CameraManufacturerText).HasColumnName("CameraManufacturer_Text");
            });

            modelBuilder.Entity<CameraModel>(entity =>
            {
                entity.ToTable("CameraModel");

                entity.HasIndex(e => e.CameraModelText, "CameraModel_Text");

                entity.Property(e => e.CameraModelId)
                    .ValueGeneratedNever()
                    .HasColumnName("CameraModel_Id");

                entity.Property(e => e.CameraModelText).HasColumnName("CameraModel_Text");
            });

            modelBuilder.Entity<CloudAlbum>(entity =>
            {
                entity.ToTable("CloudAlbum");

                entity.HasIndex(e => e.CloudAlbumAlbumId, "CloudAlbum_AlbumId");

                entity.HasIndex(e => new { e.CloudAlbumCloudAlbumDefinitionId, e.CloudAlbumAlbumId }, "CloudAlbum_CloudAlbumDefinitionIdAlbumId")
                    .IsUnique();

                entity.HasIndex(e => e.CloudAlbumCloudId, "CloudAlbum_CloudId");

                entity.Property(e => e.CloudAlbumId)
                    .ValueGeneratedNever()
                    .HasColumnName("CloudAlbum_Id");

                entity.Property(e => e.CloudAlbumAlbumId).HasColumnName("CloudAlbum_AlbumId");

                entity.Property(e => e.CloudAlbumCloudAlbumDefinitionId).HasColumnName("CloudAlbum_CloudAlbumDefinitionId");

                entity.Property(e => e.CloudAlbumCloudId).HasColumnName("CloudAlbum_CloudId");

                entity.HasOne(d => d.CloudAlbumAlbum)
                    .WithMany(p => p.CloudAlbums)
                    .HasForeignKey(d => d.CloudAlbumAlbumId);

                entity.HasOne(d => d.CloudAlbumCloudAlbumDefinition)
                    .WithMany(p => p.CloudAlbums)
                    .HasForeignKey(d => d.CloudAlbumCloudAlbumDefinitionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CloudAlbumDefinition>(entity =>
            {
                entity.ToTable("CloudAlbumDefinition");

                entity.HasIndex(e => e.CloudAlbumDefinitionCloudId, "IX_CloudAlbumDefinition_CloudAlbumDefinition_CloudId")
                    .IsUnique();

                entity.HasIndex(e => e.CloudAlbumDefinitionCloudId, "CloudAlbumDefinition_CloudId");

                entity.Property(e => e.CloudAlbumDefinitionId)
                    .ValueGeneratedNever()
                    .HasColumnName("CloudAlbumDefinition_Id");

                entity.Property(e => e.CloudAlbumDefinitionCloudFriendlyName).HasColumnName("CloudAlbumDefinition_CloudFriendlyName");

                entity.Property(e => e.CloudAlbumDefinitionCloudId).HasColumnName("CloudAlbumDefinition_CloudId");

                entity.Property(e => e.CloudAlbumDefinitionCloudQuery).HasColumnName("CloudAlbumDefinition_CloudQuery");

                entity.Property(e => e.CloudAlbumDefinitionDateLastAlbumsMaintenance)
                    .HasColumnName("CloudAlbumDefinition_DateLastAlbumsMaintenance")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CloudAlbumDefinitionQueryType).HasColumnName("CloudAlbumDefinition_QueryType");
            });

            modelBuilder.Entity<ConceptTagSuppressedTagList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ConceptTagSuppressedTagList");

                entity.Property(e => e.ConceptTagSuppressedTagListTagResourceId).HasColumnName("ConceptTagSuppressedTagList_TagResourceId");
            });

            modelBuilder.Entity<DbRecoveryTaskState>(entity =>
            {
                entity.ToTable("DbRecoveryTaskState");

                entity.Property(e => e.DbRecoveryTaskStateId)
                    .ValueGeneratedNever()
                    .HasColumnName("DbRecoveryTaskState_Id");

                entity.Property(e => e.DbRecoveryTaskStateLastRun).HasColumnName("DbRecoveryTaskState_LastRun");

                entity.Property(e => e.DbRecoveryTaskStateStatePayload).HasColumnName("DbRecoveryTaskState_StatePayload");

                entity.Property(e => e.DbRecoveryTaskStateTaskName)
                    .IsRequired()
                    .HasColumnName("DbRecoveryTaskState_TaskName");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.HasIndex(e => e.EventStartDate, "Event_StartDate")
                    .IsUnique();

                entity.Property(e => e.EventId)
                    .ValueGeneratedNever()
                    .HasColumnName("Event_Id");

                entity.Property(e => e.EventEndDate).HasColumnName("Event_EndDate");

                entity.Property(e => e.EventSize).HasColumnName("Event_Size");

                entity.Property(e => e.EventStartDate).HasColumnName("Event_StartDate");
            });

            modelBuilder.Entity<ExcludedAlbum>(entity =>
            {
                entity.ToTable("ExcludedAlbum");

                entity.HasIndex(e => e.ExcludedAlbumAlbumId, "ExcludedAlbum_AlbumId");

                entity.HasIndex(e => e.ExcludedAlbumExcludedForUse, "ExcludedAlbum_ExcludedForUse");

                entity.Property(e => e.ExcludedAlbumId)
                    .ValueGeneratedNever()
                    .HasColumnName("ExcludedAlbum_Id");

                entity.Property(e => e.ExcludedAlbumAlbumId).HasColumnName("ExcludedAlbum_AlbumId");

                entity.Property(e => e.ExcludedAlbumExcludedDate).HasColumnName("ExcludedAlbum_ExcludedDate");

                entity.Property(e => e.ExcludedAlbumExcludedForUse).HasColumnName("ExcludedAlbum_ExcludedForUse");

                entity.HasOne(d => d.ExcludedAlbumAlbum)
                    .WithMany(p => p.ExcludedAlbums)
                    .HasForeignKey(d => d.ExcludedAlbumAlbumId);
            });

            modelBuilder.Entity<ExcludedFace>(entity =>
            {
                entity.ToTable("ExcludedFace");

                entity.HasIndex(e => e.ExcludedFaceFaceClusterId, "ExcludedFace_FaceClusterId");

                entity.HasIndex(e => e.ExcludedFaceFaceId, "ExcludedFace_FaceId");

                entity.Property(e => e.ExcludedFaceId)
                    .ValueGeneratedNever()
                    .HasColumnName("ExcludedFace_Id");

                entity.Property(e => e.ExcludedFaceExcludedDate).HasColumnName("ExcludedFace_ExcludedDate");

                entity.Property(e => e.ExcludedFaceExcludedForUse).HasColumnName("ExcludedFace_ExcludedForUse");

                entity.Property(e => e.ExcludedFaceFaceClusterId).HasColumnName("ExcludedFace_FaceClusterId");

                entity.Property(e => e.ExcludedFaceFaceId).HasColumnName("ExcludedFace_FaceId");

                entity.HasOne(d => d.ExcludedFaceFaceCluster)
                    .WithMany(p => p.ExcludedFaces)
                    .HasForeignKey(d => d.ExcludedFaceFaceClusterId);

                entity.HasOne(d => d.ExcludedFaceFace)
                    .WithMany(p => p.ExcludedFaces)
                    .HasForeignKey(d => d.ExcludedFaceFaceId);
            });

            modelBuilder.Entity<ExcludedImport>(entity =>
            {
                entity.ToTable("ExcludedImport");

                entity.HasIndex(e => e.ExcludedImportExcludedForUse, "ExcludedImport_ExcludedForUse");

                entity.Property(e => e.ExcludedImportId)
                    .ValueGeneratedNever()
                    .HasColumnName("ExcludedImport_Id");

                entity.Property(e => e.ExcludedImportExcludedDate).HasColumnName("ExcludedImport_ExcludedDate");

                entity.Property(e => e.ExcludedImportExcludedForUse).HasColumnName("ExcludedImport_ExcludedForUse");

                entity.Property(e => e.ExcludedImportImportId).HasColumnName("ExcludedImport_ImportId");
            });

            modelBuilder.Entity<ExcludedItemTag>(entity =>
            {
                entity.ToTable("ExcludedItemTag");

                entity.HasIndex(e => e.ExcludedItemTagItemId, "ExcludedItemTag_ItemId");

                entity.HasIndex(e => e.ExcludedItemTagTagId, "ExcludedItemTag_TagId");

                entity.Property(e => e.ExcludedItemTagId)
                    .ValueGeneratedNever()
                    .HasColumnName("ExcludedItemTag_Id");

                entity.Property(e => e.ExcludedItemTagConceptModelVersion).HasColumnName("ExcludedItemTag_ConceptModelVersion");

                entity.Property(e => e.ExcludedItemTagExcludedDate).HasColumnName("ExcludedItemTag_ExcludedDate");

                entity.Property(e => e.ExcludedItemTagExcludedForUse).HasColumnName("ExcludedItemTag_ExcludedForUse");

                entity.Property(e => e.ExcludedItemTagItemId).HasColumnName("ExcludedItemTag_ItemId");

                entity.Property(e => e.ExcludedItemTagTagId).HasColumnName("ExcludedItemTag_TagId");

                entity.Property(e => e.ExcludedItemTagUploadAttempts).HasColumnName("ExcludedItemTag_UploadAttempts");

                entity.Property(e => e.ExcludedItemTagUploadDateLastAttempt).HasColumnName("ExcludedItemTag_UploadDateLastAttempt");

                entity.Property(e => e.ExcludedItemTagUploadState).HasColumnName("ExcludedItemTag_UploadState");

                entity.HasOne(d => d.ExcludedItemTagItem)
                    .WithMany(p => p.ExcludedItemTags)
                    .HasForeignKey(d => d.ExcludedItemTagItemId);

                entity.HasOne(d => d.ExcludedItemTagTag)
                    .WithMany(p => p.ExcludedItemTags)
                    .HasForeignKey(d => d.ExcludedItemTagTagId);
            });

            modelBuilder.Entity<ExcludedLocation>(entity =>
            {
                entity.ToTable("ExcludedLocation");

                entity.HasIndex(e => e.ExcludedLocationExcludedForUse, "ExcludedLocation_ExcludedForUse");

                entity.HasIndex(e => e.ExcludedLocationLocationId, "ExcludedLocation_LocationId");

                entity.Property(e => e.ExcludedLocationId)
                    .ValueGeneratedNever()
                    .HasColumnName("ExcludedLocation_Id");

                entity.Property(e => e.ExcludedLocationExcludedDate).HasColumnName("ExcludedLocation_ExcludedDate");

                entity.Property(e => e.ExcludedLocationExcludedForUse).HasColumnName("ExcludedLocation_ExcludedForUse");

                entity.Property(e => e.ExcludedLocationLocationId).HasColumnName("ExcludedLocation_LocationId");

                entity.HasOne(d => d.ExcludedLocationLocation)
                    .WithMany(p => p.ExcludedLocations)
                    .HasForeignKey(d => d.ExcludedLocationLocationId);
            });

            modelBuilder.Entity<ExcludedPerson>(entity =>
            {
                entity.ToTable("ExcludedPerson");

                entity.HasIndex(e => e.ExcludedPersonExcludedForUse, "ExcludedPerson_ExcludedForUse");

                entity.HasIndex(e => e.ExcludedPersonPersonId, "ExcludedPerson_PersonId");

                entity.Property(e => e.ExcludedPersonId)
                    .ValueGeneratedNever()
                    .HasColumnName("ExcludedPerson_Id");

                entity.Property(e => e.ExcludedPersonExcludedDate).HasColumnName("ExcludedPerson_ExcludedDate");

                entity.Property(e => e.ExcludedPersonExcludedForUse).HasColumnName("ExcludedPerson_ExcludedForUse");

                entity.Property(e => e.ExcludedPersonPersonId).HasColumnName("ExcludedPerson_PersonId");

                entity.HasOne(d => d.ExcludedPersonPerson)
                    .WithMany(p => p.ExcludedPeople)
                    .HasForeignKey(d => d.ExcludedPersonPersonId);
            });

            modelBuilder.Entity<ExcludedTag>(entity =>
            {
                entity.ToTable("ExcludedTag");

                entity.HasIndex(e => e.ExcludedTagExcludedForUse, "ExcludedTag_ExcludedForUse");

                entity.HasIndex(e => e.ExcludedTagTagId, "ExcludedTag_TagId");

                entity.Property(e => e.ExcludedTagId)
                    .ValueGeneratedNever()
                    .HasColumnName("ExcludedTag_Id");

                entity.Property(e => e.ExcludedTagExcludedDate).HasColumnName("ExcludedTag_ExcludedDate");

                entity.Property(e => e.ExcludedTagExcludedForUse).HasColumnName("ExcludedTag_ExcludedForUse");

                entity.Property(e => e.ExcludedTagTagId).HasColumnName("ExcludedTag_TagId");

                entity.HasOne(d => d.ExcludedTagTag)
                    .WithMany(p => p.ExcludedTags)
                    .HasForeignKey(d => d.ExcludedTagTagId);
            });

            modelBuilder.Entity<ExtractedText>(entity =>
            {
                entity.ToTable("ExtractedText");

                entity.HasIndex(e => e.ExtractedTextItemId, "ExtractedText_ItemId");

                entity.Property(e => e.ExtractedTextId)
                    .ValueGeneratedNever()
                    .HasColumnName("ExtractedText_Id");

                entity.Property(e => e.ExtractedTextItemId).HasColumnName("ExtractedText_ItemId");

                entity.Property(e => e.ExtractedTextText).HasColumnName("ExtractedText_Text");

                entity.HasOne(d => d.ExtractedTextItem)
                    .WithMany(p => p.ExtractedTexts)
                    .HasForeignKey(d => d.ExtractedTextItemId);
            });

            modelBuilder.Entity<Face>(entity =>
            {
                entity.ToTable("Face");

                entity.HasIndex(e => e.FaceExemplarScore, "Face_ExemplarScore");

                entity.HasIndex(e => e.FaceFaceClusterId, "Face_FaceClusterId");

                entity.HasIndex(e => e.FaceItemId, "Face_ItemId");

                entity.HasIndex(e => e.FacePersonId, "Face_PersonId");

                entity.HasIndex(e => e.FaceQualityScore, "Face_QualityScore");

                entity.HasIndex(e => e.FaceRecoGroupId, "Face_RecoGroupId");

                entity.Property(e => e.FaceId)
                    .ValueGeneratedNever()
                    .HasColumnName("Face_Id");

                entity.Property(e => e.FaceCutOffState).HasColumnName("Face_CutOffState");

                entity.Property(e => e.FaceExemplarScore).HasColumnName("Face_ExemplarScore");

                entity.Property(e => e.FaceExpression).HasColumnName("Face_Expression");

                entity.Property(e => e.FaceFaceClusterId).HasColumnName("Face_FaceClusterId");

                entity.Property(e => e.FaceFaceSharpness).HasColumnName("Face_FaceSharpness");

                entity.Property(e => e.FaceIsHighQualityExemplarScore)
                    .HasColumnName("Face_IsHighQualityExemplarScore")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.FaceItemId).HasColumnName("Face_ItemId");

                entity.Property(e => e.FaceLeftEyeCameraFocus).HasColumnName("Face_LeftEyeCameraFocus");

                entity.Property(e => e.FaceLeftEyeLookingAtCamera).HasColumnName("Face_LeftEyeLookingAtCamera");

                entity.Property(e => e.FaceLeftEyeOpen).HasColumnName("Face_LeftEyeOpen");

                entity.Property(e => e.FaceLeftEyeRedEye).HasColumnName("Face_LeftEyeRedEye");

                entity.Property(e => e.FaceLeftEyeSharpness).HasColumnName("Face_LeftEyeSharpness");

                entity.Property(e => e.FaceMouthOpenState).HasColumnName("Face_MouthOpenState");

                entity.Property(e => e.FacePersonConfirmation).HasColumnName("Face_PersonConfirmation");

                entity.Property(e => e.FacePersonId).HasColumnName("Face_PersonId");

                entity.Property(e => e.FacePose).HasColumnName("Face_Pose");

                entity.Property(e => e.FaceQualityScore).HasColumnName("Face_QualityScore");

                entity.Property(e => e.FaceRecoExemplar).HasColumnName("Face_RecoExemplar");

                entity.Property(e => e.FaceRecoGroupId).HasColumnName("Face_RecoGroupId");

                entity.Property(e => e.FaceRectHeight).HasColumnName("Face_Rect_Height");

                entity.Property(e => e.FaceRectLeft).HasColumnName("Face_Rect_Left");

                entity.Property(e => e.FaceRectTop).HasColumnName("Face_Rect_Top");

                entity.Property(e => e.FaceRectWidth).HasColumnName("Face_Rect_Width");

                entity.Property(e => e.FaceRightEyeCameraFocus).HasColumnName("Face_RightEyeCameraFocus");

                entity.Property(e => e.FaceRightEyeLookingAtCamera).HasColumnName("Face_RightEyeLookingAtCamera");

                entity.Property(e => e.FaceRightEyeOpen).HasColumnName("Face_RightEyeOpen");

                entity.Property(e => e.FaceRightEyeRedEye).HasColumnName("Face_RightEyeRedEye");

                entity.Property(e => e.FaceRightEyeSharpness).HasColumnName("Face_RightEyeSharpness");

                entity.Property(e => e.FaceSmileProbability).HasColumnName("Face_SmileProbability");

                entity.Property(e => e.FaceTeethVisibleState).HasColumnName("Face_TeethVisibleState");

                entity.Property(e => e.FaceVersion).HasColumnName("Face_Version");

                entity.Property(e => e.FaceViewRectHeight).HasColumnName("Face_ViewRect_Height");

                entity.Property(e => e.FaceViewRectLeft).HasColumnName("Face_ViewRect_Left");

                entity.Property(e => e.FaceViewRectTop).HasColumnName("Face_ViewRect_Top");

                entity.Property(e => e.FaceViewRectWidth).HasColumnName("Face_ViewRect_Width");

                entity.HasOne(d => d.FaceFaceCluster)
                    .WithMany(p => p.Faces)
                    .HasForeignKey(d => d.FaceFaceClusterId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.FaceItem)
                    .WithMany(p => p.Faces)
                    .HasForeignKey(d => d.FaceItemId);

                entity.HasOne(d => d.FacePerson)
                    .WithMany(p => p.Faces)
                    .HasForeignKey(d => d.FacePersonId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<FaceCluster>(entity =>
            {
                entity.ToTable("FaceCluster");

                entity.HasIndex(e => e.FaceClusterBestFaceId, "FaceCluster_BestFaceId");

                entity.HasIndex(e => e.FaceClusterPersonId, "FaceCluster_PersonId");

                entity.Property(e => e.FaceClusterId)
                    .ValueGeneratedNever()
                    .HasColumnName("FaceCluster_Id");

                entity.Property(e => e.FaceClusterBestFaceId).HasColumnName("FaceCluster_BestFaceId");

                entity.Property(e => e.FaceClusterPersonId).HasColumnName("FaceCluster_PersonId");

                entity.HasOne(d => d.FaceClusterBestFace)
                    .WithMany(p => p.FaceClusters)
                    .HasForeignKey(d => d.FaceClusterBestFaceId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.FaceClusterPerson)
                    .WithMany(p => p.FaceClusters)
                    .HasForeignKey(d => d.FaceClusterPersonId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<FaceFeature>(entity =>
            {
                entity.HasKey(e => new { e.FaceFeatureFaceId, e.FaceFeatureFeatureType });

                entity.ToTable("FaceFeature");

                entity.Property(e => e.FaceFeatureFaceId).HasColumnName("FaceFeature_FaceId");

                entity.Property(e => e.FaceFeatureFeatureType).HasColumnName("FaceFeature_FeatureType");

                entity.Property(e => e.FaceFeatureX).HasColumnName("FaceFeature_X");

                entity.Property(e => e.FaceFeatureY).HasColumnName("FaceFeature_Y");

                entity.HasOne(d => d.FaceFeatureFace)
                    .WithMany(p => p.FaceFeatures)
                    .HasForeignKey(d => d.FaceFeatureFaceId);
            });

            modelBuilder.Entity<FileExtensionFt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ItemFileExtension).HasColumnName("Item_FileExtension");
            });

            modelBuilder.Entity<FileExtensionFtsDocsize>(entity =>
            {
                entity.HasKey(e => e.Docid);

                entity.ToTable("FileExtensionFts_docsize");

                entity.Property(e => e.Docid)
                    .ValueGeneratedNever()
                    .HasColumnName("docid");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<FileExtensionFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });

                entity.ToTable("FileExtensionFts_segdir");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.EndBlock).HasColumnName("end_block");

                entity.Property(e => e.LeavesEndBlock).HasColumnName("leaves_end_block");

                entity.Property(e => e.Root).HasColumnName("root");

                entity.Property(e => e.StartBlock).HasColumnName("start_block");
            });

            modelBuilder.Entity<FileExtensionFtsSegment>(entity =>
            {
                entity.HasKey(e => e.Blockid);

                entity.ToTable("FileExtensionFts_segments");

                entity.Property(e => e.Blockid)
                    .ValueGeneratedNever()
                    .HasColumnName("blockid");

                entity.Property(e => e.Block).HasColumnName("block");
            });

            modelBuilder.Entity<FileExtensionFtsStat>(entity =>
            {
                entity.ToTable("FileExtensionFts_stat");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<FilenameFt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ItemFilename).HasColumnName("Item_Filename");
            });

            modelBuilder.Entity<FilenameFtsDocsize>(entity =>
            {
                entity.HasKey(e => e.Docid);

                entity.ToTable("FilenameFts_docsize");

                entity.Property(e => e.Docid)
                    .ValueGeneratedNever()
                    .HasColumnName("docid");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<FilenameFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });

                entity.ToTable("FilenameFts_segdir");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.EndBlock).HasColumnName("end_block");

                entity.Property(e => e.LeavesEndBlock).HasColumnName("leaves_end_block");

                entity.Property(e => e.Root).HasColumnName("root");

                entity.Property(e => e.StartBlock).HasColumnName("start_block");
            });

            modelBuilder.Entity<FilenameFtsSegment>(entity =>
            {
                entity.HasKey(e => e.Blockid);

                entity.ToTable("FilenameFts_segments");

                entity.Property(e => e.Blockid)
                    .ValueGeneratedNever()
                    .HasColumnName("blockid");

                entity.Property(e => e.Block).HasColumnName("block");
            });

            modelBuilder.Entity<FilenameFtsStat>(entity =>
            {
                entity.ToTable("FilenameFts_stat");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Folder>(entity =>
            {
                entity.ToTable("Folder");

                entity.HasIndex(e => e.FolderDisplayName, "Folder_DisplayName");

                entity.HasIndex(e => e.FolderKnownFolderType, "Folder_KnownFolderType");

                entity.HasIndex(e => e.FolderParentFolderId, "Folder_ParentFolderId");

                entity.HasIndex(e => e.FolderPath, "Folder_Path")
                    .IsUnique();

                entity.HasIndex(e => e.FolderSource, "Folder_Source");

                entity.HasIndex(e => e.FolderSourceId, "Folder_SourceId");

                entity.HasIndex(e => e.FolderStorageProviderFileId, "Folder_StorageProviderFileId");

                entity.HasIndex(e => e.FolderSyncWith, "Folder_SyncWith");

                entity.Property(e => e.FolderId)
                    .ValueGeneratedNever()
                    .HasColumnName("Folder_Id");

                entity.Property(e => e.FolderDateCreated).HasColumnName("Folder_DateCreated");

                entity.Property(e => e.FolderDateModified).HasColumnName("Folder_DateModified");

                entity.Property(e => e.FolderDisplayName).HasColumnName("Folder_DisplayName");

                entity.Property(e => e.FolderInOneDrivePicturesScope).HasColumnName("Folder_InOneDrivePicturesScope");

                entity.Property(e => e.FolderItemCount).HasColumnName("Folder_ItemCount");

                entity.Property(e => e.FolderKnownFolderType).HasColumnName("Folder_KnownFolderType");

                entity.Property(e => e.FolderLibraryRelationship).HasColumnName("Folder_LibraryRelationship");

                entity.Property(e => e.FolderParentFolderId).HasColumnName("Folder_ParentFolderId");

                entity.Property(e => e.FolderPath).HasColumnName("Folder_Path");

                entity.Property(e => e.FolderSource).HasColumnName("Folder_Source");

                entity.Property(e => e.FolderSourceId).HasColumnName("Folder_SourceId");

                entity.Property(e => e.FolderStorageProviderFileId).HasColumnName("Folder_StorageProviderFileId");

                entity.Property(e => e.FolderSyncWith).HasColumnName("Folder_SyncWith");

                entity.HasOne(d => d.FolderParentFolder)
                    .WithMany(p => p.InverseFolderParentFolder)
                    .HasForeignKey(d => d.FolderParentFolderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.FolderSourceNavigation)
                    .WithMany(p => p.Folders)
                    .HasForeignKey(d => d.FolderSourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FolderNameFt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FolderDisplayName).HasColumnName("Folder_DisplayName");
            });

            modelBuilder.Entity<FolderNameFtsDocsize>(entity =>
            {
                entity.HasKey(e => e.Docid);

                entity.ToTable("FolderNameFts_docsize");

                entity.Property(e => e.Docid)
                    .ValueGeneratedNever()
                    .HasColumnName("docid");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<FolderNameFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });

                entity.ToTable("FolderNameFts_segdir");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.EndBlock).HasColumnName("end_block");

                entity.Property(e => e.LeavesEndBlock).HasColumnName("leaves_end_block");

                entity.Property(e => e.Root).HasColumnName("root");

                entity.Property(e => e.StartBlock).HasColumnName("start_block");
            });

            modelBuilder.Entity<FolderNameFtsSegment>(entity =>
            {
                entity.HasKey(e => e.Blockid);

                entity.ToTable("FolderNameFts_segments");

                entity.Property(e => e.Blockid)
                    .ValueGeneratedNever()
                    .HasColumnName("blockid");

                entity.Property(e => e.Block).HasColumnName("block");
            });

            modelBuilder.Entity<FolderNameFtsStat>(entity =>
            {
                entity.ToTable("FolderNameFts_stat");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<ImageAnalysis>(entity =>
            {
                entity.HasKey(e => e.ImageAnalysisItemId);

                entity.ToTable("ImageAnalysis");

                entity.Property(e => e.ImageAnalysisItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("ImageAnalysis_ItemId");

                entity.Property(e => e.ImageAnalysisAnalysisModuleVersion).HasColumnName("ImageAnalysis_AnalysisModuleVersion");

                entity.Property(e => e.ImageAnalysisAverageEyeYlocation).HasColumnName("ImageAnalysis_AverageEyeYLocation");

                entity.Property(e => e.ImageAnalysisAverageEyeYlocationTopRow).HasColumnName("ImageAnalysis_AverageEyeYLocationTopRow");

                entity.Property(e => e.ImageAnalysisAverageFaceXcoordinate).HasColumnName("ImageAnalysis_AverageFaceXCoordinate");

                entity.Property(e => e.ImageAnalysisAverageSaliency).HasColumnName("ImageAnalysis_AverageSaliency");

                entity.Property(e => e.ImageAnalysisBlackPoint).HasColumnName("ImageAnalysis_BlackPoint");

                entity.Property(e => e.ImageAnalysisBlueAdjustment).HasColumnName("ImageAnalysis_BlueAdjustment");

                entity.Property(e => e.ImageAnalysisCenterExposureBalance).HasColumnName("ImageAnalysis_CenterExposureBalance");

                entity.Property(e => e.ImageAnalysisCenterExposureQuality).HasColumnName("ImageAnalysis_CenterExposureQuality");

                entity.Property(e => e.ImageAnalysisCenterOverExposure).HasColumnName("ImageAnalysis_CenterOverExposure");

                entity.Property(e => e.ImageAnalysisCenterUnderExposure).HasColumnName("ImageAnalysis_CenterUnderExposure");

                entity.Property(e => e.ImageAnalysisChromaNoise).HasColumnName("ImageAnalysis_ChromaNoise");

                entity.Property(e => e.ImageAnalysisDetailsNoise).HasColumnName("ImageAnalysis_DetailsNoise");

                entity.Property(e => e.ImageAnalysisExposureBalance).HasColumnName("ImageAnalysis_ExposureBalance");

                entity.Property(e => e.ImageAnalysisExposureQuality).HasColumnName("ImageAnalysis_ExposureQuality");

                entity.Property(e => e.ImageAnalysisFacingOutOfFrame).HasColumnName("ImageAnalysis_FacingOutOfFrame");

                entity.Property(e => e.ImageAnalysisFramedCenter).HasColumnName("ImageAnalysis_FramedCenter");

                entity.Property(e => e.ImageAnalysisGreenAdjustment).HasColumnName("ImageAnalysis_GreenAdjustment");

                entity.Property(e => e.ImageAnalysisHasMainObjectInBackground).HasColumnName("ImageAnalysis_HasMainObjectInBackground");

                entity.Property(e => e.ImageAnalysisHasSharpBackground).HasColumnName("ImageAnalysis_HasSharpBackground");

                entity.Property(e => e.ImageAnalysisHighlightsAdjustment).HasColumnName("ImageAnalysis_HighlightsAdjustment");

                entity.Property(e => e.ImageAnalysisHistogramBuckets).HasColumnName("ImageAnalysis_HistogramBuckets");

                entity.Property(e => e.ImageAnalysisHueVariety).HasColumnName("ImageAnalysis_HueVariety");

                entity.Property(e => e.ImageAnalysisLumaNoise).HasColumnName("ImageAnalysis_LumaNoise");

                entity.Property(e => e.ImageAnalysisMidPoint).HasColumnName("ImageAnalysis_MidPoint");

                entity.Property(e => e.ImageAnalysisNoiseLevel).HasColumnName("ImageAnalysis_NoiseLevel");

                entity.Property(e => e.ImageAnalysisOpenEyeFacePercentage).HasColumnName("ImageAnalysis_OpenEyeFacePercentage");

                entity.Property(e => e.ImageAnalysisOverExposure).HasColumnName("ImageAnalysis_OverExposure");

                entity.Property(e => e.ImageAnalysisPhotoAspectRatio).HasColumnName("ImageAnalysis_PhotoAspectRatio");

                entity.Property(e => e.ImageAnalysisPortraitSize).HasColumnName("ImageAnalysis_PortraitSize");

                entity.Property(e => e.ImageAnalysisPortraitType).HasColumnName("ImageAnalysis_PortraitType");

                entity.Property(e => e.ImageAnalysisReDoAnalysis).HasColumnName("ImageAnalysis_ReDoAnalysis");

                entity.Property(e => e.ImageAnalysisRedAdjustment).HasColumnName("ImageAnalysis_RedAdjustment");

                entity.Property(e => e.ImageAnalysisRelevantFacesPercentage).HasColumnName("ImageAnalysis_RelevantFacesPercentage");

                entity.Property(e => e.ImageAnalysisSaliencyNormalizer).HasColumnName("ImageAnalysis_SaliencyNormalizer");

                entity.Property(e => e.ImageAnalysisSaliencyScore).HasColumnName("ImageAnalysis_SaliencyScore");

                entity.Property(e => e.ImageAnalysisSaturationQuality).HasColumnName("ImageAnalysis_SaturationQuality");

                entity.Property(e => e.ImageAnalysisSaturationType).HasColumnName("ImageAnalysis_SaturationType");

                entity.Property(e => e.ImageAnalysisShadowsAdjustment).HasColumnName("ImageAnalysis_ShadowsAdjustment");

                entity.Property(e => e.ImageAnalysisShadowsChromaNoise).HasColumnName("ImageAnalysis_ShadowsChromaNoise");

                entity.Property(e => e.ImageAnalysisShadowsDetailsNoise).HasColumnName("ImageAnalysis_ShadowsDetailsNoise");

                entity.Property(e => e.ImageAnalysisShadowsLumaNoise).HasColumnName("ImageAnalysis_ShadowsLumaNoise");

                entity.Property(e => e.ImageAnalysisShadowsNoiseLevel).HasColumnName("ImageAnalysis_ShadowsNoiseLevel");

                entity.Property(e => e.ImageAnalysisSharpnessPoint0).HasColumnName("ImageAnalysis_SharpnessPoint0");

                entity.Property(e => e.ImageAnalysisSharpnessPoint1).HasColumnName("ImageAnalysis_SharpnessPoint1");

                entity.Property(e => e.ImageAnalysisSharpnessPoint2).HasColumnName("ImageAnalysis_SharpnessPoint2");

                entity.Property(e => e.ImageAnalysisSharpnessPoint3).HasColumnName("ImageAnalysis_SharpnessPoint3");

                entity.Property(e => e.ImageAnalysisSharpnessPoint4).HasColumnName("ImageAnalysis_SharpnessPoint4");

                entity.Property(e => e.ImageAnalysisStraightenAngle).HasColumnName("ImageAnalysis_StraightenAngle");

                entity.Property(e => e.ImageAnalysisTone).HasColumnName("ImageAnalysis_Tone");

                entity.Property(e => e.ImageAnalysisUnderExposure).HasColumnName("ImageAnalysis_UnderExposure");

                entity.Property(e => e.ImageAnalysisUnsharpMaskAmount).HasColumnName("ImageAnalysis_UnsharpMaskAmount");

                entity.Property(e => e.ImageAnalysisUnsharpMaskRadius).HasColumnName("ImageAnalysis_UnsharpMaskRadius");

                entity.Property(e => e.ImageAnalysisUnsharpMaskThreshold).HasColumnName("ImageAnalysis_UnsharpMaskThreshold");

                entity.Property(e => e.ImageAnalysisUtility).HasColumnName("ImageAnalysis_Utility");

                entity.Property(e => e.ImageAnalysisWhitePoint).HasColumnName("ImageAnalysis_WhitePoint");

                entity.HasOne(d => d.ImageAnalysisItem)
                    .WithOne(p => p.ImageAnalysis)
                    .HasForeignKey<ImageAnalysis>(d => d.ImageAnalysisItemId);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.HasIndex(e => e.ItemAnalysisQueueState, "Item_AnalysisQueueState");

                entity.HasIndex(e => e.ItemBurstChunk, "Item_BurstChunk");

                entity.HasIndex(e => e.ItemBurstClusterNumber, "Item_BurstClusterNumber");

                entity.HasIndex(e => e.ItemBurstPrevItemId, "Item_BurstPrevItemId");

                entity.HasIndex(e => e.ItemDateCreated, "Item_DateCreated");

                entity.HasIndex(e => e.ItemDateDupFinding, "Item_DateDupFinding");

                entity.HasIndex(e => e.ItemDateIngested, "Item_DateIngested");

                entity.HasIndex(e => e.ItemDateTaken, "Item_DateTaken");

                entity.HasIndex(e => e.ItemEditState, "Item_EditState");

                entity.HasIndex(e => e.ItemEventId, "Item_EventId");

                entity.HasIndex(e => e.ItemFileExtension, "Item_FileExtension");

                entity.HasIndex(e => e.ItemFileName, "Item_FileName");

                entity.HasIndex(e => e.ItemLibraryRelationship, "Item_LibraryRelationship");

                entity.HasIndex(e => e.ItemLocationId, "Item_LocationId");

                entity.HasIndex(e => e.ItemMediaType, "Item_MediaType");

                entity.HasIndex(e => e.ItemMetadataHash, "Item_MetadataHash");

                entity.HasIndex(e => new { e.ItemParentFolderId, e.ItemFileName }, "Item_ParentFolderIdFileName");

                entity.HasIndex(e => e.ItemRewriteSupplementaryPropertiesNeeded, "Item_RewriteSupplementaryPropertiesNeeded");

                entity.HasIndex(e => e.ItemRichMediaSlowGrovelPending, "Item_RichMediaSlowGrovelPending");

                entity.HasIndex(e => e.ItemSameAs, "Item_SameAs");

                entity.HasIndex(e => e.ItemSource, "Item_Source");

                entity.HasIndex(e => e.ItemSourceId, "Item_SourceId");

                entity.HasIndex(e => e.ItemStorageProviderFileId, "Item_StorageProviderFileId");

                entity.HasIndex(e => e.ItemSyncWith, "Item_SyncWith");

                entity.HasIndex(e => e.ItemUserSelectedDupId, "Item_UserSelectedDupId");

                entity.Property(e => e.ItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("Item_Id");

                entity.Property(e => e.ItemAnalysisErrorCode).HasColumnName("Item_AnalysisErrorCode");

                entity.Property(e => e.ItemAnalysisErrorCount).HasColumnName("Item_AnalysisErrorCount");

                entity.Property(e => e.ItemAnalysisLastRun).HasColumnName("Item_AnalysisLastRun");

                entity.Property(e => e.ItemAnalysisQueueState).HasColumnName("Item_AnalysisQueueState");

                entity.Property(e => e.ItemAnalysisVersion).HasColumnName("Item_AnalysisVersion");

                entity.Property(e => e.ItemApplicationNameId).HasColumnName("Item_ApplicationNameId");

                entity.Property(e => e.ItemBurstChunk)
                    .HasColumnName("Item_BurstChunk")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ItemBurstClusterNumber)
                    .HasColumnName("Item_BurstClusterNumber")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ItemBurstPrevItemId).HasColumnName("Item_BurstPrevItemId");

                entity.Property(e => e.ItemCameraManufacturerId).HasColumnName("Item_CameraManufacturerId");

                entity.Property(e => e.ItemCameraModelId).HasColumnName("Item_CameraModelId");

                entity.Property(e => e.ItemCameraSettingExposureTime).HasColumnName("Item_CameraSettingExposureTime");

                entity.Property(e => e.ItemCameraSettingFnumber).HasColumnName("Item_CameraSettingFNumber");

                entity.Property(e => e.ItemCameraSettingFocalLength).HasColumnName("Item_CameraSettingFocalLength");

                entity.Property(e => e.ItemCameraSettingIsospeed).HasColumnName("Item_CameraSettingISOSpeed");

                entity.Property(e => e.ItemCaption).HasColumnName("Item_Caption");

                entity.Property(e => e.ItemDateCreated).HasColumnName("Item_DateCreated");

                entity.Property(e => e.ItemDateDupFinding).HasColumnName("Item_DateDupFinding");

                entity.Property(e => e.ItemDateIngested).HasColumnName("Item_DateIngested");

                entity.Property(e => e.ItemDateModified).HasColumnName("Item_DateModified");

                entity.Property(e => e.ItemDateModifiedAtLastAnalysisRun).HasColumnName("Item_DateModifiedAtLastAnalysisRun");

                entity.Property(e => e.ItemDateModifiedAtLastBurstRun)
                    .HasColumnName("Item_DateModifiedAtLastBurstRun")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ItemDateTaken).HasColumnName("Item_DateTaken");

                entity.Property(e => e.ItemDupFinderVersion).HasColumnName("Item_DupFinderVersion");

                entity.Property(e => e.ItemDupState).HasColumnName("Item_DupState");

                entity.Property(e => e.ItemDuration).HasColumnName("Item_Duration");

                entity.Property(e => e.ItemEditList).HasColumnName("Item_EditList");

                entity.Property(e => e.ItemEditState).HasColumnName("Item_EditState");

                entity.Property(e => e.ItemEtag).HasColumnName("Item_ETag");

                entity.Property(e => e.ItemEventId).HasColumnName("Item_EventId");

                entity.Property(e => e.ItemExclusiveDateTaken).HasColumnName("Item_ExclusiveDateTaken");

                entity.Property(e => e.ItemFileExtension).HasColumnName("Item_FileExtension");

                entity.Property(e => e.ItemFileHeight).HasColumnName("Item_FileHeight");

                entity.Property(e => e.ItemFileName).HasColumnName("Item_FileName").UseCollation("NoCaseUnicode");

                entity.Property(e => e.ItemFileSize).HasColumnName("Item_FileSize");

                entity.Property(e => e.ItemFileWidth).HasColumnName("Item_FileWidth");

                entity.Property(e => e.ItemFlash).HasColumnName("Item_Flash");

                entity.Property(e => e.ItemFrameRate).HasColumnName("Item_FrameRate");

                entity.Property(e => e.ItemHasAuthor).HasColumnName("Item_HasAuthor");

                entity.Property(e => e.ItemHasCopyright).HasColumnName("Item_HasCopyright");

                entity.Property(e => e.ItemHasDup).HasColumnName("Item_HasDup");

                entity.Property(e => e.ItemHasKeywords).HasColumnName("Item_HasKeywords");

                entity.Property(e => e.ItemHashAtLastNdethumbnailGeneration).HasColumnName("Item_HashAtLastNDEThumbnailGeneration");

                entity.Property(e => e.ItemHeight).HasColumnName("Item_Height");

                entity.Property(e => e.ItemImportSession).HasColumnName("Item_ImportSession");

                entity.Property(e => e.ItemInAppRotatePending).HasColumnName("Item_InAppRotatePending");

                entity.Property(e => e.ItemIsExportedMovie).HasColumnName("Item_IsExportedMovie");

                entity.Property(e => e.ItemIsInked).HasColumnName("Item_IsInked");

                entity.Property(e => e.ItemLastEditDate).HasColumnName("Item_LastEditDate");

                entity.Property(e => e.ItemLastUploadAttemptTime)
                    .HasColumnName("Item_LastUploadAttemptTime")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ItemLatitude).HasColumnName("Item_Latitude");

                entity.Property(e => e.ItemLibraryRelationship).HasColumnName("Item_LibraryRelationship");

                entity.Property(e => e.ItemLocationId).HasColumnName("Item_LocationId");

                entity.Property(e => e.ItemLongitude).HasColumnName("Item_Longitude");

                entity.Property(e => e.ItemMediaType).HasColumnName("Item_MediaType");

                entity.Property(e => e.ItemMetadataExtractedAsOf).HasColumnName("Item_MetadataExtractedAsOf");

                entity.Property(e => e.ItemMetadataHash).HasColumnName("Item_MetadataHash");

                entity.Property(e => e.ItemModificationVersion)
                    .HasColumnName("Item_ModificationVersion")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ItemNdeheight).HasColumnName("Item_NDEHeight");

                entity.Property(e => e.ItemNdethumbnailGenerationErrorCount).HasColumnName("Item_NDEThumbnailGenerationErrorCount");

                entity.Property(e => e.ItemNdewidth).HasColumnName("Item_NDEWidth");

                entity.Property(e => e.ItemOnlineContentAttributionString).HasColumnName("Item_OnlineContentAttributionString");

                entity.Property(e => e.ItemParentFolderId).HasColumnName("Item_ParentFolderId");

                entity.Property(e => e.ItemPendingNde).HasColumnName("Item_PendingNDE");

                entity.Property(e => e.ItemPendingTelemetryUploadState)
                    .HasColumnName("Item_PendingTelemetryUploadState")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ItemPendingXmpExtractionMask).HasColumnName("Item_PendingXmpExtractionMask");

                entity.Property(e => e.ItemPixelHash).HasColumnName("Item_PixelHash");

                entity.Property(e => e.ItemQualityScore).HasColumnName("Item_QualityScore");

                entity.Property(e => e.ItemRewriteSupplementaryPropertiesNeeded)
                    .HasColumnName("Item_RewriteSupplementaryPropertiesNeeded")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.ItemRichMediaAppId).HasColumnName("Item_RichMediaAppId");

                entity.Property(e => e.ItemRichMediaFileStatus).HasColumnName("Item_RichMediaFileStatus");

                entity.Property(e => e.ItemRichMediaId).HasColumnName("Item_RichMediaId");

                entity.Property(e => e.ItemRichMediaLaunchOptions).HasColumnName("Item_RichMediaLaunchOptions");

                entity.Property(e => e.ItemRichMediaSlowGrovelPending).HasColumnName("Item_RichMediaSlowGrovelPending");

                entity.Property(e => e.ItemSameAs).HasColumnName("Item_SameAs");

                entity.Property(e => e.ItemSentTelemetryUploadState)
                    .HasColumnName("Item_SentTelemetryUploadState")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ItemSimpleRating).HasColumnName("Item_SimpleRating");

                entity.Property(e => e.ItemSource).HasColumnName("Item_Source");

                entity.Property(e => e.ItemSourceId).HasColumnName("Item_SourceId");

                entity.Property(e => e.ItemStorageProviderFileId).HasColumnName("Item_StorageProviderFileId");

                entity.Property(e => e.ItemSyncWith).HasColumnName("Item_SyncWith");

                entity.Property(e => e.ItemSystemThumbnailCacheId).HasColumnName("Item_SystemThumbnailCacheId");

                entity.Property(e => e.ItemThumbnailPrecacheAttempted).HasColumnName("Item_ThumbnailPrecacheAttempted");

                entity.Property(e => e.ItemUploadAttempts)
                    .HasColumnName("Item_UploadAttempts")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ItemUploadPendingState)
                    .HasColumnName("Item_UploadPendingState")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ItemUploadRequestTime)
                    .HasColumnName("Item_UploadRequestTime")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ItemUserSelectedDupId).HasColumnName("Item_UserSelectedDupId");

                entity.Property(e => e.ItemUserUnlink).HasColumnName("Item_UserUnlink");

                entity.Property(e => e.ItemWidth).HasColumnName("Item_Width");

                entity.HasOne(d => d.ItemApplicationName)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemApplicationNameId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.ItemBurstPrevItem)
                    .WithMany(p => p.InverseItemBurstPrevItem)
                    .HasForeignKey(d => d.ItemBurstPrevItemId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.ItemCameraManufacturer)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemCameraManufacturerId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.ItemCameraModel)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemCameraModelId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.ItemEvent)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemEventId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.ItemLocation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemLocationId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.ItemParentFolder)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemParentFolderId);

                entity.HasOne(d => d.ItemSourceNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemSourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ItemDateTaken>(entity =>
            {
                entity.HasKey(e => e.ItemDateTakenItemId);

                entity.ToTable("ItemDateTaken");

                entity.HasIndex(e => e.ItemDateTakenDay, "ItemDateTaken_Day");

                entity.HasIndex(e => e.ItemDateTakenDayOfWeek, "ItemDateTaken_DayOfWeek");

                entity.HasIndex(e => e.ItemDateTakenMonth, "ItemDateTaken_Month");

                entity.HasIndex(e => e.ItemDateTakenYear, "ItemDateTaken_Year");

                entity.Property(e => e.ItemDateTakenItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("ItemDateTaken_ItemId");

                entity.Property(e => e.ItemDateTakenDay).HasColumnName("ItemDateTaken_Day");

                entity.Property(e => e.ItemDateTakenDayOfWeek).HasColumnName("ItemDateTaken_DayOfWeek");

                entity.Property(e => e.ItemDateTakenMonth).HasColumnName("ItemDateTaken_Month");

                entity.Property(e => e.ItemDateTakenYear).HasColumnName("ItemDateTaken_Year");
            });

            modelBuilder.Entity<ItemEdit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ItemEdit");

                entity.HasIndex(e => e.ItemEditItemId, "ItemEdit_ItemId");

                entity.Property(e => e.ItemEditEditDate).HasColumnName("ItemEdit_EditDate");

                entity.Property(e => e.ItemEditEditTypeId).HasColumnName("ItemEdit_EditTypeId");

                entity.Property(e => e.ItemEditItemId).HasColumnName("ItemEdit_ItemId");

                entity.HasOne(d => d.ItemEditItem)
                    .WithMany()
                    .HasForeignKey(d => d.ItemEditItemId);
            });

            modelBuilder.Entity<ItemEngineExemplar>(entity =>
            {
                entity.ToTable("ItemEngineExemplar");

                entity.HasIndex(e => e.ItemEngineExemplarItemId, "ItemEngineExemplar_ItemId")
                    .IsUnique();

                entity.Property(e => e.ItemEngineExemplarId)
                    .ValueGeneratedNever()
                    .HasColumnName("ItemEngineExemplar_Id");

                entity.Property(e => e.ItemEngineExemplarExemplar).HasColumnName("ItemEngineExemplar_Exemplar");

                entity.Property(e => e.ItemEngineExemplarItemId).HasColumnName("ItemEngineExemplar_ItemId");

                entity.HasOne(d => d.ItemEngineExemplarItem)
                    .WithOne(p => p.ItemEngineExemplar)
                    .HasForeignKey<ItemEngineExemplar>(d => d.ItemEngineExemplarItemId);
            });

            modelBuilder.Entity<ItemEngineStatus>(entity =>
            {
                entity.ToTable("ItemEngineStatus");

                entity.HasIndex(e => e.ItemEngineStatusItemId, "ItemEngineStatus_ItemId")
                    .IsUnique();

                entity.HasIndex(e => e.ItemEngineStatusLastRun, "ItemEngineStatus_LastRun");

                entity.Property(e => e.ItemEngineStatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("ItemEngineStatus_Id");

                entity.Property(e => e.ItemEngineStatusAnalysisDone).HasColumnName("ItemEngineStatus_AnalysisDone");

                entity.Property(e => e.ItemEngineStatusErrorCode).HasColumnName("ItemEngineStatus_ErrorCode");

                entity.Property(e => e.ItemEngineStatusErrorString).HasColumnName("ItemEngineStatus_ErrorString");

                entity.Property(e => e.ItemEngineStatusItemId).HasColumnName("ItemEngineStatus_ItemId");

                entity.Property(e => e.ItemEngineStatusLastFrameAnalyzed).HasColumnName("ItemEngineStatus_LastFrameAnalyzed");

                entity.Property(e => e.ItemEngineStatusLastRun).HasColumnName("ItemEngineStatus_LastRun");

                entity.Property(e => e.ItemEngineStatusPartialVideoVersion).HasColumnName("ItemEngineStatus_PartialVideoVersion");

                entity.Property(e => e.ItemEngineStatusRetryCount).HasColumnName("ItemEngineStatus_RetryCount");

                entity.Property(e => e.ItemEngineStatusStatus).HasColumnName("ItemEngineStatus_Status");

                entity.Property(e => e.ItemEngineStatusVersion).HasColumnName("ItemEngineStatus_Version");

                entity.HasOne(d => d.ItemEngineStatusItem)
                    .WithOne(p => p.ItemEngineStatus)
                    .HasForeignKey<ItemEngineStatus>(d => d.ItemEngineStatusItemId);
            });

            modelBuilder.Entity<ItemTag>(entity =>
            {
                entity.HasKey(e => e.ItemTagsId);

                entity.HasIndex(e => e.ItemTagsItemId, "ItemTags_ItemId");

                entity.HasIndex(e => new { e.ItemTagsItemId, e.ItemTagsTagId }, "ItemTags_ItemId_TagId")
                    .IsUnique();

                entity.HasIndex(e => e.ItemTagsTagId, "ItemTags_TagId");

                entity.Property(e => e.ItemTagsId)
                    .ValueGeneratedNever()
                    .HasColumnName("ItemTags_Id");

                entity.Property(e => e.ItemTagsConfidence).HasColumnName("ItemTags_Confidence");

                entity.Property(e => e.ItemTagsItemId).HasColumnName("ItemTags_ItemId");

                entity.Property(e => e.ItemTagsTagId).HasColumnName("ItemTags_TagId");

                entity.HasOne(d => d.ItemTagsItem)
                    .WithMany(p => p.ItemTags)
                    .HasForeignKey(d => d.ItemTagsItemId);

                entity.HasOne(d => d.ItemTagsTag)
                    .WithMany(p => p.ItemTags)
                    .HasForeignKey(d => d.ItemTagsTagId);
            });

            modelBuilder.Entity<ItemVideoQuality>(entity =>
            {
                entity.ToTable("ItemVideoQuality");

                entity.HasIndex(e => e.ItemVideoQualityItemId, "ItemVideoQuality_ItemId");

                entity.Property(e => e.ItemVideoQualityId)
                    .ValueGeneratedNever()
                    .HasColumnName("ItemVideoQuality_Id");

                entity.Property(e => e.ItemVideoQualityBeginFrame).HasColumnName("ItemVideoQuality_BeginFrame");

                entity.Property(e => e.ItemVideoQualityEndFrame).HasColumnName("ItemVideoQuality_EndFrame");

                entity.Property(e => e.ItemVideoQualityItemId).HasColumnName("ItemVideoQuality_ItemId");

                entity.Property(e => e.ItemVideoQualityQuality).HasColumnName("ItemVideoQuality_Quality");

                entity.Property(e => e.ItemVideoQualityQualityLevel).HasColumnName("ItemVideoQuality_QualityLevel");

                entity.HasOne(d => d.ItemVideoQualityItem)
                    .WithMany(p => p.ItemVideoQualities)
                    .HasForeignKey(d => d.ItemVideoQualityItemId);
            });

            modelBuilder.Entity<ItemVideoTag>(entity =>
            {
                entity.HasKey(e => e.ItemVideoTagsId);

                entity.HasIndex(e => e.ItemVideoTagsItemTagsId, "ItemVideoTags_ItemTagsId");

                entity.Property(e => e.ItemVideoTagsId)
                    .ValueGeneratedNever()
                    .HasColumnName("ItemVideoTags_Id");

                entity.Property(e => e.ItemVideoTagsBeginFrame).HasColumnName("ItemVideoTags_BeginFrame");

                entity.Property(e => e.ItemVideoTagsConfidence).HasColumnName("ItemVideoTags_Confidence");

                entity.Property(e => e.ItemVideoTagsEndFrame).HasColumnName("ItemVideoTags_EndFrame");

                entity.Property(e => e.ItemVideoTagsItemTagsId).HasColumnName("ItemVideoTags_ItemTagsId");

                entity.HasOne(d => d.ItemVideoTagsItemTags)
                    .WithMany(p => p.ItemVideoTags)
                    .HasForeignKey(d => d.ItemVideoTagsItemTagsId);
            });

            modelBuilder.Entity<LiveTile>(entity =>
            {
                entity.HasKey(e => e.LiveTileItemId);

                entity.ToTable("LiveTile");

                entity.Property(e => e.LiveTileItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("LiveTile_ItemId");

                entity.HasOne(d => d.LiveTileItem)
                    .WithOne(p => p.LiveTile)
                    .HasForeignKey<LiveTile>(d => d.LiveTileItemId);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.HasIndex(e => e.LocationCoverItemId, "Location_CoverItemId");

                entity.HasIndex(e => e.LocationItemsCountExcDupes, "Location_ItemsCountExcDupes");

                entity.HasIndex(e => e.LocationLocationCountryId, "Location_LocationCountryId");

                entity.HasIndex(e => e.LocationLocationDistrictId, "Location_LocationDistrictId");

                entity.HasIndex(e => e.LocationLocationRegionId, "Location_LocationRegionId");

                entity.HasIndex(e => e.LocationName, "Location_Name");

                entity.Property(e => e.LocationId)
                    .ValueGeneratedNever()
                    .HasColumnName("Location_Id");

                entity.Property(e => e.LocationCoverItemId).HasColumnName("Location_CoverItemId");

                entity.Property(e => e.LocationItemsCountExcDupes)
                    .HasColumnName("Location_ItemsCountExcDupes")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.LocationLocationCountryId).HasColumnName("Location_LocationCountryId");

                entity.Property(e => e.LocationLocationDistrictId).HasColumnName("Location_LocationDistrictId");

                entity.Property(e => e.LocationLocationRegionId).HasColumnName("Location_LocationRegionId");

                entity.Property(e => e.LocationName).HasColumnName("Location_Name");

                entity.HasOne(d => d.LocationCoverItem)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.LocationCoverItemId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.LocationLocationCountry)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.LocationLocationCountryId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.LocationLocationDistrict)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.LocationLocationDistrictId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.LocationLocationRegion)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.LocationLocationRegionId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<LocationCountry>(entity =>
            {
                entity.ToTable("LocationCountry");

                entity.HasIndex(e => e.LocationCountryName, "LocationCountry_Name");

                entity.Property(e => e.LocationCountryId)
                    .ValueGeneratedNever()
                    .HasColumnName("LocationCountry_Id");

                entity.Property(e => e.LocationCountryName).HasColumnName("LocationCountry_Name");
            });

            modelBuilder.Entity<LocationCountryFt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LocationCountryName).HasColumnName("LocationCountry_Name");
            });

            modelBuilder.Entity<LocationCountryFtsDocsize>(entity =>
            {
                entity.HasKey(e => e.Docid);

                entity.ToTable("LocationCountryFts_docsize");

                entity.Property(e => e.Docid)
                    .ValueGeneratedNever()
                    .HasColumnName("docid");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<LocationCountryFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });

                entity.ToTable("LocationCountryFts_segdir");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.EndBlock).HasColumnName("end_block");

                entity.Property(e => e.LeavesEndBlock).HasColumnName("leaves_end_block");

                entity.Property(e => e.Root).HasColumnName("root");

                entity.Property(e => e.StartBlock).HasColumnName("start_block");
            });

            modelBuilder.Entity<LocationCountryFtsSegment>(entity =>
            {
                entity.HasKey(e => e.Blockid);

                entity.ToTable("LocationCountryFts_segments");

                entity.Property(e => e.Blockid)
                    .ValueGeneratedNever()
                    .HasColumnName("blockid");

                entity.Property(e => e.Block).HasColumnName("block");
            });

            modelBuilder.Entity<LocationCountryFtsStat>(entity =>
            {
                entity.ToTable("LocationCountryFts_stat");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<LocationDistrict>(entity =>
            {
                entity.ToTable("LocationDistrict");

                entity.HasIndex(e => e.LocationDistrictName, "LocationDistrict_Name");

                entity.Property(e => e.LocationDistrictId)
                    .ValueGeneratedNever()
                    .HasColumnName("LocationDistrict_Id");

                entity.Property(e => e.LocationDistrictLocationRegionId).HasColumnName("LocationDistrict_LocationRegionId");

                entity.Property(e => e.LocationDistrictName).HasColumnName("LocationDistrict_Name");

                entity.HasOne(d => d.LocationDistrictLocationRegion)
                    .WithMany(p => p.LocationDistricts)
                    .HasForeignKey(d => d.LocationDistrictLocationRegionId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<LocationDistrictFt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LocationDistrictName).HasColumnName("LocationDistrict_Name");
            });

            modelBuilder.Entity<LocationDistrictFtsDocsize>(entity =>
            {
                entity.HasKey(e => e.Docid);

                entity.ToTable("LocationDistrictFts_docsize");

                entity.Property(e => e.Docid)
                    .ValueGeneratedNever()
                    .HasColumnName("docid");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<LocationDistrictFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });

                entity.ToTable("LocationDistrictFts_segdir");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.EndBlock).HasColumnName("end_block");

                entity.Property(e => e.LeavesEndBlock).HasColumnName("leaves_end_block");

                entity.Property(e => e.Root).HasColumnName("root");

                entity.Property(e => e.StartBlock).HasColumnName("start_block");
            });

            modelBuilder.Entity<LocationDistrictFtsSegment>(entity =>
            {
                entity.HasKey(e => e.Blockid);

                entity.ToTable("LocationDistrictFts_segments");

                entity.Property(e => e.Blockid)
                    .ValueGeneratedNever()
                    .HasColumnName("blockid");

                entity.Property(e => e.Block).HasColumnName("block");
            });

            modelBuilder.Entity<LocationDistrictFtsStat>(entity =>
            {
                entity.ToTable("LocationDistrictFts_stat");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<LocationFt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LocationName).HasColumnName("Location_Name");
            });

            modelBuilder.Entity<LocationFtsDocsize>(entity =>
            {
                entity.HasKey(e => e.Docid);

                entity.ToTable("LocationFts_docsize");

                entity.Property(e => e.Docid)
                    .ValueGeneratedNever()
                    .HasColumnName("docid");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<LocationFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });

                entity.ToTable("LocationFts_segdir");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.EndBlock).HasColumnName("end_block");

                entity.Property(e => e.LeavesEndBlock).HasColumnName("leaves_end_block");

                entity.Property(e => e.Root).HasColumnName("root");

                entity.Property(e => e.StartBlock).HasColumnName("start_block");
            });

            modelBuilder.Entity<LocationFtsSegment>(entity =>
            {
                entity.HasKey(e => e.Blockid);

                entity.ToTable("LocationFts_segments");

                entity.Property(e => e.Blockid)
                    .ValueGeneratedNever()
                    .HasColumnName("blockid");

                entity.Property(e => e.Block).HasColumnName("block");
            });

            modelBuilder.Entity<LocationFtsStat>(entity =>
            {
                entity.ToTable("LocationFts_stat");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<LocationGrid>(entity =>
            {
                entity.ToTable("LocationGrid");

                entity.HasIndex(e => new { e.LocationGridLatitude, e.LocationGridLongitude }, "LocationGrid_LatLong");

                entity.Property(e => e.LocationGridId)
                    .ValueGeneratedNever()
                    .HasColumnName("LocationGrid_Id");

                entity.Property(e => e.LocationGridErrorCount).HasColumnName("LocationGrid_ErrorCount");

                entity.Property(e => e.LocationGridLastRun).HasColumnName("LocationGrid_LastRun");

                entity.Property(e => e.LocationGridLatitude).HasColumnName("LocationGrid_Latitude");

                entity.Property(e => e.LocationGridLocationId).HasColumnName("LocationGrid_LocationId");

                entity.Property(e => e.LocationGridLongitude).HasColumnName("LocationGrid_Longitude");

                entity.HasOne(d => d.LocationGridLocation)
                    .WithMany(p => p.LocationGrids)
                    .HasForeignKey(d => d.LocationGridLocationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LocationRegion>(entity =>
            {
                entity.ToTable("LocationRegion");

                entity.HasIndex(e => e.LocationRegionName, "LocationRegion_Name");

                entity.Property(e => e.LocationRegionId)
                    .ValueGeneratedNever()
                    .HasColumnName("LocationRegion_Id");

                entity.Property(e => e.LocationRegionLocationCountryId).HasColumnName("LocationRegion_LocationCountryId");

                entity.Property(e => e.LocationRegionName).HasColumnName("LocationRegion_Name");

                entity.HasOne(d => d.LocationRegionLocationCountry)
                    .WithMany(p => p.LocationRegions)
                    .HasForeignKey(d => d.LocationRegionLocationCountryId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<LocationRegionFt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LocationRegionName).HasColumnName("LocationRegion_Name");
            });

            modelBuilder.Entity<LocationRegionFtsDocsize>(entity =>
            {
                entity.HasKey(e => e.Docid);

                entity.ToTable("LocationRegionFts_docsize");

                entity.Property(e => e.Docid)
                    .ValueGeneratedNever()
                    .HasColumnName("docid");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<LocationRegionFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });

                entity.ToTable("LocationRegionFts_segdir");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.EndBlock).HasColumnName("end_block");

                entity.Property(e => e.LeavesEndBlock).HasColumnName("leaves_end_block");

                entity.Property(e => e.Root).HasColumnName("root");

                entity.Property(e => e.StartBlock).HasColumnName("start_block");
            });

            modelBuilder.Entity<LocationRegionFtsSegment>(entity =>
            {
                entity.HasKey(e => e.Blockid);

                entity.ToTable("LocationRegionFts_segments");

                entity.Property(e => e.Blockid)
                    .ValueGeneratedNever()
                    .HasColumnName("blockid");

                entity.Property(e => e.Block).HasColumnName("block");
            });

            modelBuilder.Entity<LocationRegionFtsStat>(entity =>
            {
                entity.ToTable("LocationRegionFts_stat");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<NetworkTelemetry>(entity =>
            {
                entity.HasKey(e => new { e.NetworkTelemetrySource, e.NetworkTelemetryRequestType, e.NetworkTelemetryIsBackgroundTaskHost });

                entity.ToTable("NetworkTelemetry");

                entity.Property(e => e.NetworkTelemetrySource).HasColumnName("NetworkTelemetry_Source");

                entity.Property(e => e.NetworkTelemetryRequestType).HasColumnName("NetworkTelemetry_RequestType");

                entity.Property(e => e.NetworkTelemetryIsBackgroundTaskHost).HasColumnName("NetworkTelemetry_IsBackgroundTaskHost");

                entity.Property(e => e.NetworkTelemetryByteSize).HasColumnName("NetworkTelemetry_ByteSize");

                entity.Property(e => e.NetworkTelemetryTotalCount).HasColumnName("NetworkTelemetry_TotalCount");
            });

            modelBuilder.Entity<Ocritem>(entity =>
            {
                entity.ToTable("OCRItem");

                entity.HasIndex(e => e.OcritemItemId, "OCRItem_ItemId");

                entity.Property(e => e.OcritemId)
                    .ValueGeneratedNever()
                    .HasColumnName("OCRItem_Id");

                entity.Property(e => e.OcritemItemId).HasColumnName("OCRItem_ItemId");

                entity.Property(e => e.OcritemTextAngle).HasColumnName("OCRItem_TextAngle");

                entity.HasOne(d => d.OcritemItem)
                    .WithMany(p => p.Ocritems)
                    .HasForeignKey(d => d.OcritemItemId);
            });

            modelBuilder.Entity<OcritemTextView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OCRItemTextView");

                entity.Property(e => e.OcritemTextViewText).HasColumnName("OCRItemTextView_Text");

                entity.Property(e => e.Rowid).HasColumnName("rowid");
            });

            modelBuilder.Entity<OcritemTextViewFt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OCRItemTextViewFts");

                entity.Property(e => e.OcritemTextViewText).HasColumnName("OCRItemTextView_Text");
            });

            modelBuilder.Entity<OcritemTextViewFtsDocsize>(entity =>
            {
                entity.HasKey(e => e.Docid);

                entity.ToTable("OCRItemTextViewFts_docsize");

                entity.Property(e => e.Docid)
                    .ValueGeneratedNever()
                    .HasColumnName("docid");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<OcritemTextViewFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });

                entity.ToTable("OCRItemTextViewFts_segdir");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.EndBlock).HasColumnName("end_block");

                entity.Property(e => e.LeavesEndBlock).HasColumnName("leaves_end_block");

                entity.Property(e => e.Root).HasColumnName("root");

                entity.Property(e => e.StartBlock).HasColumnName("start_block");
            });

            modelBuilder.Entity<OcritemTextViewFtsSegment>(entity =>
            {
                entity.HasKey(e => e.Blockid);

                entity.ToTable("OCRItemTextViewFts_segments");

                entity.Property(e => e.Blockid)
                    .ValueGeneratedNever()
                    .HasColumnName("blockid");

                entity.Property(e => e.Block).HasColumnName("block");
            });

            modelBuilder.Entity<OcritemTextViewFtsStat>(entity =>
            {
                entity.ToTable("OCRItemTextViewFts_stat");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Ocrline>(entity =>
            {
                entity.ToTable("OCRLine");

                entity.HasIndex(e => e.OcrlineOcritemId, "OCRLine_OCRItemId");

                entity.Property(e => e.OcrlineId)
                    .ValueGeneratedNever()
                    .HasColumnName("OCRLine_Id");

                entity.Property(e => e.OcrlineIndexOnItem).HasColumnName("OCRLine_IndexOnItem");

                entity.Property(e => e.OcrlineOcritemId).HasColumnName("OCRLine_OCRItemId");

                entity.HasOne(d => d.OcrlineOcritem)
                    .WithMany(p => p.Ocrlines)
                    .HasForeignKey(d => d.OcrlineOcritemId);
            });

            modelBuilder.Entity<Ocrword>(entity =>
            {
                entity.ToTable("OCRWord");

                entity.HasIndex(e => e.OcrwordOcrlineId, "OCRWord_OCRLineId");

                entity.HasIndex(e => e.OcrwordText, "OCRWord_Text");

                entity.Property(e => e.OcrwordId)
                    .ValueGeneratedNever()
                    .HasColumnName("OCRWord_Id");

                entity.Property(e => e.OcrwordHeight).HasColumnName("OCRWord_Height");

                entity.Property(e => e.OcrwordIndexOnLine).HasColumnName("OCRWord_IndexOnLine");

                entity.Property(e => e.OcrwordOcrlineId).HasColumnName("OCRWord_OCRLineId");

                entity.Property(e => e.OcrwordText)
                    .IsRequired()
                    .HasColumnName("OCRWord_Text");

                entity.Property(e => e.OcrwordWidth).HasColumnName("OCRWord_Width");

                entity.Property(e => e.OcrwordX).HasColumnName("OCRWord_X");

                entity.Property(e => e.OcrwordY).HasColumnName("OCRWord_Y");

                entity.HasOne(d => d.OcrwordOcrline)
                    .WithMany(p => p.Ocrwords)
                    .HasForeignKey(d => d.OcrwordOcrlineId);
            });

            modelBuilder.Entity<OneDriveStorageAndUpsellInfo>(entity =>
            {
                entity.HasKey(e => e.OneDriveStorageAndUpsellInfoUserId);

                entity.ToTable("OneDriveStorageAndUpsellInfo");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoUserId).HasColumnName("OneDriveStorageAndUpsellInfo_UserId");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoCountOfClickUpsellLink)
                    .HasColumnName("OneDriveStorageAndUpsellInfo_CountOfClickUpsellLink")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoCurrentPlan)
                    .HasColumnName("OneDriveStorageAndUpsellInfo_CurrentPlan")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoIsHighestPlan)
                    .HasColumnName("OneDriveStorageAndUpsellInfo_IsHighestPlan")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoLastGetQuotaTime)
                    .HasColumnName("OneDriveStorageAndUpsellInfo_LastGetQuotaTime")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoLastGetUpsellInfoTime)
                    .HasColumnName("OneDriveStorageAndUpsellInfo_LastGetUpsellInfoTime")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoPaidSpace)
                    .HasColumnName("OneDriveStorageAndUpsellInfo_PaidSpace")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoPriceForDisplay).HasColumnName("OneDriveStorageAndUpsellInfo_PriceForDisplay");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoQuotaStatus)
                    .HasColumnName("OneDriveStorageAndUpsellInfo_QuotaStatus")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoTotalSpace)
                    .HasColumnName("OneDriveStorageAndUpsellInfo_TotalSpace")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoTotalSpaceForDisplay).HasColumnName("OneDriveStorageAndUpsellInfo_TotalSpaceForDisplay");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoUpsellState)
                    .HasColumnName("OneDriveStorageAndUpsellInfo_UpsellState")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoUpsellUrl).HasColumnName("OneDriveStorageAndUpsellInfo_UpsellUrl");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoUsedSpace)
                    .HasColumnName("OneDriveStorageAndUpsellInfo_UsedSpace")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoUsedSpaceForDisplay).HasColumnName("OneDriveStorageAndUpsellInfo_UsedSpaceForDisplay");
            });

            modelBuilder.Entity<PendingCloudAlbumDelete>(entity =>
            {
                entity.ToTable("PendingCloudAlbumDelete");

                entity.HasIndex(e => new { e.PendingCloudAlbumDeletePhotosCloudId, e.PendingCloudAlbumDeleteSourceId }, "PendingCloudAlbumDelete_PhotosCloudIdSourceId")
                    .IsUnique();

                entity.HasIndex(e => e.PendingCloudAlbumDeleteSourceId, "PendingCloudAlbumDelete_SourceId");

                entity.Property(e => e.PendingCloudAlbumDeleteId)
                    .ValueGeneratedNever()
                    .HasColumnName("PendingCloudAlbumDelete_Id");

                entity.Property(e => e.PendingCloudAlbumDeletePhotosCloudId).HasColumnName("PendingCloudAlbumDelete_PhotosCloudId");

                entity.Property(e => e.PendingCloudAlbumDeleteSourceId).HasColumnName("PendingCloudAlbumDelete_SourceId");

                entity.HasOne(d => d.PendingCloudAlbumDeleteSource)
                    .WithMany(p => p.PendingCloudAlbumDeletes)
                    .HasForeignKey(d => d.PendingCloudAlbumDeleteSourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PendingUploadItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PendingUploadItem");

                entity.HasIndex(e => e.PendingUploadItemAlbumId, "PendingUploadItem_AlbumId");

                entity.HasIndex(e => e.PendingUploadItemItemId, "PendingUploadItem_ItemAlbum");

                entity.HasIndex(e => e.PendingUploadItemSourceId, "PendingUploadItem_SourceId");

                entity.Property(e => e.PendingUploadItemActionAfterUpload).HasColumnName("PendingUploadItem_ActionAfterUpload");

                entity.Property(e => e.PendingUploadItemAlbumId).HasColumnName("PendingUploadItem_AlbumId");

                entity.Property(e => e.PendingUploadItemAlbumRemoteId).HasColumnName("PendingUploadItem_AlbumRemoteId");

                entity.Property(e => e.PendingUploadItemItemId).HasColumnName("PendingUploadItem_ItemId");

                entity.Property(e => e.PendingUploadItemNeedsAeupload)
                    .HasColumnName("PendingUploadItem_NeedsAEUpload")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PendingUploadItemResourceId).HasColumnName("PendingUploadItem_ResourceId");

                entity.Property(e => e.PendingUploadItemResourceIdSourceType)
                    .HasColumnName("PendingUploadItem_ResourceIdSourceType")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.PendingUploadItemResult).HasColumnName("PendingUploadItem_Result");

                entity.Property(e => e.PendingUploadItemResumableUploadUrl).HasColumnName("PendingUploadItem_ResumableUploadUrl");

                entity.Property(e => e.PendingUploadItemSourceId).HasColumnName("PendingUploadItem_SourceId");

                entity.Property(e => e.PendingUploadItemType)
                    .HasColumnName("PendingUploadItem_Type")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PendingUploadItemUploadSessionUrl).HasColumnName("PendingUploadItem_UploadSessionUrl");

                entity.HasOne(d => d.PendingUploadItemAlbum)
                    .WithMany()
                    .HasForeignKey(d => d.PendingUploadItemAlbumId);

                entity.HasOne(d => d.PendingUploadItemItem)
                    .WithMany()
                    .HasForeignKey(d => d.PendingUploadItemItemId);

                entity.HasOne(d => d.PendingUploadItemSource)
                    .WithMany()
                    .HasForeignKey(d => d.PendingUploadItemSourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.HasIndex(e => e.PersonBestFaceId, "Person_BestFaceId");

                entity.HasIndex(e => e.PersonName, "Person_Name");

                entity.HasIndex(e => e.PersonRank, "Person_Rank");

                entity.HasIndex(e => e.PersonRecalcBestFace, "Person_RecalcBestFace");

                entity.HasIndex(e => e.PersonRecalcRank, "Person_RecalcRank");

                entity.HasIndex(e => e.PersonSafeBestFaceId, "Person_SafeBestFaceId");

                entity.Property(e => e.PersonId)
                    .ValueGeneratedNever()
                    .HasColumnName("Person_Id");

                entity.Property(e => e.PersonBestFaceId).HasColumnName("Person_BestFaceId");

                entity.Property(e => e.PersonCid).HasColumnName("Person_CID");

                entity.Property(e => e.PersonEmailDigest).HasColumnName("Person_EmailDigest");

                entity.Property(e => e.PersonItemCount)
                    .HasColumnName("Person_ItemCount")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PersonName).HasColumnName("Person_Name");

                entity.Property(e => e.PersonRank).HasColumnName("Person_Rank");

                entity.Property(e => e.PersonRecalcBestFace)
                    .HasColumnName("Person_RecalcBestFace")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.PersonRecalcRank)
                    .HasColumnName("Person_RecalcRank")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.PersonRepresentativeThumbStream).HasColumnName("Person_RepresentativeThumbStream");

                entity.Property(e => e.PersonSafeBestFaceId).HasColumnName("Person_SafeBestFaceId");

                entity.Property(e => e.PersonServiceId).HasColumnName("Person_ServiceId");

                entity.Property(e => e.PersonSourceAndId).HasColumnName("Person_SourceAndId");

                entity.HasOne(d => d.PersonBestFace)
                    .WithMany(p => p.PersonPersonBestFaces)
                    .HasForeignKey(d => d.PersonBestFaceId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.PersonSafeBestFace)
                    .WithMany(p => p.PersonPersonSafeBestFaces)
                    .HasForeignKey(d => d.PersonSafeBestFaceId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<PersonFt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.PersonName).HasColumnName("Person_Name");
            });

            modelBuilder.Entity<PersonFtsDocsize>(entity =>
            {
                entity.HasKey(e => e.Docid);

                entity.ToTable("PersonFts_docsize");

                entity.Property(e => e.Docid)
                    .ValueGeneratedNever()
                    .HasColumnName("docid");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<PersonFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });

                entity.ToTable("PersonFts_segdir");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.EndBlock).HasColumnName("end_block");

                entity.Property(e => e.LeavesEndBlock).HasColumnName("leaves_end_block");

                entity.Property(e => e.Root).HasColumnName("root");

                entity.Property(e => e.StartBlock).HasColumnName("start_block");
            });

            modelBuilder.Entity<PersonFtsSegment>(entity =>
            {
                entity.HasKey(e => e.Blockid);

                entity.ToTable("PersonFts_segments");

                entity.Property(e => e.Blockid)
                    .ValueGeneratedNever()
                    .HasColumnName("blockid");

                entity.Property(e => e.Block).HasColumnName("block");
            });

            modelBuilder.Entity<PersonFtsStat>(entity =>
            {
                entity.ToTable("PersonFts_stat");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<PinnedSearch>(entity =>
            {
                entity.ToTable("PinnedSearch");

                entity.HasIndex(e => e.PinnedSearchPinnedDate, "PinnedSearch_PinnedDate");

                entity.Property(e => e.PinnedSearchId)
                    .ValueGeneratedNever()
                    .HasColumnName("PinnedSearch_Id");

                entity.Property(e => e.PinnedSearchPinnedDate).HasColumnName("PinnedSearch_PinnedDate");

                entity.Property(e => e.PinnedSearchSearchText).HasColumnName("PinnedSearch_SearchText");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.HasIndex(e => e.ProjectAlbumId, "Project_AlbumId")
                    .IsUnique();

                entity.HasIndex(e => e.ProjectGuid, "Project_Guid")
                    .IsUnique();

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("Project_Id");

                entity.Property(e => e.ProjectAgmState).HasColumnName("Project_AgmState");

                entity.Property(e => e.ProjectAlbumId).HasColumnName("Project_AlbumId");

                entity.Property(e => e.ProjectDateCreated).HasColumnName("Project_DateCreated");

                entity.Property(e => e.ProjectDuration).HasColumnName("Project_Duration");

                entity.Property(e => e.ProjectGuid)
                    .IsRequired()
                    .HasColumnName("Project_Guid");

                entity.Property(e => e.ProjectName).HasColumnName("Project_Name");

                entity.Property(e => e.ProjectRpmState).HasColumnName("Project_RpmState");

                entity.Property(e => e.ProjectStoryBuilderProjectState).HasColumnName("Project_StoryBuilderProjectState");

                entity.HasOne(d => d.ProjectAlbum)
                    .WithOne(p => p.Project)
                    .HasForeignKey<Project>(d => d.ProjectAlbumId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RemoteAlbum>(entity =>
            {
                entity.HasKey(e => e.RemoteAlbumAlbumId);

                entity.ToTable("RemoteAlbum");

                entity.HasIndex(e => e.RemoteAlbumPhotosCloudId, "RemoteAlbum_PhotosCloudId");

                entity.Property(e => e.RemoteAlbumAlbumId)
                    .ValueGeneratedNever()
                    .HasColumnName("RemoteAlbum_AlbumId");

                entity.Property(e => e.RemoteAlbumAlbumType)
                    .HasColumnName("RemoteAlbum_AlbumType")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.RemoteAlbumCoverDuringUpload).HasColumnName("RemoteAlbum_CoverDuringUpload");

                entity.Property(e => e.RemoteAlbumEtag).HasColumnName("RemoteAlbum_ETag");

                entity.Property(e => e.RemoteAlbumGenericViewUrl).HasColumnName("RemoteAlbum_GenericViewUrl");

                entity.Property(e => e.RemoteAlbumPhotosCloudId).HasColumnName("RemoteAlbum_PhotosCloudId");

                entity.Property(e => e.RemoteAlbumPresentAtSync)
                    .HasColumnName("RemoteAlbum_PresentAtSync")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.RemoteAlbumRemoteId).HasColumnName("RemoteAlbum_RemoteId");

                entity.HasOne(d => d.RemoteAlbumAlbum)
                    .WithOne(p => p.RemoteAlbum)
                    .HasForeignKey<RemoteAlbum>(d => d.RemoteAlbumAlbumId);
            });

            modelBuilder.Entity<RemoteItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RemoteItem");

                entity.HasIndex(e => e.RemoteItemFolderId, "RemoteItem_FolderId")
                    .IsUnique();

                entity.HasIndex(e => e.RemoteItemItemId, "RemoteItem_ItemId")
                    .IsUnique();

                entity.HasIndex(e => e.RemoteItemRemoteId, "RemoteItem_RemoteId");

                entity.Property(e => e.RemoteItemDownloadUrl).HasColumnName("RemoteItem_DownloadUrl");

                entity.Property(e => e.RemoteItemFolderId).HasColumnName("RemoteItem_FolderId");

                entity.Property(e => e.RemoteItemItemId).HasColumnName("RemoteItem_ItemId");

                entity.Property(e => e.RemoteItemPhotosCloudId).HasColumnName("RemoteItem_PhotosCloudId");

                entity.Property(e => e.RemoteItemPresentAtSync)
                    .HasColumnName("RemoteItem_PresentAtSync")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.RemoteItemRemoteId).HasColumnName("RemoteItem_RemoteId");

                entity.Property(e => e.RemoteItemRemoteParentId).HasColumnName("RemoteItem_RemoteParentId");

                entity.HasOne(d => d.RemoteItemFolder)
                    .WithOne()
                    .HasForeignKey<RemoteItem>(d => d.RemoteItemFolderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.RemoteItemItem)
                    .WithOne()
                    .HasForeignKey<RemoteItem>(d => d.RemoteItemItemId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RemoteProject>(entity =>
            {
                entity.ToTable("RemoteProject");

                entity.HasIndex(e => e.RemoteProjectPhotosCloudId, "RemoteProject_PhotosCloudId");

                entity.HasIndex(e => e.RemoteProjectProjectGuid, "RemoteProject_ProjectGuid");

                entity.Property(e => e.RemoteProjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("RemoteProject_Id");

                entity.Property(e => e.RemoteProjectDateLastSynced).HasColumnName("RemoteProject_DateLastSynced");

                entity.Property(e => e.RemoteProjectEtag).HasColumnName("RemoteProject_ETag");

                entity.Property(e => e.RemoteProjectMigratedFromCloud).HasColumnName("RemoteProject_MigratedFromCloud");

                entity.Property(e => e.RemoteProjectPhotosCloudId).HasColumnName("RemoteProject_PhotosCloudId");

                entity.Property(e => e.RemoteProjectProjectGuid)
                    .IsRequired()
                    .HasColumnName("RemoteProject_ProjectGuid");

                entity.Property(e => e.RemoteProjectPublishState).HasColumnName("RemoteProject_PublishState");

                entity.HasOne(d => d.RemoteProjectProjectGu)
                    .WithMany(p => p.RemoteProjects)
                    .HasPrincipalKey(p => p.ProjectGuid)
                    .HasForeignKey(d => d.RemoteProjectProjectGuid);
            });

            modelBuilder.Entity<RemoteThumbnail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RemoteThumbnail");

                entity.HasIndex(e => e.RemoteThumbnailItemId, "RemoteThumbnail_ItemId");

                entity.Property(e => e.RemoteThumbnailHeight).HasColumnName("RemoteThumbnail_Height");

                entity.Property(e => e.RemoteThumbnailItemId).HasColumnName("RemoteThumbnail_ItemId");

                entity.Property(e => e.RemoteThumbnailUrl).HasColumnName("RemoteThumbnail_Url");

                entity.Property(e => e.RemoteThumbnailWidth).HasColumnName("RemoteThumbnail_Width");

                entity.HasOne(d => d.RemoteThumbnailItem)
                    .WithMany()
                    .HasForeignKey(d => d.RemoteThumbnailItemId);
            });

            modelBuilder.Entity<SalientRect>(entity =>
            {
                entity.ToTable("SalientRect");

                entity.HasIndex(e => e.SalientRectItemId, "SalientRect_ItemId");

                entity.Property(e => e.SalientRectId)
                    .ValueGeneratedNever()
                    .HasColumnName("SalientRect_Id");

                entity.Property(e => e.SalientRectContainsFaces).HasColumnName("SalientRect_ContainsFaces");

                entity.Property(e => e.SalientRectIsFaceUnionRect).HasColumnName("SalientRect_IsFaceUnionRect");

                entity.Property(e => e.SalientRectItemId).HasColumnName("SalientRect_ItemId");

                entity.Property(e => e.SalientRectRectHeight).HasColumnName("SalientRect_Rect_Height");

                entity.Property(e => e.SalientRectRectLeft).HasColumnName("SalientRect_Rect_Left");

                entity.Property(e => e.SalientRectRectTop).HasColumnName("SalientRect_Rect_Top");

                entity.Property(e => e.SalientRectRectWidth).HasColumnName("SalientRect_Rect_Width");

                entity.Property(e => e.SalientRectSharpness).HasColumnName("SalientRect_Sharpness");

                entity.HasOne(d => d.SalientRectItem)
                    .WithMany(p => p.SalientRects)
                    .HasForeignKey(d => d.SalientRectItemId);
            });

            modelBuilder.Entity<SearchAnalysisItemPriority>(entity =>
            {
                entity.ToTable("SearchAnalysisItemPriority");

                entity.HasIndex(e => e.SearchAnalysisItemPriorityItemId, "SearchAnalysisItemPriority_ItemId")
                    .IsUnique();

                entity.Property(e => e.SearchAnalysisItemPriorityId)
                    .ValueGeneratedNever()
                    .HasColumnName("SearchAnalysisItemPriority_Id");

                entity.Property(e => e.SearchAnalysisItemPriorityItemId).HasColumnName("SearchAnalysisItemPriority_ItemId");

                entity.Property(e => e.SearchAnalysisItemPriorityPriority).HasColumnName("SearchAnalysisItemPriority_Priority");

                entity.HasOne(d => d.SearchAnalysisItemPriorityItem)
                    .WithOne(p => p.SearchAnalysisItemPriority)
                    .HasForeignKey<SearchAnalysisItemPriority>(d => d.SearchAnalysisItemPriorityItemId);
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.ToTable("Source");

                entity.Property(e => e.SourceId)
                    .ValueGeneratedNever()
                    .HasColumnName("Source_Id");

                entity.Property(e => e.SourceDeltaSyncToken).HasColumnName("Source_DeltaSyncToken");

                entity.Property(e => e.SourceFullSyncCompleted).HasColumnName("Source_FullSyncCompleted");

                entity.Property(e => e.SourceItemsResyncing)
                    .HasColumnName("Source_ItemsResyncing")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SourceOdsyncThrottleStartTime).HasColumnName("Source_ODSyncThrottleStartTime");

                entity.Property(e => e.SourcePhotosCloudUserId).HasColumnName("Source_PhotosCloudUserId");

                entity.Property(e => e.SourceSignOutTime)
                    .HasColumnName("Source_SignOutTime")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SourceStatus).HasColumnName("Source_Status");

                entity.Property(e => e.SourceType).HasColumnName("Source_Type");

                entity.Property(e => e.SourceUserEnabled).HasColumnName("Source_UserEnabled");

                entity.Property(e => e.SourceUserId).HasColumnName("Source_UserId");

                entity.Property(e => e.SourceUserName).HasColumnName("Source_UserName");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.HasIndex(e => e.TagResourceId, "Tag_ResourceId");

                entity.Property(e => e.TagId)
                    .ValueGeneratedNever()
                    .HasColumnName("Tag_Id");

                entity.Property(e => e.TagCreatedDate).HasColumnName("Tag_CreatedDate");

                entity.Property(e => e.TagResourceId).HasColumnName("Tag_ResourceId");
            });

            modelBuilder.Entity<TagVariant>(entity =>
            {
                entity.ToTable("TagVariant");

                entity.HasIndex(e => e.TagVariantTagResourceId, "TagVariant_TagResourceId");

                entity.HasIndex(e => e.TagVariantText, "TagVariant_Text");

                entity.Property(e => e.TagVariantId)
                    .ValueGeneratedNever()
                    .HasColumnName("TagVariant_Id");

                entity.Property(e => e.TagVariantIsPrimary).HasColumnName("TagVariant_IsPrimary");

                entity.Property(e => e.TagVariantTagResourceId).HasColumnName("TagVariant_TagResourceId");

                entity.Property(e => e.TagVariantText).HasColumnName("TagVariant_Text");
            });

            modelBuilder.Entity<TagVariantFt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.TagVariantText).HasColumnName("TagVariant_Text");
            });

            modelBuilder.Entity<TagVariantFtsDocsize>(entity =>
            {
                entity.HasKey(e => e.Docid);

                entity.ToTable("TagVariantFts_docsize");

                entity.Property(e => e.Docid)
                    .ValueGeneratedNever()
                    .HasColumnName("docid");

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<TagVariantFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });

                entity.ToTable("TagVariantFts_segdir");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.EndBlock).HasColumnName("end_block");

                entity.Property(e => e.LeavesEndBlock).HasColumnName("leaves_end_block");

                entity.Property(e => e.Root).HasColumnName("root");

                entity.Property(e => e.StartBlock).HasColumnName("start_block");
            });

            modelBuilder.Entity<TagVariantFtsSegment>(entity =>
            {
                entity.HasKey(e => e.Blockid);

                entity.ToTable("TagVariantFts_segments");

                entity.Property(e => e.Blockid)
                    .ValueGeneratedNever()
                    .HasColumnName("blockid");

                entity.Property(e => e.Block).HasColumnName("block");
            });

            modelBuilder.Entity<TagVariantFtsStat>(entity =>
            {
                entity.ToTable("TagVariantFts_stat");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<UserActionAlbumView>(entity =>
            {
                entity.ToTable("UserActionAlbumView");

                entity.HasIndex(e => e.UserActionAlbumViewAlbumId, "UserActionAlbumView_AlbumId");

                entity.Property(e => e.UserActionAlbumViewId).HasColumnName("UserActionAlbumView_Id");

                entity.Property(e => e.UserActionAlbumViewActionOrigin).HasColumnName("UserActionAlbumView_ActionOrigin");

                entity.Property(e => e.UserActionAlbumViewAlbumId).HasColumnName("UserActionAlbumView_AlbumId");

                entity.Property(e => e.UserActionAlbumViewDate).HasColumnName("UserActionAlbumView_Date");

                entity.HasOne(d => d.UserActionAlbumViewAlbum)
                    .WithMany(p => p.UserActionAlbumViews)
                    .HasForeignKey(d => d.UserActionAlbumViewAlbumId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UserActionImport>(entity =>
            {
                entity.ToTable("UserActionImport");

                entity.Property(e => e.UserActionImportId).HasColumnName("UserActionImport_Id");

                entity.Property(e => e.UserActionImportActionOrigin).HasColumnName("UserActionImport_ActionOrigin");

                entity.Property(e => e.UserActionImportDate).HasColumnName("UserActionImport_Date");

                entity.Property(e => e.UserActionImportDestination)
                    .IsRequired()
                    .HasColumnName("UserActionImport_Destination");

                entity.Property(e => e.UserActionImportManufacturer)
                    .IsRequired()
                    .HasColumnName("UserActionImport_Manufacturer");

                entity.Property(e => e.UserActionImportModel)
                    .IsRequired()
                    .HasColumnName("UserActionImport_Model");

                entity.Property(e => e.UserActionImportSessionId).HasColumnName("UserActionImport_SessionId");

                entity.Property(e => e.UserActionImportTotalCount).HasColumnName("UserActionImport_TotalCount");
            });

            modelBuilder.Entity<UserActionLaunch>(entity =>
            {
                entity.ToTable("UserActionLaunch");

                entity.Property(e => e.UserActionLaunchId).HasColumnName("UserActionLaunch_Id");

                entity.Property(e => e.UserActionLaunchDate).HasColumnName("UserActionLaunch_Date");

                entity.Property(e => e.UserActionLaunchEntryPoint).HasColumnName("UserActionLaunch_EntryPoint");
            });

            modelBuilder.Entity<UserActionPrint>(entity =>
            {
                entity.ToTable("UserActionPrint");

                entity.HasIndex(e => e.UserActionPrintItemId, "UserActionPrint_ItemId");

                entity.Property(e => e.UserActionPrintId).HasColumnName("UserActionPrint_Id");

                entity.Property(e => e.UserActionPrintActionOrigin).HasColumnName("UserActionPrint_ActionOrigin");

                entity.Property(e => e.UserActionPrintDate).HasColumnName("UserActionPrint_Date");

                entity.Property(e => e.UserActionPrintItemId).HasColumnName("UserActionPrint_ItemId");

                entity.HasOne(d => d.UserActionPrintItem)
                    .WithMany(p => p.UserActionPrints)
                    .HasForeignKey(d => d.UserActionPrintItemId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UserActionSearch>(entity =>
            {
                entity.ToTable("UserActionSearch");

                entity.Property(e => e.UserActionSearchId).HasColumnName("UserActionSearch_Id");

                entity.Property(e => e.UserActionSearchActionOrigin).HasColumnName("UserActionSearch_ActionOrigin");

                entity.Property(e => e.UserActionSearchDate).HasColumnName("UserActionSearch_Date");

                entity.Property(e => e.UserActionSearchIndexingWasComplete).HasColumnName("UserActionSearch_IndexingWasComplete");

                entity.Property(e => e.UserActionSearchJson)
                    .IsRequired()
                    .HasColumnName("UserActionSearch_Json");

                entity.Property(e => e.UserActionSearchNumberOfResults).HasColumnName("UserActionSearch_NumberOfResults");

                entity.Property(e => e.UserActionSearchRequestOrigin).HasColumnName("UserActionSearch_RequestOrigin");

                entity.Property(e => e.UserActionSearchTextbox)
                    .IsRequired()
                    .HasColumnName("UserActionSearch_Textbox");
            });

            modelBuilder.Entity<UserActionShare>(entity =>
            {
                entity.ToTable("UserActionShare");

                entity.HasIndex(e => e.UserActionShareItemId, "UserActionShare_ItemId");

                entity.Property(e => e.UserActionShareId).HasColumnName("UserActionShare_Id");

                entity.Property(e => e.UserActionShareActionOrigin).HasColumnName("UserActionShare_ActionOrigin");

                entity.Property(e => e.UserActionShareDate).HasColumnName("UserActionShare_Date");

                entity.Property(e => e.UserActionShareItemId).HasColumnName("UserActionShare_ItemId");

                entity.Property(e => e.UserActionShareResult).HasColumnName("UserActionShare_Result");

                entity.Property(e => e.UserActionShareTarget)
                    .IsRequired()
                    .HasColumnName("UserActionShare_Target");

                entity.HasOne(d => d.UserActionShareItem)
                    .WithMany(p => p.UserActionShares)
                    .HasForeignKey(d => d.UserActionShareItemId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UserActionSlideshow>(entity =>
            {
                entity.ToTable("UserActionSlideshow");

                entity.HasIndex(e => e.UserActionSlideshowAlbumId, "UserActionSlideshow_AlbumId");

                entity.HasIndex(e => e.UserActionSlideshowItemId, "UserActionSlideshow_ItemId");

                entity.Property(e => e.UserActionSlideshowId).HasColumnName("UserActionSlideshow_Id");

                entity.Property(e => e.UserActionSlideshowActionOrigin).HasColumnName("UserActionSlideshow_ActionOrigin");

                entity.Property(e => e.UserActionSlideshowAlbumId).HasColumnName("UserActionSlideshow_AlbumId");

                entity.Property(e => e.UserActionSlideshowDate).HasColumnName("UserActionSlideshow_Date");

                entity.Property(e => e.UserActionSlideshowItemId).HasColumnName("UserActionSlideshow_ItemId");

                entity.HasOne(d => d.UserActionSlideshowAlbum)
                    .WithMany(p => p.UserActionSlideshows)
                    .HasForeignKey(d => d.UserActionSlideshowAlbumId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.UserActionSlideshowItem)
                    .WithMany(p => p.UserActionSlideshows)
                    .HasForeignKey(d => d.UserActionSlideshowItemId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UserActionView>(entity =>
            {
                entity.ToTable("UserActionView");

                entity.HasIndex(e => e.UserActionViewItemId, "UserActionView_ItemId");

                entity.Property(e => e.UserActionViewId).HasColumnName("UserActionView_Id");

                entity.Property(e => e.UserActionViewActionOrigin).HasColumnName("UserActionView_ActionOrigin");

                entity.Property(e => e.UserActionViewDate).HasColumnName("UserActionView_Date");

                entity.Property(e => e.UserActionViewItemId).HasColumnName("UserActionView_ItemId");

                entity.HasOne(d => d.UserActionViewItem)
                    .WithMany(p => p.UserActionViews)
                    .HasForeignKey(d => d.UserActionViewItemId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<VideoFaceOccurrence>(entity =>
            {
                entity.ToTable("VideoFaceOccurrence");

                entity.HasIndex(e => e.VideoFaceOccurrenceFaceId, "VideoFaceOccurrence_FaceId");

                entity.Property(e => e.VideoFaceOccurrenceId)
                    .ValueGeneratedNever()
                    .HasColumnName("VideoFaceOccurrence_Id");

                entity.Property(e => e.VideoFaceOccurrenceBeginFrame).HasColumnName("VideoFaceOccurrence_BeginFrame");

                entity.Property(e => e.VideoFaceOccurrenceEndFrame).HasColumnName("VideoFaceOccurrence_EndFrame");

                entity.Property(e => e.VideoFaceOccurrenceFaceFrame).HasColumnName("VideoFaceOccurrence_FaceFrame");

                entity.Property(e => e.VideoFaceOccurrenceFaceId).HasColumnName("VideoFaceOccurrence_FaceId");

                entity.HasOne(d => d.VideoFaceOccurrenceFace)
                    .WithMany(p => p.VideoFaceOccurrences)
                    .HasForeignKey(d => d.VideoFaceOccurrenceFaceId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            

        }
	}
}

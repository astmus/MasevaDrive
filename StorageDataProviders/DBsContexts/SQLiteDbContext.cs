using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using StorageDataProviders.SQLiteModels;

#nullable disable

namespace StorageDataProviders
{
    public partial class SQLiteDbContext : DbContext
    {
        public SQLiteDbContext()
        {
        }

        public SQLiteDbContext(DbContextOptions<SQLiteDbContext> options)
            : base(options)
        {
            
        }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Filename=e:\\StorageDB\\MediaDb.v1.sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(e => e.AlbumId).ValueGeneratedNever();

                entity.Property(e => e.AlbumCreationType).HasDefaultValueSql("0");

                entity.Property(e => e.AlbumOrder).HasDefaultValueSql("0");

                entity.Property(e => e.AlbumPendingTelemetryUploadState).HasDefaultValueSql("0");

                entity.Property(e => e.AlbumSentTelemetryUploadState).HasDefaultValueSql("0");

                entity.HasOne(d => d.AlbumCoverItem)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.AlbumCoverItemId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.AlbumSourceNavigation)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.AlbumSourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AlbumNameFtsDocsize>(entity =>
            {
                entity.Property(e => e.Docid).ValueGeneratedNever();
            });

            modelBuilder.Entity<AlbumNameFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });
            });

            modelBuilder.Entity<AlbumNameFtsSegment>(entity =>
            {
                entity.Property(e => e.Blockid).ValueGeneratedNever();
            });

            modelBuilder.Entity<AlbumNameFtsStat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AppGlobalState>(entity =>
            {
                entity.Property(e => e.AppGlobalStateCachedLocalCollectionSize).HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateDateLastLocationLookupReady).HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateExistingItemsSyncStarted).HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateFaceRecognitionConsentDate).HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateImportBadgeDisplayState).HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateNewAlbumsBadgeCount).HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateOneDriveAlbumsResyncing).HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateOneDriveItemsResyncing).HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateRichMediaGrovelVersion).HasDefaultValueSql("0");

                entity.Property(e => e.AppGlobalStateXboxLiveItemsResyncing).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<ApplicationName>(entity =>
            {
                entity.Property(e => e.ApplicationNameId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Audio>(entity =>
            {
                entity.Property(e => e.AudioId).ValueGeneratedNever();
            });

            modelBuilder.Entity<BackgroundTaskTelemetry>(entity =>
            {
                entity.Property(e => e.BackgroundTaskTelemetryMaxTime).HasDefaultValueSql("0");

                entity.Property(e => e.BackgroundTaskTelemetryMinTime).HasDefaultValueSql("0");

                entity.Property(e => e.BackgroundTaskTelemetryTotalTime).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Cache>(entity =>
            {
                entity.Property(e => e.CacheId).ValueGeneratedNever();

                entity.HasOne(d => d.CacheItem)
                    .WithOne(p => p.Cache)
                    .HasForeignKey<Cache>(d => d.CacheItemId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<CameraManufacturer>(entity =>
            {
                entity.Property(e => e.CameraManufacturerId).ValueGeneratedNever();
            });

            modelBuilder.Entity<CameraModel>(entity =>
            {
                entity.Property(e => e.CameraModelId).ValueGeneratedNever();
            });

            modelBuilder.Entity<CloudAlbum>(entity =>
            {
                entity.Property(e => e.CloudAlbumId).ValueGeneratedNever();

                entity.HasOne(d => d.CloudAlbumCloudAlbumDefinition)
                    .WithMany(p => p.CloudAlbums)
                    .HasForeignKey(d => d.CloudAlbumCloudAlbumDefinitionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CloudAlbumDefinition>(entity =>
            {
                entity.Property(e => e.CloudAlbumDefinitionId).ValueGeneratedNever();

                entity.Property(e => e.CloudAlbumDefinitionDateLastAlbumsMaintenance).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<DbRecoveryTaskState>(entity =>
            {
                entity.Property(e => e.DbRecoveryTaskStateId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ExcludedAlbum>(entity =>
            {
                entity.Property(e => e.ExcludedAlbumId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ExcludedFace>(entity =>
            {
                entity.Property(e => e.ExcludedFaceId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ExcludedImport>(entity =>
            {
                entity.Property(e => e.ExcludedImportId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ExcludedItemTag>(entity =>
            {
                entity.Property(e => e.ExcludedItemTagId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ExcludedLocation>(entity =>
            {
                entity.Property(e => e.ExcludedLocationId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ExcludedPerson>(entity =>
            {
                entity.Property(e => e.ExcludedPersonId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ExcludedTag>(entity =>
            {
                entity.Property(e => e.ExcludedTagId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ExtractedText>(entity =>
            {
                entity.Property(e => e.ExtractedTextId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Face>(entity =>
            {
                entity.Property(e => e.FaceId).ValueGeneratedNever();

                entity.Property(e => e.FaceIsHighQualityExemplarScore).HasDefaultValueSql("1");

                entity.HasOne(d => d.FaceFaceCluster)
                    .WithMany(p => p.Faces)
                    .HasForeignKey(d => d.FaceFaceClusterId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.FacePerson)
                    .WithMany(p => p.Faces)
                    .HasForeignKey(d => d.FacePersonId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<FaceCluster>(entity =>
            {
                entity.Property(e => e.FaceClusterId).ValueGeneratedNever();

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
            });

            modelBuilder.Entity<FileExtensionFtsDocsize>(entity =>
            {
                entity.Property(e => e.Docid).ValueGeneratedNever();
            });

            modelBuilder.Entity<FileExtensionFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });
            });

            modelBuilder.Entity<FileExtensionFtsSegment>(entity =>
            {
                entity.Property(e => e.Blockid).ValueGeneratedNever();
            });

            modelBuilder.Entity<FileExtensionFtsStat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<FilenameFtsDocsize>(entity =>
            {
                entity.Property(e => e.Docid).ValueGeneratedNever();
            });

            modelBuilder.Entity<FilenameFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });
            });

            modelBuilder.Entity<FilenameFtsSegment>(entity =>
            {
                entity.Property(e => e.Blockid).ValueGeneratedNever();
            });

            modelBuilder.Entity<FilenameFtsStat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Folder>(entity =>
            {
                entity.Property(e => e.FolderId).ValueGeneratedNever();

                entity.HasOne(d => d.FolderParentFolder)
                    .WithMany(p => p.InverseFolderParentFolder)
                    .HasForeignKey(d => d.FolderParentFolderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.FolderSourceNavigation)
                    .WithMany(p => p.Folders)
                    .HasForeignKey(d => d.FolderSourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FolderNameFtsDocsize>(entity =>
            {
                entity.Property(e => e.Docid).ValueGeneratedNever();
            });

            modelBuilder.Entity<FolderNameFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });
            });

            modelBuilder.Entity<FolderNameFtsSegment>(entity =>
            {
                entity.Property(e => e.Blockid).ValueGeneratedNever();
            });

            modelBuilder.Entity<FolderNameFtsStat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ImageAnalysis>(entity =>
            {
                entity.Property(e => e.ImageAnalysisItemId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemId).ValueGeneratedNever();

                entity.Property(e => e.ItemBurstChunk).HasDefaultValueSql("0");

                entity.Property(e => e.ItemBurstClusterNumber).HasDefaultValueSql("0");

                entity.Property(e => e.ItemDateModifiedAtLastBurstRun).HasDefaultValueSql("0");

                entity.Property(e => e.ItemLastUploadAttemptTime).HasDefaultValueSql("0");

                entity.Property(e => e.ItemModificationVersion).HasDefaultValueSql("0");

                entity.Property(e => e.ItemPendingTelemetryUploadState).HasDefaultValueSql("0");

                entity.Property(e => e.ItemRewriteSupplementaryPropertiesNeeded).HasDefaultValueSql("1");

                entity.Property(e => e.ItemSentTelemetryUploadState).HasDefaultValueSql("0");

                entity.Property(e => e.ItemUploadAttempts).HasDefaultValueSql("0");

                entity.Property(e => e.ItemUploadPendingState).HasDefaultValueSql("0");

                entity.Property(e => e.ItemUploadRequestTime).HasDefaultValueSql("0");

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

                entity.HasOne(d => d.ItemSourceNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemSourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ItemDateTaken>(entity =>
            {
                entity.Property(e => e.ItemDateTakenItemId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ItemEngineExemplar>(entity =>
            {
                entity.Property(e => e.ItemEngineExemplarId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ItemEngineStatus>(entity =>
            {
                entity.Property(e => e.ItemEngineStatusId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ItemTag>(entity =>
            {
                entity.Property(e => e.ItemTagsId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ItemVideoQuality>(entity =>
            {
                entity.Property(e => e.ItemVideoQualityId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ItemVideoTag>(entity =>
            {
                entity.Property(e => e.ItemVideoTagsId).ValueGeneratedNever();
            });

            modelBuilder.Entity<LiveTile>(entity =>
            {
                entity.Property(e => e.LiveTileItemId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).ValueGeneratedNever();

                entity.Property(e => e.LocationItemsCountExcDupes).HasDefaultValueSql("0");

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
                entity.Property(e => e.LocationCountryId).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationCountryFtsDocsize>(entity =>
            {
                entity.Property(e => e.Docid).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationCountryFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });
            });

            modelBuilder.Entity<LocationCountryFtsSegment>(entity =>
            {
                entity.Property(e => e.Blockid).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationCountryFtsStat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationDistrict>(entity =>
            {
                entity.Property(e => e.LocationDistrictId).ValueGeneratedNever();

                entity.HasOne(d => d.LocationDistrictLocationRegion)
                    .WithMany(p => p.LocationDistricts)
                    .HasForeignKey(d => d.LocationDistrictLocationRegionId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<LocationDistrictFtsDocsize>(entity =>
            {
                entity.Property(e => e.Docid).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationDistrictFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });
            });

            modelBuilder.Entity<LocationDistrictFtsSegment>(entity =>
            {
                entity.Property(e => e.Blockid).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationDistrictFtsStat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationFtsDocsize>(entity =>
            {
                entity.Property(e => e.Docid).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });
            });

            modelBuilder.Entity<LocationFtsSegment>(entity =>
            {
                entity.Property(e => e.Blockid).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationFtsStat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationGrid>(entity =>
            {
                entity.Property(e => e.LocationGridId).ValueGeneratedNever();

                entity.HasOne(d => d.LocationGridLocation)
                    .WithMany(p => p.LocationGrids)
                    .HasForeignKey(d => d.LocationGridLocationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LocationRegion>(entity =>
            {
                entity.Property(e => e.LocationRegionId).ValueGeneratedNever();

                entity.HasOne(d => d.LocationRegionLocationCountry)
                    .WithMany(p => p.LocationRegions)
                    .HasForeignKey(d => d.LocationRegionLocationCountryId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<LocationRegionFtsDocsize>(entity =>
            {
                entity.Property(e => e.Docid).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationRegionFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });
            });

            modelBuilder.Entity<LocationRegionFtsSegment>(entity =>
            {
                entity.Property(e => e.Blockid).ValueGeneratedNever();
            });

            modelBuilder.Entity<LocationRegionFtsStat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<NetworkTelemetry>(entity =>
            {
                entity.HasKey(e => new { e.NetworkTelemetrySource, e.NetworkTelemetryRequestType, e.NetworkTelemetryIsBackgroundTaskHost });
            });

            modelBuilder.Entity<Ocritem>(entity =>
            {
                entity.Property(e => e.OcritemId).ValueGeneratedNever();
            });

            modelBuilder.Entity<OcritemTextView>(entity =>
            {
                entity.ToView("OCRItemTextView");
            });

            modelBuilder.Entity<OcritemTextViewFtsDocsize>(entity =>
            {
                entity.Property(e => e.Docid).ValueGeneratedNever();
            });

            modelBuilder.Entity<OcritemTextViewFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });
            });

            modelBuilder.Entity<OcritemTextViewFtsSegment>(entity =>
            {
                entity.Property(e => e.Blockid).ValueGeneratedNever();
            });

            modelBuilder.Entity<OcritemTextViewFtsStat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Ocrline>(entity =>
            {
                entity.Property(e => e.OcrlineId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Ocrword>(entity =>
            {
                entity.Property(e => e.OcrwordId).ValueGeneratedNever();
            });

            modelBuilder.Entity<OneDriveStorageAndUpsellInfo>(entity =>
            {
                entity.Property(e => e.OneDriveStorageAndUpsellInfoCountOfClickUpsellLink).HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoCurrentPlan).HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoIsHighestPlan).HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoLastGetQuotaTime).HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoLastGetUpsellInfoTime).HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoPaidSpace).HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoQuotaStatus).HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoTotalSpace).HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoUpsellState).HasDefaultValueSql("0");

                entity.Property(e => e.OneDriveStorageAndUpsellInfoUsedSpace).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<PendingCloudAlbumDelete>(entity =>
            {
                entity.Property(e => e.PendingCloudAlbumDeleteId).ValueGeneratedNever();

                entity.HasOne(d => d.PendingCloudAlbumDeleteSource)
                    .WithMany(p => p.PendingCloudAlbumDeletes)
                    .HasForeignKey(d => d.PendingCloudAlbumDeleteSourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PendingUploadItem>(entity =>
            {
                entity.Property(e => e.PendingUploadItemNeedsAeupload).HasDefaultValueSql("0");

                entity.Property(e => e.PendingUploadItemResourceIdSourceType).HasDefaultValueSql("1");

                entity.Property(e => e.PendingUploadItemType).HasDefaultValueSql("0");

                entity.HasOne(d => d.PendingUploadItemSource)
                    .WithMany()
                    .HasForeignKey(d => d.PendingUploadItemSourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.PersonId).ValueGeneratedNever();

                entity.Property(e => e.PersonItemCount).HasDefaultValueSql("0");

                entity.Property(e => e.PersonRecalcBestFace).HasDefaultValueSql("1");

                entity.Property(e => e.PersonRecalcRank).HasDefaultValueSql("1");

                entity.HasOne(d => d.PersonBestFace)
                    .WithMany(p => p.PersonPersonBestFaces)
                    .HasForeignKey(d => d.PersonBestFaceId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.PersonSafeBestFace)
                    .WithMany(p => p.PersonPersonSafeBestFaces)
                    .HasForeignKey(d => d.PersonSafeBestFaceId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<PersonFtsDocsize>(entity =>
            {
                entity.Property(e => e.Docid).ValueGeneratedNever();
            });

            modelBuilder.Entity<PersonFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });
            });

            modelBuilder.Entity<PersonFtsSegment>(entity =>
            {
                entity.Property(e => e.Blockid).ValueGeneratedNever();
            });

            modelBuilder.Entity<PersonFtsStat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<PinnedSearch>(entity =>
            {
                entity.Property(e => e.PinnedSearchId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.ProjectId).ValueGeneratedNever();

                entity.HasOne(d => d.ProjectAlbum)
                    .WithOne(p => p.Project)
                    .HasForeignKey<Project>(d => d.ProjectAlbumId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RemoteAlbum>(entity =>
            {
                entity.Property(e => e.RemoteAlbumAlbumId).ValueGeneratedNever();

                entity.Property(e => e.RemoteAlbumAlbumType).HasDefaultValueSql("0");

                entity.Property(e => e.RemoteAlbumPresentAtSync).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<RemoteItem>(entity =>
            {
                entity.Property(e => e.RemoteItemPresentAtSync).HasDefaultValueSql("0");

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
                entity.Property(e => e.RemoteProjectId).ValueGeneratedNever();

                entity.HasOne(d => d.RemoteProjectProjectGu)
                    .WithMany(p => p.RemoteProjects)
                    .HasPrincipalKey(p => p.ProjectGuid)
                    .HasForeignKey(d => d.RemoteProjectProjectGuid);
            });

            modelBuilder.Entity<SalientRect>(entity =>
            {
                entity.Property(e => e.SalientRectId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SearchAnalysisItemPriority>(entity =>
            {
                entity.Property(e => e.SearchAnalysisItemPriorityId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.Property(e => e.SourceId).ValueGeneratedNever();

                entity.Property(e => e.SourceItemsResyncing).HasDefaultValueSql("0");

                entity.Property(e => e.SourceSignOutTime).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TagVariant>(entity =>
            {
                entity.Property(e => e.TagVariantId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TagVariantFtsDocsize>(entity =>
            {
                entity.Property(e => e.Docid).ValueGeneratedNever();
            });

            modelBuilder.Entity<TagVariantFtsSegdir>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Idx });
            });

            modelBuilder.Entity<TagVariantFtsSegment>(entity =>
            {
                entity.Property(e => e.Blockid).ValueGeneratedNever();
            });

            modelBuilder.Entity<TagVariantFtsStat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<UserActionAlbumView>(entity =>
            {
                entity.HasOne(d => d.UserActionAlbumViewAlbum)
                    .WithMany(p => p.UserActionAlbumViews)
                    .HasForeignKey(d => d.UserActionAlbumViewAlbumId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UserActionPrint>(entity =>
            {
                entity.HasOne(d => d.UserActionPrintItem)
                    .WithMany(p => p.UserActionPrints)
                    .HasForeignKey(d => d.UserActionPrintItemId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UserActionShare>(entity =>
            {
                entity.HasOne(d => d.UserActionShareItem)
                    .WithMany(p => p.UserActionShares)
                    .HasForeignKey(d => d.UserActionShareItemId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UserActionSlideshow>(entity =>
            {
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
                entity.HasOne(d => d.UserActionViewItem)
                    .WithMany(p => p.UserActionViews)
                    .HasForeignKey(d => d.UserActionViewItemId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<VideoFaceOccurrence>(entity =>
            {
                entity.Property(e => e.VideoFaceOccurrenceId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);        
    }
}

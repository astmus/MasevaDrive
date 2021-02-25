using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class OneDriveStorageAndUpsellInfo
    {
        public string OneDriveStorageAndUpsellInfoUserId { get; set; }
        public long? OneDriveStorageAndUpsellInfoTotalSpace { get; set; }
        public long? OneDriveStorageAndUpsellInfoUsedSpace { get; set; }
        public long? OneDriveStorageAndUpsellInfoIsHighestPlan { get; set; }
        public long? OneDriveStorageAndUpsellInfoPaidSpace { get; set; }
        public long? OneDriveStorageAndUpsellInfoCountOfClickUpsellLink { get; set; }
        public string OneDriveStorageAndUpsellInfoTotalSpaceForDisplay { get; set; }
        public string OneDriveStorageAndUpsellInfoUsedSpaceForDisplay { get; set; }
        public string OneDriveStorageAndUpsellInfoPriceForDisplay { get; set; }
        public string OneDriveStorageAndUpsellInfoUpsellUrl { get; set; }
        public long? OneDriveStorageAndUpsellInfoUpsellState { get; set; }
        public long? OneDriveStorageAndUpsellInfoLastGetQuotaTime { get; set; }
        public long? OneDriveStorageAndUpsellInfoLastGetUpsellInfoTime { get; set; }
        public long? OneDriveStorageAndUpsellInfoCurrentPlan { get; set; }
        public long? OneDriveStorageAndUpsellInfoQuotaStatus { get; set; }
    }
}

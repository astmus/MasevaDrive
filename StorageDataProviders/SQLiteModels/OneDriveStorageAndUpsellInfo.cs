using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("OneDriveStorageAndUpsellInfo")]
    public partial class OneDriveStorageAndUpsellInfo
    {
        [Key]
        [Column("OneDriveStorageAndUpsellInfo_UserId")]
        public string OneDriveStorageAndUpsellInfoUserId { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_TotalSpace")]
        public long? OneDriveStorageAndUpsellInfoTotalSpace { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_UsedSpace")]
        public long? OneDriveStorageAndUpsellInfoUsedSpace { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_IsHighestPlan")]
        public long? OneDriveStorageAndUpsellInfoIsHighestPlan { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_PaidSpace")]
        public long? OneDriveStorageAndUpsellInfoPaidSpace { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_CountOfClickUpsellLink")]
        public long? OneDriveStorageAndUpsellInfoCountOfClickUpsellLink { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_TotalSpaceForDisplay")]
        public string OneDriveStorageAndUpsellInfoTotalSpaceForDisplay { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_UsedSpaceForDisplay")]
        public string OneDriveStorageAndUpsellInfoUsedSpaceForDisplay { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_PriceForDisplay")]
        public string OneDriveStorageAndUpsellInfoPriceForDisplay { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_UpsellUrl")]
        public string OneDriveStorageAndUpsellInfoUpsellUrl { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_UpsellState")]
        public long? OneDriveStorageAndUpsellInfoUpsellState { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_LastGetQuotaTime")]
        public long? OneDriveStorageAndUpsellInfoLastGetQuotaTime { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_LastGetUpsellInfoTime")]
        public long? OneDriveStorageAndUpsellInfoLastGetUpsellInfoTime { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_CurrentPlan")]
        public long? OneDriveStorageAndUpsellInfoCurrentPlan { get; set; }
        [Column("OneDriveStorageAndUpsellInfo_QuotaStatus")]
        public long? OneDriveStorageAndUpsellInfoQuotaStatus { get; set; }
    }
}

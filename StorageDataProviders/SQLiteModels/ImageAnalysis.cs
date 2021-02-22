using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("ImageAnalysis")]
    public partial class ImageAnalysis
    {
        [Key]
        [Column("ImageAnalysis_ItemId")]
        public long ImageAnalysisItemId { get; set; }
        [Column("ImageAnalysis_ReDoAnalysis")]
        public long? ImageAnalysisReDoAnalysis { get; set; }
        [Column("ImageAnalysis_AnalysisModuleVersion")]
        public byte[] ImageAnalysisAnalysisModuleVersion { get; set; }
        [Column("ImageAnalysis_SaliencyScore")]
        public double? ImageAnalysisSaliencyScore { get; set; }
        [Column("ImageAnalysis_RelevantFacesPercentage")]
        public double? ImageAnalysisRelevantFacesPercentage { get; set; }
        [Column("ImageAnalysis_PortraitType")]
        public long? ImageAnalysisPortraitType { get; set; }
        [Column("ImageAnalysis_AverageSaliency")]
        public double? ImageAnalysisAverageSaliency { get; set; }
        [Column("ImageAnalysis_SaliencyNormalizer")]
        public double? ImageAnalysisSaliencyNormalizer { get; set; }
        [Column("ImageAnalysis_PortraitSize")]
        public long? ImageAnalysisPortraitSize { get; set; }
        [Column("ImageAnalysis_PhotoAspectRatio")]
        public double? ImageAnalysisPhotoAspectRatio { get; set; }
        [Column("ImageAnalysis_AverageEyeYLocation")]
        public double? ImageAnalysisAverageEyeYlocation { get; set; }
        [Column("ImageAnalysis_AverageEyeYLocationTopRow")]
        public double? ImageAnalysisAverageEyeYlocationTopRow { get; set; }
        [Column("ImageAnalysis_AverageFaceXCoordinate")]
        public double? ImageAnalysisAverageFaceXcoordinate { get; set; }
        [Column("ImageAnalysis_FramedCenter")]
        public double? ImageAnalysisFramedCenter { get; set; }
        [Column("ImageAnalysis_OpenEyeFacePercentage")]
        public double? ImageAnalysisOpenEyeFacePercentage { get; set; }
        [Column("ImageAnalysis_FacingOutOfFrame")]
        public long? ImageAnalysisFacingOutOfFrame { get; set; }
        [Column("ImageAnalysis_HasMainObjectInBackground")]
        public long? ImageAnalysisHasMainObjectInBackground { get; set; }
        [Column("ImageAnalysis_HasSharpBackground")]
        public long? ImageAnalysisHasSharpBackground { get; set; }
        [Column("ImageAnalysis_UnsharpMaskRadius")]
        public double? ImageAnalysisUnsharpMaskRadius { get; set; }
        [Column("ImageAnalysis_UnsharpMaskAmount")]
        public double? ImageAnalysisUnsharpMaskAmount { get; set; }
        [Column("ImageAnalysis_UnsharpMaskThreshold")]
        public double? ImageAnalysisUnsharpMaskThreshold { get; set; }
        [Column("ImageAnalysis_RedAdjustment")]
        public double? ImageAnalysisRedAdjustment { get; set; }
        [Column("ImageAnalysis_GreenAdjustment")]
        public double? ImageAnalysisGreenAdjustment { get; set; }
        [Column("ImageAnalysis_BlueAdjustment")]
        public double? ImageAnalysisBlueAdjustment { get; set; }
        [Column("ImageAnalysis_HighlightsAdjustment")]
        public double? ImageAnalysisHighlightsAdjustment { get; set; }
        [Column("ImageAnalysis_ShadowsAdjustment")]
        public double? ImageAnalysisShadowsAdjustment { get; set; }
        [Column("ImageAnalysis_BlackPoint")]
        public double? ImageAnalysisBlackPoint { get; set; }
        [Column("ImageAnalysis_MidPoint")]
        public double? ImageAnalysisMidPoint { get; set; }
        [Column("ImageAnalysis_WhitePoint")]
        public double? ImageAnalysisWhitePoint { get; set; }
        [Column("ImageAnalysis_ShadowsNoiseLevel")]
        public long? ImageAnalysisShadowsNoiseLevel { get; set; }
        [Column("ImageAnalysis_StraightenAngle")]
        public double? ImageAnalysisStraightenAngle { get; set; }
        [Column("ImageAnalysis_ExposureQuality")]
        public double? ImageAnalysisExposureQuality { get; set; }
        [Column("ImageAnalysis_OverExposure")]
        public double? ImageAnalysisOverExposure { get; set; }
        [Column("ImageAnalysis_UnderExposure")]
        public double? ImageAnalysisUnderExposure { get; set; }
        [Column("ImageAnalysis_ExposureBalance")]
        public double? ImageAnalysisExposureBalance { get; set; }
        [Column("ImageAnalysis_CenterExposureQuality")]
        public double? ImageAnalysisCenterExposureQuality { get; set; }
        [Column("ImageAnalysis_CenterOverExposure")]
        public double? ImageAnalysisCenterOverExposure { get; set; }
        [Column("ImageAnalysis_CenterUnderExposure")]
        public double? ImageAnalysisCenterUnderExposure { get; set; }
        [Column("ImageAnalysis_CenterExposureBalance")]
        public double? ImageAnalysisCenterExposureBalance { get; set; }
        [Column("ImageAnalysis_SaturationQuality")]
        public double? ImageAnalysisSaturationQuality { get; set; }
        [Column("ImageAnalysis_SaturationType")]
        public long? ImageAnalysisSaturationType { get; set; }
        [Column("ImageAnalysis_HueVariety")]
        public double? ImageAnalysisHueVariety { get; set; }
        [Column("ImageAnalysis_SharpnessPoint0")]
        public double? ImageAnalysisSharpnessPoint0 { get; set; }
        [Column("ImageAnalysis_SharpnessPoint1")]
        public double? ImageAnalysisSharpnessPoint1 { get; set; }
        [Column("ImageAnalysis_SharpnessPoint2")]
        public double? ImageAnalysisSharpnessPoint2 { get; set; }
        [Column("ImageAnalysis_SharpnessPoint3")]
        public double? ImageAnalysisSharpnessPoint3 { get; set; }
        [Column("ImageAnalysis_SharpnessPoint4")]
        public double? ImageAnalysisSharpnessPoint4 { get; set; }
        [Column("ImageAnalysis_NoiseLevel")]
        public long? ImageAnalysisNoiseLevel { get; set; }
        [Column("ImageAnalysis_LumaNoise")]
        public double? ImageAnalysisLumaNoise { get; set; }
        [Column("ImageAnalysis_ChromaNoise")]
        public double? ImageAnalysisChromaNoise { get; set; }
        [Column("ImageAnalysis_DetailsNoise")]
        public double? ImageAnalysisDetailsNoise { get; set; }
        [Column("ImageAnalysis_ShadowsLumaNoise")]
        public double? ImageAnalysisShadowsLumaNoise { get; set; }
        [Column("ImageAnalysis_ShadowsChromaNoise")]
        public double? ImageAnalysisShadowsChromaNoise { get; set; }
        [Column("ImageAnalysis_ShadowsDetailsNoise")]
        public double? ImageAnalysisShadowsDetailsNoise { get; set; }
        [Column("ImageAnalysis_Utility")]
        public long? ImageAnalysisUtility { get; set; }
        [Column("ImageAnalysis_HistogramBuckets")]
        public byte[] ImageAnalysisHistogramBuckets { get; set; }
        [Column("ImageAnalysis_Tone")]
        public byte[] ImageAnalysisTone { get; set; }

        [ForeignKey(nameof(ImageAnalysisItemId))]
        [InverseProperty(nameof(Item.ImageAnalysis))]
        public virtual Item ImageAnalysisItem { get; set; }
    }
}

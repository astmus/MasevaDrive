using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class ImageAnalysis
    {
        public long ImageAnalysisItemId { get; set; }
        public long? ImageAnalysisReDoAnalysis { get; set; }
        public byte[] ImageAnalysisAnalysisModuleVersion { get; set; }
        public double? ImageAnalysisSaliencyScore { get; set; }
        public double? ImageAnalysisRelevantFacesPercentage { get; set; }
        public long? ImageAnalysisPortraitType { get; set; }
        public double? ImageAnalysisAverageSaliency { get; set; }
        public double? ImageAnalysisSaliencyNormalizer { get; set; }
        public long? ImageAnalysisPortraitSize { get; set; }
        public double? ImageAnalysisPhotoAspectRatio { get; set; }
        public double? ImageAnalysisAverageEyeYlocation { get; set; }
        public double? ImageAnalysisAverageEyeYlocationTopRow { get; set; }
        public double? ImageAnalysisAverageFaceXcoordinate { get; set; }
        public double? ImageAnalysisFramedCenter { get; set; }
        public double? ImageAnalysisOpenEyeFacePercentage { get; set; }
        public long? ImageAnalysisFacingOutOfFrame { get; set; }
        public long? ImageAnalysisHasMainObjectInBackground { get; set; }
        public long? ImageAnalysisHasSharpBackground { get; set; }
        public double? ImageAnalysisUnsharpMaskRadius { get; set; }
        public double? ImageAnalysisUnsharpMaskAmount { get; set; }
        public double? ImageAnalysisUnsharpMaskThreshold { get; set; }
        public double? ImageAnalysisRedAdjustment { get; set; }
        public double? ImageAnalysisGreenAdjustment { get; set; }
        public double? ImageAnalysisBlueAdjustment { get; set; }
        public double? ImageAnalysisHighlightsAdjustment { get; set; }
        public double? ImageAnalysisShadowsAdjustment { get; set; }
        public double? ImageAnalysisBlackPoint { get; set; }
        public double? ImageAnalysisMidPoint { get; set; }
        public double? ImageAnalysisWhitePoint { get; set; }
        public long? ImageAnalysisShadowsNoiseLevel { get; set; }
        public double? ImageAnalysisStraightenAngle { get; set; }
        public double? ImageAnalysisExposureQuality { get; set; }
        public double? ImageAnalysisOverExposure { get; set; }
        public double? ImageAnalysisUnderExposure { get; set; }
        public double? ImageAnalysisExposureBalance { get; set; }
        public double? ImageAnalysisCenterExposureQuality { get; set; }
        public double? ImageAnalysisCenterOverExposure { get; set; }
        public double? ImageAnalysisCenterUnderExposure { get; set; }
        public double? ImageAnalysisCenterExposureBalance { get; set; }
        public double? ImageAnalysisSaturationQuality { get; set; }
        public long? ImageAnalysisSaturationType { get; set; }
        public double? ImageAnalysisHueVariety { get; set; }
        public double? ImageAnalysisSharpnessPoint0 { get; set; }
        public double? ImageAnalysisSharpnessPoint1 { get; set; }
        public double? ImageAnalysisSharpnessPoint2 { get; set; }
        public double? ImageAnalysisSharpnessPoint3 { get; set; }
        public double? ImageAnalysisSharpnessPoint4 { get; set; }
        public long? ImageAnalysisNoiseLevel { get; set; }
        public double? ImageAnalysisLumaNoise { get; set; }
        public double? ImageAnalysisChromaNoise { get; set; }
        public double? ImageAnalysisDetailsNoise { get; set; }
        public double? ImageAnalysisShadowsLumaNoise { get; set; }
        public double? ImageAnalysisShadowsChromaNoise { get; set; }
        public double? ImageAnalysisShadowsDetailsNoise { get; set; }
        public long? ImageAnalysisUtility { get; set; }
        public byte[] ImageAnalysisHistogramBuckets { get; set; }
        public byte[] ImageAnalysisTone { get; set; }

        public virtual Item ImageAnalysisItem { get; set; }
    }
}

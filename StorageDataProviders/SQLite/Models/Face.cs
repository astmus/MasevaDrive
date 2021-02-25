using System;
using System.Collections.Generic;

#nullable disable

namespace StorageDataProviders.SQLite.Models
{
    public partial class Face
    {
        public Face()
        {
            ExcludedFaces = new HashSet<ExcludedFace>();
            FaceClusters = new HashSet<FaceCluster>();
            FaceFeatures = new HashSet<FaceFeature>();
            PersonPersonBestFaces = new HashSet<Person>();
            PersonPersonSafeBestFaces = new HashSet<Person>();
            VideoFaceOccurrences = new HashSet<VideoFaceOccurrence>();
        }

        public long FaceId { get; set; }
        public long FaceItemId { get; set; }
        public long? FaceFaceClusterId { get; set; }
        public long? FacePersonId { get; set; }
        public long? FacePersonConfirmation { get; set; }
        public long? FaceRecoGroupId { get; set; }
        public long? FacePose { get; set; }
        public double? FaceQualityScore { get; set; }
        public double? FaceRectTop { get; set; }
        public double? FaceRectLeft { get; set; }
        public double? FaceRectWidth { get; set; }
        public double? FaceRectHeight { get; set; }
        public double? FaceViewRectTop { get; set; }
        public double? FaceViewRectLeft { get; set; }
        public double? FaceViewRectWidth { get; set; }
        public double? FaceViewRectHeight { get; set; }
        public long? FaceLeftEyeOpen { get; set; }
        public long? FaceRightEyeOpen { get; set; }
        public long? FaceLeftEyeCameraFocus { get; set; }
        public long? FaceRightEyeCameraFocus { get; set; }
        public long? FaceLeftEyeLookingAtCamera { get; set; }
        public long? FaceRightEyeLookingAtCamera { get; set; }
        public long? FaceLeftEyeSharpness { get; set; }
        public long? FaceRightEyeSharpness { get; set; }
        public long? FaceLeftEyeRedEye { get; set; }
        public long? FaceRightEyeRedEye { get; set; }
        public long? FaceMouthOpenState { get; set; }
        public long? FaceTeethVisibleState { get; set; }
        public long? FaceCutOffState { get; set; }
        public long? FaceFaceSharpness { get; set; }
        public long? FaceExpression { get; set; }
        public byte[] FaceRecoExemplar { get; set; }
        public double? FaceExemplarScore { get; set; }
        public long? FaceVersion { get; set; }
        public double? FaceSmileProbability { get; set; }
        public long? FaceIsHighQualityExemplarScore { get; set; }

        public virtual FaceCluster FaceFaceCluster { get; set; }
        public virtual Item FaceItem { get; set; }
        public virtual Person FacePerson { get; set; }
        public virtual ICollection<ExcludedFace> ExcludedFaces { get; set; }
        public virtual ICollection<FaceCluster> FaceClusters { get; set; }
        public virtual ICollection<FaceFeature> FaceFeatures { get; set; }
        public virtual ICollection<Person> PersonPersonBestFaces { get; set; }
        public virtual ICollection<Person> PersonPersonSafeBestFaces { get; set; }
        public virtual ICollection<VideoFaceOccurrence> VideoFaceOccurrences { get; set; }
    }
}

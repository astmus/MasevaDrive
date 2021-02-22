using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StorageDataProviders.SQLiteModels
{
    [Table("Face")]
    [Index(nameof(FaceExemplarScore), Name = "Face_ExemplarScore")]
    [Index(nameof(FaceFaceClusterId), Name = "Face_FaceClusterId")]
    [Index(nameof(FaceItemId), Name = "Face_ItemId")]
    [Index(nameof(FacePersonId), Name = "Face_PersonId")]
    [Index(nameof(FaceQualityScore), Name = "Face_QualityScore")]
    [Index(nameof(FaceRecoGroupId), Name = "Face_RecoGroupId")]
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

        [Key]
        [Column("Face_Id")]
        public long FaceId { get; set; }
        [Column("Face_ItemId")]
        public long FaceItemId { get; set; }
        [Column("Face_FaceClusterId")]
        public long? FaceFaceClusterId { get; set; }
        [Column("Face_PersonId")]
        public long? FacePersonId { get; set; }
        [Column("Face_PersonConfirmation")]
        public long? FacePersonConfirmation { get; set; }
        [Column("Face_RecoGroupId")]
        public long? FaceRecoGroupId { get; set; }
        [Column("Face_Pose")]
        public long? FacePose { get; set; }
        [Column("Face_QualityScore")]
        public double? FaceQualityScore { get; set; }
        [Column("Face_Rect_Top")]
        public double? FaceRectTop { get; set; }
        [Column("Face_Rect_Left")]
        public double? FaceRectLeft { get; set; }
        [Column("Face_Rect_Width")]
        public double? FaceRectWidth { get; set; }
        [Column("Face_Rect_Height")]
        public double? FaceRectHeight { get; set; }
        [Column("Face_ViewRect_Top")]
        public double? FaceViewRectTop { get; set; }
        [Column("Face_ViewRect_Left")]
        public double? FaceViewRectLeft { get; set; }
        [Column("Face_ViewRect_Width")]
        public double? FaceViewRectWidth { get; set; }
        [Column("Face_ViewRect_Height")]
        public double? FaceViewRectHeight { get; set; }
        [Column("Face_LeftEyeOpen")]
        public long? FaceLeftEyeOpen { get; set; }
        [Column("Face_RightEyeOpen")]
        public long? FaceRightEyeOpen { get; set; }
        [Column("Face_LeftEyeCameraFocus")]
        public long? FaceLeftEyeCameraFocus { get; set; }
        [Column("Face_RightEyeCameraFocus")]
        public long? FaceRightEyeCameraFocus { get; set; }
        [Column("Face_LeftEyeLookingAtCamera")]
        public long? FaceLeftEyeLookingAtCamera { get; set; }
        [Column("Face_RightEyeLookingAtCamera")]
        public long? FaceRightEyeLookingAtCamera { get; set; }
        [Column("Face_LeftEyeSharpness")]
        public long? FaceLeftEyeSharpness { get; set; }
        [Column("Face_RightEyeSharpness")]
        public long? FaceRightEyeSharpness { get; set; }
        [Column("Face_LeftEyeRedEye")]
        public long? FaceLeftEyeRedEye { get; set; }
        [Column("Face_RightEyeRedEye")]
        public long? FaceRightEyeRedEye { get; set; }
        [Column("Face_MouthOpenState")]
        public long? FaceMouthOpenState { get; set; }
        [Column("Face_TeethVisibleState")]
        public long? FaceTeethVisibleState { get; set; }
        [Column("Face_CutOffState")]
        public long? FaceCutOffState { get; set; }
        [Column("Face_FaceSharpness")]
        public long? FaceFaceSharpness { get; set; }
        [Column("Face_Expression")]
        public long? FaceExpression { get; set; }
        [Column("Face_RecoExemplar")]
        public byte[] FaceRecoExemplar { get; set; }
        [Column("Face_ExemplarScore")]
        public double? FaceExemplarScore { get; set; }
        [Column("Face_Version")]
        public long? FaceVersion { get; set; }
        [Column("Face_SmileProbability")]
        public double? FaceSmileProbability { get; set; }
        [Column("Face_IsHighQualityExemplarScore")]
        public long? FaceIsHighQualityExemplarScore { get; set; }

        [ForeignKey(nameof(FaceFaceClusterId))]
        [InverseProperty(nameof(FaceCluster.Faces))]
        public virtual FaceCluster FaceFaceCluster { get; set; }
        [ForeignKey(nameof(FaceItemId))]
        [InverseProperty(nameof(Item.Faces))]
        public virtual Item FaceItem { get; set; }
        [ForeignKey(nameof(FacePersonId))]
        [InverseProperty(nameof(Person.Faces))]
        public virtual Person FacePerson { get; set; }
        [InverseProperty(nameof(ExcludedFace.ExcludedFaceFace))]
        public virtual ICollection<ExcludedFace> ExcludedFaces { get; set; }
        [InverseProperty(nameof(FaceCluster.FaceClusterBestFace))]
        public virtual ICollection<FaceCluster> FaceClusters { get; set; }
        [InverseProperty(nameof(FaceFeature.FaceFeatureFace))]
        public virtual ICollection<FaceFeature> FaceFeatures { get; set; }
        [InverseProperty(nameof(Person.PersonBestFace))]
        public virtual ICollection<Person> PersonPersonBestFaces { get; set; }
        [InverseProperty(nameof(Person.PersonSafeBestFace))]
        public virtual ICollection<Person> PersonPersonSafeBestFaces { get; set; }
        [InverseProperty(nameof(VideoFaceOccurrence.VideoFaceOccurrenceFace))]
        public virtual ICollection<VideoFaceOccurrence> VideoFaceOccurrences { get; set; }
    }
}

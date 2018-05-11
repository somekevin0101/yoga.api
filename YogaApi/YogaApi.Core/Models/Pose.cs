using System;
using YogaApi.Core.Enums;

namespace YogaApi.Core.Models
{
    public class Pose
    {
        public PoseName PoseName { get; set; }
        public Difficulty Difficulty { get; set; }
        public FatigueModifer FatigueModifer { get; set; }
        public bool IsStanding { get; set; }
        public bool IsCrouched { get; set; }
        public bool IsSeated { get; set; }
        public bool IsSupine { get; set; }
        public bool IsProne { get; set; }
        public bool IsBalance { get; set; }
        public bool IsInversion { get; set; }
        public bool IsTwist { get; set; }
        public bool IsHipOpener { get; set; }
        public bool IsWeightBearingHipOpener { get; set; }
        public bool IsChestOpener { get; set; }
        public bool IsCoreStrengthener { get; set; }
        public bool IsLegStrenghener { get; set; }
        public bool IsUpperBodyStrengthener { get; set; }
        public bool IsForwardFold { get; set; }
        public bool IsBackBend { get; set; }
        public bool IsResing { get; set; }
        public bool IsBeginningPose { get; set; }
        public bool IsPenultimatePose { get; set; }
        public bool IsFirstThird { get; set; }
        public bool IsSecondThird { get; set; }
        public bool IsLastThird { get; set; }
    }
}

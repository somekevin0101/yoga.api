using System;
using System.Collections.Generic;

namespace YogaApi.Core.Models
{
    public class PoseOrder
    {
        public int Id { get; set; }
        public int DurationInSeconds { get; set; }
        public int PostitionInSequence { get; set; }
        public bool IsMiniSequence { get; set; }
        List<MiniPose> MiniSequence { get; set; }
    }

    public class MiniPose
    {
        public int Id { get; set; }
        public int DurationInSeconds { get; set; }
        public int PositionInMiniSequence { get; set; }
    }
}


using System;
using System.Collections.Generic;

namespace YogaApi.Core.Models
{
    public class PoseOrder
    {
        public int PoseId { get; set; }
        public int DurationInSeconds { get; set; }
        public int OrderInSequence { get; set; }
        public bool IsMiniSequence { get; set; }
        public List<MiniPose> MiniSequence { get; set; }
    }

    public class MiniPose
    {
        public int PoseId { get; set; }
        public int DurationInSeconds { get; set; }
        public int OrderInMiniSequence { get; set; }
    }
}


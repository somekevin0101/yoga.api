using System;
using System.Collections.Generic;

namespace YogaApi.Models
{
    public class PoseOrderPostModel
    {
        public int PoseId { get; set; }
        public int DurationInSeconds { get; set; }
        public int OrderInSequence { get; set; }
        public bool IsMiniSequence { get; set; }
        public List<MiniPoseOrderApiModel> MiniSequence { get; set; }
    }

    public class MiniPoseOrderApiModel
    {
        public int PoseId { get; set; }
        public int DurationInSeconds { get; set; }
        public int OrderInMiniSequence { get; set; }
    }
}
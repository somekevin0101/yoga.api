using System;
using System.Collections.Generic;

namespace YogaApi.Models
{
    public class PoseOrderApiModel
    {
        public int Id { get; set; }
        public int DurationInSeconds { get; set; }
        public int PostitionInSequence { get; set; }
        public bool IsMiniSequence { get; set; }
        List<MiniPoseOrderApiModel> MiniSequence { get; set; }
    }

    public class MiniPoseOrderApiModel
    {
        public int Id { get; set; }
        public int DurationInSeconds { get; set; }
        public int PositionInMiniSequence { get; set; }
    }
}
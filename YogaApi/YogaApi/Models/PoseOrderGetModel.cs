using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YogaApi.Models
{
    public class PoseOrderGetModel
    {
        public long SequencePosesId { get; set; }
        public int PoseId { get; set; }
        public int DurationInSeconds { get; set; }
        public int OrderInSequence { get; set; }
        public bool IsMiniSequence { get; set; }
        public List<MiniPoseOrderApiModel> MiniSequence { get; set; }
    }
}
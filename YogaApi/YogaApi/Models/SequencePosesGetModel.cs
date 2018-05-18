using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YogaApi.Models
{
    public class SequencePosesGetModel
    {
        public SequencePosesGetModel(long sequenceId)
        {
            SequenceId = sequenceId;
            Poses = new List<PoseOrderGetModel>();
        }

        public long SequenceId { get; set; }
        public List<PoseOrderGetModel> Poses { get; set; }
    }
}
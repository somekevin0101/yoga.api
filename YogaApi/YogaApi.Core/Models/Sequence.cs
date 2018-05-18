using System;
using System.Collections.Generic;

namespace YogaApi.Core.Models
{
    public class Sequence
    {
        public Sequence()
        {
            Poses = new List<PoseOrder>();
        }

        public int UserId { get; set; }
        public string SequenceName { get; set; }
        public string SequenceStyle { get; set; }
        public string IsCustomMiniSequence { get; set; }
        public List<PoseOrder> Poses { get; set; }
    }
}

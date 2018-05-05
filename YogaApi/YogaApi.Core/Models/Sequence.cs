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
        public string Name { get; set; }
        public List<PoseOrder> Poses { get; set; }
    }
}

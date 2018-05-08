using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogaApi.Core.Models
{
    public class SequencePoses
    {
        public long SequencePosesId { get; set; }
        public int PoseId { get; set; }
        public int OrderInSequence { get; set; }
        public bool IsMiniSequence { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YogaApi.Models
{
    public class SequenceGetModel
    {
        public long SequenceId { get; set; }
        public string SequenceName { get; set; }
        public string SequenceStyle { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YogaApi.Models
{
    public class SequencePostModel
    {
        [Required]
        public int UserId { get; set; }
        public string SequenceName { get; set; }
        public string SequenceStyle { get; set; }
        [Required]
        public List<PoseOrderApiModel> Poses { get; set; }
    }
}
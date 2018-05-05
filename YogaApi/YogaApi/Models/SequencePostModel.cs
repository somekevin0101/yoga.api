using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YogaApi.Models
{
    public class SequencePostModel
    {
        [Required]
        public int UserId { get; set; }
        public string Name { get; set; }
        [Required]
        public List<PoseOrderApiModel> Poses { get; set; }
    }
}
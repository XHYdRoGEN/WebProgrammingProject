using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgrammingProject1.Models
{
    public class Likes
    {
        [Key]
        public int LikeID { get; set; }
        [Display(Name ="Favorilere Alan")]
        public Users User { get; set; }
        [Display(Name ="Paylaşım")]
        public Posts Post { get; set; }
    }
}

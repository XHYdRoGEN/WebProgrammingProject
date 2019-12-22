using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgrammingProject1.Models
{
    public class Comments
    {
       
        [Key]
        public int CommentID { get; set; }
        public string CommentContent { get; set; }
        public Posts Post { get; set; }
        public Users User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgrammingProject1.Models
{
    public class Admins
    {
        [Key]
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
        public Users Users { get; set; }

    }
}

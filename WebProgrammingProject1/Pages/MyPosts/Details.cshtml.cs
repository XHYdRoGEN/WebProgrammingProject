using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebProgrammingProject1.Context;
using WebProgrammingProject1.Models;

namespace WebProgrammingProject1
{
    public class DetailsMyPosts : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;

        public DetailsMyPosts(WebProgrammingProject1.Context.ShareItContext context)
        {
            _context = context;
        }

        public Posts Posts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Posts = await _context.Posts.FirstOrDefaultAsync(m => m.PostID == id);

            if (Posts == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

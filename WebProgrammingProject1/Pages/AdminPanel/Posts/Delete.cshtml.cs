using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebProgrammingProject1.Context;
using WebProgrammingProject1.Models;

namespace WebProgrammingProject1
{
    public class DeleteModel : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public DeleteModel(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = _httpContextAccessor;
            _session = this._httpContextAccessor.HttpContext.Session;
        }

        [BindProperty]
        public Posts Posts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_session.Get("admin") == null)
            {
                return RedirectToPage("/Login/Login");
            }
            else
            {
                Posts = await _context.Posts.FirstOrDefaultAsync(m => m.PostID == id);

                if (Posts == null)
                {
                    return NotFound();
                }
                return Page();
            }
            
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            
            if (_session.Get("admin") == null)
            {
                return RedirectToPage("/Login/Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }
                Posts = await _context.Posts.FindAsync(id);

                if (Posts != null)
                {
                    _context.Posts.Remove(Posts);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("./Index");
            }
            
        }
    }
}

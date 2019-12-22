using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgrammingProject1.Context;
using WebProgrammingProject1.Models;

namespace WebProgrammingProject1
{
    public class EditModel : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public EditModel(WebProgrammingProject1.Context.ShareItContext context,IHttpContextAccessor accessor)
        {
            _context = context;
            _httpContextAccessor = accessor;
            _session = _httpContextAccessor.HttpContext.Session;

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

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_session.Get("admin") == null)
            {
                return RedirectToPage("/Login/Login");
            }
            else
            {
                _context.Attach(Posts).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsExists(Posts.PostID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("./Index");
            }
        }

        private bool PostsExists(int id)
        {
                return _context.Posts.Any(e => e.PostID == id);
        }
    }
}

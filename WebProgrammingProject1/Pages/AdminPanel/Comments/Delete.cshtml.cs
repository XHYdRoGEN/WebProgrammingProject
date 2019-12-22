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
    public class DeleteComments : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public DeleteComments(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = _httpContextAccessor;
            this._session = _httpContextAccessor.HttpContext.Session;
        }

        [BindProperty]
        public Comments Comments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_session.Get("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                Comments = await _context.Comments.FirstOrDefaultAsync(m => m.CommentID == id);

                if (Comments == null)
                {
                    return NotFound();
                }
                return Page();
            }
            else
            {
                return RedirectToPage("/Login/Login");
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (_session.Get("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                Comments = await _context.Comments.Include(x=>x.Post).Where(x=>x.CommentID == id).FirstOrDefaultAsync();

                if (Comments != null)
                {
                    Comments.Post.PostCommentCount--;
                    _context.Comments.Remove(Comments);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("/Login/Login");
            }
        }
    }
}

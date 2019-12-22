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
    public class DetailsComments : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public DetailsComments(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = _httpContextAccessor;
            this._session = this._httpContextAccessor.HttpContext.Session;
        }

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
    }
}

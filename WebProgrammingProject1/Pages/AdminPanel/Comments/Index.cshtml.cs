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
    public class CommentsModel : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public CommentsModel(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = _httpContextAccessor;
            this._session = this._httpContextAccessor.HttpContext.Session;
        }

        public IList<Comments> Comments { get;set; }

        public IActionResult OnGetAsync()
        {
            if (_session.Get("admin") != null) { 
                Comments = _context.Comments.Include(c=>c.User).Include(c=>c.Post).ToList();
                return Page();
            }
            else
            {
                return RedirectToPage("/Login/Login");
            }
        }
    }
}

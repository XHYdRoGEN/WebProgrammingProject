using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProgrammingProject1.Context;

namespace WebProgrammingProject1
{
    public class ProfileModel : PageModel
    {
        private readonly ShareItContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public ProfileModel(ShareItContext _context, IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            context = _context;
        }
        public IActionResult OnGet()
        {
            if (_session.Get("admin") == null)
            {
                return RedirectToPage("/Login/Login");
            }
            else
            {
                return Page();
            }
        }
    }
}
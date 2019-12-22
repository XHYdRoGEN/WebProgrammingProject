﻿using System;
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
    public class DeleteUsers : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public DeleteUsers(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = _httpContextAccessor;
            this._session = this._httpContextAccessor.HttpContext.Session;
        }

        [BindProperty]
        public Users Users { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_session.Get("admin") != null) { 
            if (id == null)
            {
                return NotFound();
            }

            Users = await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);

            if (Users == null)
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

                Users = await _context.Users.FindAsync(id);

                if (Users != null)
                {
                    _context.Users.Remove(Users);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProgrammingProject1.Context;
using WebProgrammingProject1.Models;

namespace WebProgrammingProject1
{
    public class CreateMyPosts : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public List<Categories> Categories;
        public List<SelectListItem> Kategoriler { get; set; }

        public CreateMyPosts(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = _httpContextAccessor;
            this._session = this._httpContextAccessor.HttpContext.Session;
        }

        public IActionResult OnGet()
        {
            if(_session.Get("id") == null)
            {
                return RedirectToPage("/Login/Login");
            }
            Categories = this._context.Categories.ToList();
            Kategoriler = new List<SelectListItem>();
            foreach (var item in Categories)
            {
                Kategoriler.Add(new SelectListItem(item.CategoryName, item.CategoryID.ToString()));
            }
            return Page();
        }

        [BindProperty]
        public Posts Posts { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            int CategoryID = Posts.Category.CategoryID;
            Categories category = new Categories();
            category = this._context.Categories.Where(x => x.CategoryID == CategoryID).FirstOrDefault();
            Posts.Category = category;
            Posts.User = this._context.Users.Where(x => x.UserID == BitConverter.ToInt32(_session.Get("id"))).FirstOrDefault();
            _context.Posts.Add(Posts);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

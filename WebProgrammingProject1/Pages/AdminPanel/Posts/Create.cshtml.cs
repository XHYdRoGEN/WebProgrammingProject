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
    public class CreateModel : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public List<Categories> Categories;
        public List<SelectListItem> Kategoriler{ get; set; }
        public CreateModel(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _httpContextAccessor = accessor;
            _session = _httpContextAccessor.HttpContext.Session;

        }

        public IActionResult OnGet()
        {
            if (_session.Get("admin") != null) { 
                Categories = this._context.Categories.ToList();
                Kategoriler = new List<SelectListItem>();
                foreach (var item in Categories)
                {
                    Kategoriler.Add(new SelectListItem(item.CategoryName,item.CategoryID.ToString()));
                }
                return Page();
            }
            else
            {
                return RedirectToAction("/Login/Login");
            }
        }

        [BindProperty]
        public Posts Posts { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (_session.Get("admin") == null)
            {
                return RedirectToPage("/Login/Login");
            }
            else
            {
                int CategoryID = Posts.Category.CategoryID;
                Categories category = new Categories();
                category = this._context.Categories.Where(x => x.CategoryID == CategoryID).FirstOrDefault();
                Posts.Category = category;
                
                _context.Posts.Add(Posts);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
        }
    }
}

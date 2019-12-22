using Microsoft.AspNetCore.Http;
using System;

namespace WebProgrammingProject1.CustomAttributes
{
    public class ShareItAuthentication
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session; 
        public ShareItAuthentication(IHttpContextAccessor accessor)
        {
                _httpContextAccessor = accessor;
                _session = _httpContextAccessor.HttpContext.Session;
                if (_session.Get("user") == null)
                {
                    _httpContextAccessor.HttpContext.Response.Redirect("/Login/Login");
                }
            
        }
    }
}

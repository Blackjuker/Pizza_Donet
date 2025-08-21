using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Security.Claims;

namespace pizza_mama.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult  OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/admin/Pizzas");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(string username, string password,string ReturnUrl) 
        {
            var  authSection = _configuration.GetSection("Auth");
            string adminLogin = authSection["AdminLogin"]!;
            string adminPassword = authSection["AdminPassword"]!;
            if(username== adminLogin && password == adminPassword)
            {
                var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.Name,username)
                 };
                var claimIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                return Redirect(ReturnUrl == null ? "/Admin/Pizzas" : ReturnUrl);
            }
            return Page();
        }

        public async Task<IActionResult> OnGetLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin");
        }
    }
}

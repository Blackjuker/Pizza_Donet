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
            if(username== "admin")
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

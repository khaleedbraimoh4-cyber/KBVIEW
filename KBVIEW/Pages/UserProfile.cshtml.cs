using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KBVIEW.Pages
{
    public class UserProfileModel : PageModel
    {

            public string Username { get; set; }
            public string Email { get; set; }

            public IActionResult OnGet()
            {
                Email = HttpContext.Session.GetString("UserEmail");
                Username = HttpContext.Session.GetString("Username");

                if (string.IsNullOrEmpty(Email))
                {
                    return RedirectToPage("/Login");
                }

                return Page();
            }

            public IActionResult OnPostLogout()
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/Login");
            }

            //public void OnGet()
            //{
            //}
        }
}

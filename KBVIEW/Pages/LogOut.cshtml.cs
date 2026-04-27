using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KBVIEW.Pages
{
    public class LogOutModel : PageModel
    {
            public IActionResult OnGet()
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/Login");
            }
        }

        //public void OnGet()
        //{
        //}
    
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KBVIEW.Pages
{
    public class LoginModel : PageModel
    {
        //BindProperty means when the form is submitted, the value typed
        //into the email field gets stored in this variable automatically
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        // This holds any error message to show on the page if login fails
        public string ErrorMessage { get; set; }

        public void OnGet() { }

        // OnPost runs when the user clicks the Login button
        public IActionResult OnPost()
        {
            // the dictionary function stores pairs of email and password like a small table
            // the email and the password are the only two account credentials that can log in
            var validUsers = new Dictionary<string, string>
            {
                { "khaleed@test.com", "password123" },
                { "admin@kbview.com", "admin" }
            };

            // Check if the email exists in the list AND the password matches
            // Both conditions must be true for the login to work
            if (validUsers.ContainsKey(Email) && validUsers[Email] == Password)
            {
                // Save the email into the session so every page knows who is currently logged in
                HttpContext.Session.SetString("UserEmail", Email);
                HttpContext.Session.SetString("Username", Email.Split('@')[0]);
                // Send the user to the home page
                return RedirectToPage("/Index");
            }
            // If the email or password was wrong,the error message is displayed 
            // and the login page is reloaded so the user can try again
            ErrorMessage = "Invalid email or password";
            return Page();
        }


        //public void OnGet()
        //{
        //}
    }
}

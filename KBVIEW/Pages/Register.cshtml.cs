using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KBVIEW.Pages
{
    public class RegisterModel : PageModel
    {
        // Each of the BindProperty picks up the value from its matching
        // form field when the register button is clicked
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        // Holds a message to show on the page, used here for error messages
        public string Message { get; set; }

        // OnGet runs when the page first loads
        public void OnGet() { }

        // OnPost runs when the user clicks the Register button
        public IActionResult OnPost()
        {
            // Check if any of the three fields were left empty
            // IsNullOrEmpty returns true if a field has nothing in it
            // The || means OR and if any one of them is empty this runs
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                // Set the error message and reload the page
                // so the user can see what went wrong
                Message = "All fields are required";
                return Page();
            }

            // If all fields are filled in, save the email and username
            // into the session so the user is logged in straight away
            // after registering
            HttpContext.Session.SetString("UserEmail", Email);
            HttpContext.Session.SetString("Username", Username);

            // Sends the user to the home page
            return RedirectToPage("/Index");
        }

        //public void OnGet()
        //{
        //}
    }
}

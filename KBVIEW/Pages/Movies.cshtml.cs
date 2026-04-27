using KBVIEW.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KBVIEW.Pages
{
    public class MoviesModel : PageModel
    {
        // _tmdb gives this page access to the API methods
        private readonly Service _tmdb;

        // Movies holds the list of popular films to display
        public List<Movie> Movies { get; set; }

        // Constructor stores the Service so it can be used below
        public MoviesModel(Service tmdb) => _tmdb = tmdb;

        // OnGetAsync runs when the Movies page loads
        public async Task OnGetAsync()
        {
            // Ask the API for the current list of popular movies
            var result = await _tmdb.GetPopularMoviesAsync();

            // If the result came back use the list, otherwise use
            // an empty list to stop the page from crashing
            Movies = result?.Results ?? new List<Movie>();
        }

        // Builds the full poster image URL from the path TMDB returns
        public string GetPosterUrl(string path) => _tmdb.GetPosterUrl(path);
    }





    //public void OnGet()
    //{
    //}
}


using KBVIEW.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KBVIEW.Pages
{
    public class AllGenresModel : PageModel
    {

        // _tmdb gives this page access to all the API methods in Service.cs
        private readonly Service _tmdb;

        // Genres holds the full list of genre buttons to show at the top
        public List<Genre> Genres { get; set; }

        // Movies holds whatever list of films is currently being shown
        public List<Movie> Movies { get; set; }

        // SelectedGenre stores which genre button the user clicked
        [BindProperty(SupportsGet = true)]
        public int? SelectedGenre { get; set; }

        // Query stores whatever the user typed into the search bar
        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }

        // Constructor receives and stores the Service so it can be used below
        public AllGenresModel(Service tmdb) => _tmdb = tmdb;

        // OnGetAsync runs every time the page loads
        public async Task OnGetAsync()
        {
            // Always load the full list of genres first
            // so the genre buttons always appear at the top of the page
            var genreResult = await _tmdb.GetMovieGenresAsync();
            Genres = genreResult?.Genres ?? new List<Genre>();

            if (!string.IsNullOrEmpty(Query))
            {
                // If the user typed a search term, search by title
                var result = await _tmdb.SearchMoviesAsync(Query);
                Movies = result?.Results ?? new List<Movie>();
            }
            else if (SelectedGenre.HasValue)
            {
                // If the user clicked a genre button, filter by that genre
                // HasValue checks if a genre was actually selected
                var result = await _tmdb.GetMoviesByGenreAsync(SelectedGenre.Value);
                Movies = result?.Results ?? new List<Movie>();
            }
            else
            {
                // If neither a search nor a genre was chosen,
                // show popular movies as the default so the page is never left blank
                var result = await _tmdb.GetPopularMoviesAsync();
                Movies = result?.Results ?? new List<Movie>();
            }
        }

        // Builds the full poster image URL from the path TMDB returns
        public string GetPosterUrl(string path) => _tmdb.GetPosterUrl(path);
    }
}


//public void OnGet()
//{
//}



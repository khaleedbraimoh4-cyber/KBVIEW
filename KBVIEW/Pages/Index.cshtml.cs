using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KBVIEW.Model;

namespace KBVIEW.Pages
{
    public class IndexModel : PageModel
    {
        // _tmdb is the service that handles all API calls to TMDB
        private readonly Service _tmdb;

        // Movies holds the list of films to show in the card grid
        public List<Movie> Movies { get; set; }
        // FeaturedMovie holds the single film shown in the carousel
        public MovieDetails FeaturedMovie { get; set; }
        // TopRated holds the films shown in the right sidebar 
        public List<Movie> TopRated { get; set; }
        // SupportsGet = true means Query can be read from the URL
        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }
        // The constructor receives the Service and stores it
        // so the rest of the class can use it to make API calls
        public IndexModel(Service tmdb) => _tmdb = tmdb;

        // OnGetAsync runs every time the page loads
        // async means it can wait for API responses without freezing the page
        public async Task OnGetAsync()
        {
            // If no search term has been typed, show trending movies
            // If a search term exists, search for it instead
            // The ? and : here mean IF and ELSE on one line
            var result = string.IsNullOrEmpty(Query)
                ? await _tmdb.GetTrendingAsync()
                : await _tmdb.SearchMoviesAsync(Query);

            // If the result came back empty use an empty list
            // to avoid the page crashing with a null error
            Movies = result?.Results ?? new List<Movie>();

            // Get the first trending movie to show in the carousel
            FeaturedMovie = await _tmdb.GetFeaturedMovieAsync();

            // Get the top rated movies for the right sidebar
            var topRatedResult = await _tmdb.GetTopRatedAsync();
            TopRated = topRatedResult?.Results?.ToList() ?? new List<Movie>();
        }

        // This builds the full image URL from the poster path that TMDB returns
        public string GetPosterUrl(string path) => _tmdb.GetPosterUrl(path);



        //private readonly ILogger<IndexModel> _logger;

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        //public void OnGet()
        //{

        //}
    }
}

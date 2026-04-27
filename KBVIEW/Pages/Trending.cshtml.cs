using KBVIEW.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KBVIEW.Pages
{
    public class TrendingModel : PageModel
    {
        private readonly Service _tmdb;
        public List<Movie> Movies { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }

        public TrendingModel(Service tmdb) => _tmdb = tmdb;

        public async Task OnGetAsync()
        {
            var result = string.IsNullOrEmpty(Query)
                ? await _tmdb.GetTrendingAsync()
                : await _tmdb.SearchMoviesAsync(Query);
            Movies = result?.Results ?? new List<Movie>();
        }

        public string GetPosterUrl(string path) => _tmdb.GetPosterUrl(path);
    }
}

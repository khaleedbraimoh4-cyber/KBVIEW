using KBVIEW.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KBVIEW.Pages
{
    public class TVShowsModel : PageModel
    {
        private readonly Service _tmdb;
        public List<TVShows> TVShows { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }

        public TVShowsModel(Service tmdb) => _tmdb = tmdb;

        public async Task OnGetAsync()
        {
            var result = string.IsNullOrEmpty(Query)
                ? await _tmdb.GetPopularTVShowsAsync()
                : await _tmdb.SearchTVShowsAsync(Query);
            TVShows = result?.Results ?? new List<TVShows>();
        }

        public string GetPosterUrl(string path) => _tmdb.GetPosterUrl(path);



        //public void OnGet()
        //{
        //}
    }
}

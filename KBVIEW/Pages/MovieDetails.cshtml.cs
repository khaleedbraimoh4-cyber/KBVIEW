using KBVIEW.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KBVIEW.Pages
{
    public class MovieDetailsModel : PageModel
    {
        private readonly Service _tmdb;
        public MovieDetails Movie { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public MovieDetailsModel(Service tmdb) => _tmdb = tmdb;

        public async Task OnGetAsync()
        {
            Movie = await _tmdb.GetMovieAsync(Id);
        }

        public string GetPosterUrl(string path) => _tmdb.GetPosterUrl(path);
    }

}

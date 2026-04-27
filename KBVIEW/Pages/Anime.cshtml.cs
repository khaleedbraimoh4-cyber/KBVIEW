using KBVIEW.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KBVIEW.Pages
{
    public class AnimeModel : PageModel
    {

            private readonly Service _tmdb;
            public List<Movie> AnimeMovies { get; set; }

            [BindProperty(SupportsGet = true)]
            public string Query { get; set; }

            public AnimeModel(Service tmdb) => _tmdb = tmdb;

            public async Task OnGetAsync()
            {
                var result = string.IsNullOrEmpty(Query)
                    ? await _tmdb.GetAnimeMoviesAsync()
                    : await _tmdb.SearchMoviesAsync(Query);
                AnimeMovies = result?.Results ?? new List<Movie>();
            }

            public string GetPosterUrl(string path) => _tmdb.GetPosterUrl(path);
        }



        //public void OnGet()
        //{
        //}
    }


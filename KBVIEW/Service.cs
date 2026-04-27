using KBVIEW.Model;

namespace KBVIEW
{
    public class Service
    {
        // _http is the tool that sends requests to the TMDB website
        private readonly HttpClient _http;

        // _imageBase stores the beginning part of every poster image URL
        private readonly string _imageBase;

        // This runs once when the service is first created
        // It sets up the HTTP tool and reads the image URL from settings
        public Service(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient("TMDB");
            _imageBase = config["Tmdb:ImageBaseUrl"];
        }

        // Searches for movies by title
        // async means it waits for TMDB to respond before continuing
        // Uri.EscapeDataString makes the search term safe to put in a URL
        public async Task<MovieResult> SearchMoviesAsync(string query)
        {
            return await _http.GetFromJsonAsync<MovieResult>(
                $"search/movie?query={Uri.EscapeDataString(query)}");
        }

        // Gets the full details for one specific movie using its ID
        public async Task<MovieDetails> GetMovieAsync(int id)
        {
            return await _http.GetFromJsonAsync<MovieDetails>($"movie/{id}");
        }

        // Gets movies that TMDB recommends based on a given movie
        public async Task<MovieResult> GetRecommendationsAsync(int movieId)
        {
            return await _http.GetFromJsonAsync<MovieResult>(
                $"movie/{movieId}/recommendations");
        }

        // Gets the list of movies that are trending this week
        public async Task<MovieResult> GetTrendingAsync()
        {
            return await _http.GetFromJsonAsync<MovieResult>("trending/movie/week");
        }

        // Builds the full image URL by joining the base address
        // and the poster path that TMDB returns
        // e.g. base + "/abc123.jpg" = the full working image address
        public string GetPosterUrl(string path) => $"{_imageBase}{path}";

        // Gets the featured movie for the homepage carousel
        // It takes the first film from the trending list and gets
        // its full details so the overview and runtime are available
        public async Task<MovieDetails> GetFeaturedMovieAsync()
        {
            var trending = await GetTrendingAsync();

            // Check there is at least one trending movie before trying
            // to get its details, otherwise return nothing
            if (trending?.Results?.Count > 0)
                return await GetMovieAsync(trending.Results[0].Id);

            return null;
        }

        // Gets the highest rated movies of all time from TMDB
        public async Task<MovieResult> GetTopRatedAsync()
        {
            return await _http.GetFromJsonAsync<MovieResult>("movie/top_rated");
        }

        // Gets currently popular movies from TMDB
        public async Task<MovieResult> GetPopularMoviesAsync()
        {
            return await _http.GetFromJsonAsync<MovieResult>("movie/popular");
        }

        // Gets the full list of genres available on TMDB
        // e.g. Action (28), Comedy (35), Horror (27)
        public async Task<GenreListResult> GetMovieGenresAsync()
        {
            return await _http.GetFromJsonAsync<GenreListResult>("genre/movie/list");
        }

        // Gets movies that belong to a specific genre
        // using the genre ID number e.g. 28 for Action
        public async Task<MovieResult> GetMoviesByGenreAsync(int genreId)
        {
            return await _http.GetFromJsonAsync<MovieResult>(
                $"discover/movie?with_genres={genreId}");
        }

        // Gets popular TV shows - page allows loading more results
        public async Task<TVResult> GetPopularTVShowsAsync(int page = 1)
        {
            return await _http.GetFromJsonAsync<TVResult>($"tv/popular?page={page}");
        }

        // Searches for TV shows by name
        public async Task<TVResult> SearchTVShowsAsync(string query)
        {
            return await _http.GetFromJsonAsync<TVResult>(
                $"search/tv?query={Uri.EscapeDataString(query)}");
        }

        // Gets TV shows that are trending this week
        public async Task<TVResult> GetTrendingTVShowsAsync()
        {
            return await _http.GetFromJsonAsync<TVResult>("trending/tv/week");
        }

        // Gets anime movies using genre ID 16 which is Animation on TMDB
        public async Task<MovieResult> GetAnimeMoviesAsync(int page = 1)
        {
            return await _http.GetFromJsonAsync<MovieResult>(
                $"discover/movie?with_genres=16&page={page}");
        }
    }
}


using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.PrivateData;
using Newtonsoft.Json;

namespace Listifyr.ProgramLogic.APIs.TMDB
{
    public class TmdbMovies_service
    {
        private const string apiUrl = "https://api.themoviedb.org/3/search/movie?api_key=" + Data.TMDB_apiKey + "&include_adult=true&language=en-US&page=1&query=";

        public static async Task<(bool success, List<Items> results)> SearchMoviesAsync(string query)
        {
            using HttpClient client = new();
            string url = apiUrl + query;
            try
            {
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return (false, new List<Items>());

                var responseString = await response.Content.ReadAsStringAsync();
                var moviesResponse = JsonConvert.DeserializeObject<MovieResponse>(responseString);

                var mediaItems = moviesResponse?.Results?.Select(movie => new Items
                {
                    ItemName = movie.Title ?? "N/A",
                    Description = (movie.Overview ?? "No data in DB") + "\n\nPowered by The Movie Database (TMDB) API",
                    Poster = "https://image.tmdb.org/t/p/w500" + movie.PosterPath,
                    Release_Date = movie.Release_Date ?? "No data in DB"
                }).ToList();

                return (true, mediaItems ?? new List<Items>());
            }
            catch (Exception ex)
            {
                return (false, new List<Items>());
            }
        }

        private class MovieResponse
        {
            public List<Movie>? Results { get; set; }
        }

        private class Movie
        {
            [JsonProperty("title")]
            public string? Title { get; set; }

            [JsonProperty("overview")]
            public string? Overview { get; set; }

            [JsonProperty("poster_path")]
            public string? PosterPath { get; set; }

            [JsonProperty("release_date")]
            public string? Release_Date { get; set; }
        }
    }
}

using Listifyr.ItemTypes;
using Newtonsoft.Json;

namespace Listifyr.ProgramLogic.APIs.TMDB
{
    public class TmdbMovies_service
    {
        private const string apiKey = "7ad7bd4b8453c43887a81bb992f2a1fc";
        private const string apiUrl = "https://api.themoviedb.org/3/search/movie?api_key=" + apiKey + "&include_adult=true&language=en-US&page=1&query=";

        public async Task<List<Items>> SearchMoviesAsync(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = apiUrl + query;
                var response = await client.GetStringAsync(url);
                var moviesResponse = JsonConvert.DeserializeObject<MovieResponse>(response);

                var mediaItems = moviesResponse.results.Select(movie => new Items
                {
                    ItemName = movie.Title,
                    Description = movie.Overview,
                    Poster = "https://image.tmdb.org/t/p/w500" + movie.PosterPath,
                    Release_Date = movie.Release_Date
                }).ToList();
                return mediaItems;
            }
        }

        private class MovieResponse
        {
            public List<Movie> results { get; set; }
        }

        private class Movie
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("overview")]
            public string Overview { get; set; }

            [JsonProperty("poster_path")]
            public string PosterPath { get; set; }

            [JsonProperty("release_date")]
            public string Release_Date { get; set; }
        }
    }
}

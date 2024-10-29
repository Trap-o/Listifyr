using Listifyr.ItemTypes;
using Newtonsoft.Json;

namespace Listifyr.ProgramLogic.APIs.TMDB
{
    public class TmdbSeries_service
    {
        private const string apiKey = "7ad7bd4b8453c43887a81bb992f2a1fc";
        private const string apiUrl = "https://api.themoviedb.org/3/search/tv?api_key=" + apiKey + "&include_adult=true&language=en-US&page=1&query=";

        public async Task<List<Items>> SearchSeriesAsync(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = apiUrl + query;
                var response = await client.GetStringAsync(url);
                var seriesResponse = JsonConvert.DeserializeObject<SeriesResponse>(response);

                var mediaItems = seriesResponse.Results.Select(series => new Items
                {
                    ItemName = series.Name,
                    Description = series.Overview,
                    Poster = "https://image.tmdb.org/t/p/w500" + series.PosterPath,
                    Release_Date = series.FirstAirDate
                }).ToList();

                return mediaItems;
            }
        }

        private class SeriesResponse
        {
            public List<Series>? Results { get; set; }
        }

        private class Series
        {
            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("overview")]
            public string? Overview { get; set; }

            [JsonProperty("poster_path")]
            public string? PosterPath { get; set; }

            [JsonProperty("first_air_date")]
            public string? FirstAirDate { get; set; }
        }
    }
}

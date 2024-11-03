using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.PrivateData;
using Newtonsoft.Json;

namespace Listifyr.ProgramLogic.APIs.TMDB
{
    public class TmdbSeries_service
    {
        private const string apiUrl = "https://api.themoviedb.org/3/search/tv?api_key=" + Data.TMDB_apiKey + "&include_adult=true&language=en-US&page=1&query=";

        public static async Task<List<Items>> SearchSeriesAsync(string query)
        {
            using HttpClient client = new();
            string url = apiUrl + query;
            var response = await client.GetStringAsync(url);
            var seriesResponse = JsonConvert.DeserializeObject<SeriesResponse>(response);

            var mediaItems = seriesResponse?.Results?.Select(series => new Items
            {
                ItemName = series.Name ?? "N/A",
                Description = (series.Overview ?? "No data in DB") + "\n\nPowered by The Movie Database (TMDB) API",
                Poster = "https://image.tmdb.org/t/p/w500" + series.PosterPath,
                Release_Date = series.FirstAirDate ?? "No data in DB"
            }).ToList();

            return mediaItems;
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

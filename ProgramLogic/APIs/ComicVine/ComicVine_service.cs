using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.PrivateData;
using Newtonsoft.Json;

namespace Listifyr.ProgramLogic.APIs.ComicVine
{
    public class ComicVine_service
    {
        private const string apiUrl = "https://comicvine.gamespot.com/api/search/?api_key=" + Data.ComicVine_apiKey + "&format=json&resources=issue&query=";

        public async Task<List<Items>> SearchComicsAsync(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = apiUrl + query;
                var response = await client.GetStringAsync(url);
                var comicsResponse = JsonConvert.DeserializeObject<ComicsResponse>(response);

                var mediaItems = comicsResponse?.Results?.Select(comics => new Items
                {
                    ItemName = comics.Name ?? (comics.Volume.Name + " " + comics.IssueNumber) ?? "No data in DB",
                    Description = comics.Description ?? comics.Deck ?? "No data in DB",
                    Poster = comics.Image?.OriginalUrl ?? "No data in DB",
                    Release_Date = comics.CoverDate ?? "No data in DB"
                }).ToList();

                return mediaItems;
            }
        }

        private class ComicsResponse
        {
            public List<ComicsResult>? Results { get; set; }
        }

        private class ComicsResult
        {
            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("description")]
            public string? Description { get; set; }

            [JsonProperty("deck")]
            public string? Deck { get; set; }

            [JsonProperty("issue_number")]
            public string? IssueNumber { get; set; }

            [JsonProperty("cover_date")]
            public string? CoverDate { get; set; }

            [JsonProperty("volume")]
            public ComicVolume Volume { get; set; }

            [JsonProperty("image")]
            public ComicImage? Image { get; set; }
        }

        private class ComicVolume
        {
            [JsonProperty("name")]
            public string? Name { get; set; }
        }

        private class ComicImage
        {
            [JsonProperty("original_url")]
            public string? OriginalUrl { get; set; }
        }
    }
}

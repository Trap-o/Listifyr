using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.PrivateData;
using Newtonsoft.Json;

namespace Listifyr.ProgramLogic.APIs.ComicVine
{
    public class ComicVine_service
    {
        private const string apiUrl = "https://comicvine.gamespot.com/api/search/?api_key=" + Data.ComicVine_apiKey + "&format=json&resources=issue&query=";

        public static async Task<(bool success, List<Items> results)> SearchComicsAsync(string query)
        {
            using HttpClient client = new();
            string url = apiUrl + query;

            try
            {
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return (false, new List<Items>());

                var responseString = await response.Content.ReadAsStringAsync();
                var comicsResponse = JsonConvert.DeserializeObject<ComicsResponse>(responseString);

                var mediaItems = comicsResponse?.Results?.Select(comics => new Items
                {
                    ItemName = comics.Name ?? (comics?.Volume?.Name + " " + comics?.IssueNumber) ?? "N/A",
                    Description = (comics?.Description ?? comics?.Deck ?? "No data in DB") + "\n\nPowered by Comic Vine API",
                    Poster = comics?.Image?.OriginalUrl ?? Data.noImageIcon,
                    Release_Date = comics?.CoverDate ?? "No data in DB"
                }).ToList();

                return (true, mediaItems ?? new List<Items>());
            }
            catch (Exception ex)
            {
                return (false, new List<Items>());
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
            public ComicVolume? Volume { get; set; }

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

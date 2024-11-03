using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.PrivateData;
using Newtonsoft.Json;

namespace Listifyr.ProgramLogic.APIs
{
    public class RAWG_service
    {
        private const string apiUrlSearchGameID = "https://api.rawg.io/api/games?key=" + Data.RAWG_apiKey + "&page_size=20&search=";

        public static async Task<List<Items>> SearchGamesAsync(string query)
        {
            using HttpClient client = new();
            string url = apiUrlSearchGameID + query;
            var response = await client.GetStringAsync(url);
            var gamesResponse = JsonConvert.DeserializeObject<GamesResponse>(response);

            var mediaItems = new List<Items>();

            foreach (var game in gamesResponse?.Results)
            {
                var detailsResponse = await client.GetStringAsync($"https://api.rawg.io/api/games/{game.Id}?key={Data.RAWG_apiKey}");
                var gameDetailsResponse = JsonConvert.DeserializeObject<GameDetailResponse>(detailsResponse);

                var mediaItem = new Items
                {
                    ItemName = gameDetailsResponse?.Name ?? "N/A",
                    Description = (gameDetailsResponse?.Description ?? gameDetailsResponse?.RedditDescription ?? "No data in DB") +
                                  "\n\nPowered by RAWG Video Games Database API",
                    Poster = gameDetailsResponse?.BackgroundImage ?? gameDetailsResponse?.BackgroundImageAdditional ?? Data.noImageIcon,
                    Release_Date = gameDetailsResponse?.Released ?? "No data in DB"
                };
                mediaItems.Add(mediaItem);
            }
            return mediaItems;
        }

        private class GamesResponse
        {
            public List<Games>? Results { get; set; }
        }

        private class GameDetailResponse
        {
            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("description")]
            public string? Description { get; set; }

            [JsonProperty("reddit_description")]
            public string? RedditDescription { get; set; }

            [JsonProperty("background_image")]
            public string? BackgroundImage { get; set; }

            [JsonProperty("background_image_additional")]
            public string? BackgroundImageAdditional { get; set; }

            [JsonProperty("released")]
            public string? Released { get; set; }
        }

        private class Games
        {
            [JsonProperty("id")]
            public int Id { get; set; }
        }
    }
}

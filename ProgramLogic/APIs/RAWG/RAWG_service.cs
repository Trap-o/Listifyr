using Listifyr.ItemTypes;
using Newtonsoft.Json;
using Listifyr.ProgramLogic.PrivateData;

namespace Listifyr.ProgramLogic.APIs
{
    public class RAWG_service
    {
        private const string noImageIcon = "https://st3.depositphotos.com/1322515/35964/v/450/depositphotos_359648638-stock-illustration-image-available-icon.jpg";
        private const string apiUrlSearchGameID = "https://api.rawg.io/api/games?key=" + Data.RAWG_apiKey + "&page_size=20&search=";

        public async Task<List<Items>> SearchGamesAsync(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = apiUrlSearchGameID + query;
                var response = await client.GetStringAsync(url);
                var gamesResponse = JsonConvert.DeserializeObject<GamesResponse>(response);

                var mediaItems = new List<Items>();

                foreach (var game in gamesResponse.Results)
                {
                    var detailsResponse = await client.GetStringAsync($"https://api.rawg.io/api/games/{game.Id}?key={Data.RAWG_apiKey}");
                    var gameDetailsResponse = JsonConvert.DeserializeObject<GameDetailResponse>(detailsResponse);

                    var mediaItem = new Items
                    {
                        ItemName = gameDetailsResponse?.Name ?? "No data in DB",
                        Description = gameDetailsResponse?.Description ?? gameDetailsResponse?.RedditDescription ?? "No data in DB",
                        Poster = gameDetailsResponse?.BackgroundImage ?? gameDetailsResponse?.BackgroundImageAdditional ?? "No data in DB",
                        Release_Date = gameDetailsResponse?.Released ?? "No data in DB"
                    };
                    mediaItems.Add(mediaItem);
                }
                return mediaItems;
            }
        }

        public class GamesResponse
        {
            public List<Games>? Results { get; set; }
        }

        public class GameDetailResponse
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

        public class Games
        {
            [JsonProperty("id")]
            public int Id { get; set; }
        }
    }
}

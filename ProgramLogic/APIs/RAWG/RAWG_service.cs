using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.PrivateData;
using Newtonsoft.Json;
using System;

namespace Listifyr.ProgramLogic.APIs
{
    public class RAWG_service
    {
        private const string apiUrlSearchGameID = "https://api.rawg.io/api/games?key=" + Data.RAWG_apiKey + "&page_size=20&search=";

        public static async Task<(bool success, List<Items> results)> SearchGamesAsync(string query)
        {
            using HttpClient client = new();
            string url = apiUrlSearchGameID + query;
            try
            {
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return (false, new List<Items>());

                var responseString = await response.Content.ReadAsStringAsync();
                var gamesResponse = JsonConvert.DeserializeObject<GamesResponse>(responseString);

                var mediaItems = new List<Items>();

                foreach (var game in gamesResponse?.Results)
                {
                    var detailsResponse = await client.GetAsync($"https://api.rawg.io/api/games/{game.Id}?key={Data.RAWG_apiKey}");

                    if (!response.IsSuccessStatusCode)
                        return (false, new List<Items>());

                    var detailsResponseString = await detailsResponse.Content.ReadAsStringAsync();
                    var gameDetailsResponse = JsonConvert.DeserializeObject<GameDetailResponse>(detailsResponseString);

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
                return (true, mediaItems ?? new List<Items>());
            }
            catch (Exception ex)
            {
                return (false, new List<Items>());
            }
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

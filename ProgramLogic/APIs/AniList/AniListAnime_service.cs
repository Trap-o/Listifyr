using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.PrivateData;
using Newtonsoft.Json;
using System.Text;

namespace Listifyr.ProgramLogic.APIs.AniList
{
    public class AniListAnime_service
    {
        private const string apiURL = "https://graphql.anilist.co";

        public static async Task<List<Items>> SearchAnimeAsync(string query)
        {
            using HttpClient client = new();
            var queryObject = new
            {
                query = @"
                    query ($search: String) {
                      Page {
                        media(search: $search, type: ANIME) {
                          title {
                            english
                            romaji
                            native
                          }
                          coverImage {
                            large
                          }
                          description
                          startDate {
                            year
                            month
                            day
                          }
                        }
                      }
                    }",
                variables = new { search = query }
            };

            var content = new StringContent(JsonConvert.SerializeObject(queryObject), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiURL, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var animeResponse = JsonConvert.DeserializeObject<AniListApiResponse>(responseString);

            if (animeResponse?.Data?.Page?.Media == null || animeResponse.Data.Page.Media.Count == 0)
                return [];

            var mediaItems = animeResponse.Data.Page.Media.Select(anime => new Items
            {
                ItemName = anime.Title?.English ?? anime.Title?.Romaji ?? anime.Title?.Native ?? "N/A",
                Description = (anime.Description ?? "No data in DB") + "\n\nPowered by AniList API",
                Poster = anime.CoverImage?.Large ?? Data.noImageIcon,
                Release_Date = $"{anime.StartDate?.Year}-{anime.StartDate?.Month:D2}-{anime.StartDate?.Day:D2}" ?? "No data in DB"
            }).ToList();

            return mediaItems;
        }

        private class AniListApiResponse
        {
            [JsonProperty("data")]
            public AniListData? Data { get; set; }
        }

        private class AniListData
        {
            [JsonProperty("Page")]
            public AniListPage? Page { get; set; }
        }

        private class AniListPage
        {
            [JsonProperty("media")]
            public List<Anime>? Media { get; set; }
        }

        private class Anime
        {
            [JsonProperty("title")]
            public AniListTitle? Title { get; set; }

            [JsonProperty("description")]
            public string? Description { get; set; }

            [JsonProperty("coverImage")]
            public AniListCoverImage? CoverImage { get; set; }

            [JsonProperty("startDate")]
            public AniListDate? StartDate { get; set; }
        }

        private class AniListTitle
        {
            [JsonProperty("english")]
            public string? English { get; set; }

            [JsonProperty("romaji")]
            public string? Romaji { get; set; }

            [JsonProperty("native")]
            public string? Native { get; set; }
        }

        private class AniListCoverImage
        {
            [JsonProperty("large")]
            public string? Large { get; set; }
        }

        private class AniListDate
        {
            [JsonProperty("year")]
            public int? Year { get; set; }

            [JsonProperty("month")]
            public int? Month { get; set; }

            [JsonProperty("day")]
            public int? Day { get; set; }
        }
    }
}

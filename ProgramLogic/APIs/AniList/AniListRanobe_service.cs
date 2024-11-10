using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.PrivateData;
using Newtonsoft.Json;
using System.Text;

namespace Listifyr.ProgramLogic.APIs.AniList
{
    public class AniListRanobe_service
    {
        private const string apiURL = "https://graphql.anilist.co";

        public static async Task<(bool success, List<Items> results)> SearchRanobeAsync(string query)
        {
            using HttpClient client = new();
            var queryObject = new
            {
                query = @"
                    query ($search: String) {
                      Page {
                        media(search: $search, type: MANGA, format: NOVEL) {
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
            try
            {
                var response = await client.PostAsync(apiURL, content);

                if (!response.IsSuccessStatusCode)
                    return (false, new List<Items>());

                var responseString = await response.Content.ReadAsStringAsync();
                var ranobeResponse = JsonConvert.DeserializeObject<AniListApiResponse>(responseString);

                if (ranobeResponse?.Data?.Page?.Media == null || ranobeResponse.Data.Page.Media.Count == 0)
                    return (true, new List<Items>());

                var mediaItems = ranobeResponse.Data.Page.Media.Select(ranobe => new Items
                {
                    ItemName = ranobe.Title?.English ?? ranobe.Title?.Romaji ?? ranobe.Title?.Native ?? "N/A",
                    Description = (ranobe.Description ?? "No data in DB") + "\n\nPowered by AniList API",
                    Poster = ranobe.CoverImage?.Large ?? Data.noImageIcon,
                    Release_Date = $"{ranobe.StartDate?.Year}-{ranobe.StartDate?.Month:D2}-{ranobe.StartDate?.Day:D2}" ?? "No data in DB"
                }).ToList();

                return (true, mediaItems);
            }
            catch (Exception ex)
            {
                return (false, new List<Items>());
            }
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
            public List<Ranobe>? Media { get; set; }
        }

        private class Ranobe
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

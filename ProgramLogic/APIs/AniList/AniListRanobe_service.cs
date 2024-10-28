using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Listifyr.ItemTypes;
using Newtonsoft.Json;

namespace Listifyr.ProgramLogic.APIs.AniList
{
    public class AniListRanobe_service
    {
        private const string apiURL = "https://graphql.anilist.co";

        public async Task<List<Items>> SearchRanobeAsync(string query)
        {
            using (HttpClient client = new HttpClient())
            {
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
                var response = await client.PostAsync(apiURL, content);
                var responseString = await response.Content.ReadAsStringAsync();

                // Перевірка відповіді
                Console.WriteLine("Response: " + responseString);

                var ranobeResponse = JsonConvert.DeserializeObject<AniListApiResponse>(responseString);

                // Перевірка на порожній результат
                if (ranobeResponse?.Data?.Page?.Media == null || !ranobeResponse.Data.Page.Media.Any())
                {
                    Console.WriteLine("No results found or response is null.");
                    return new List<Items>();
                }

                var mediaItems = ranobeResponse.Data.Page.Media.Select(ranobe => new Items
                {
                    ItemName = ranobe.Title?.English ?? ranobe.Title?.Romaji ?? ranobe.Title?.Native ?? "N/A",
                    Description = ranobe.Description ?? null,
                    Poster = ranobe.CoverImage?.Large ?? null,
                    Release_Date = $"{ranobe.StartDate?.Year}-{ranobe.StartDate?.Month:D2}-{ranobe.StartDate?.Day:D2}" ?? null
                }).ToList();

                return mediaItems;
            }
        }

        // Структура класів для десеріалізації
        public class AniListApiResponse
        {
            [JsonProperty("data")]
            public AniListData? Data { get; set; }
        }

        public class AniListData
        {
            [JsonProperty("Page")]
            public AniListPage? Page { get; set; }
        }

        public class AniListPage
        {
            [JsonProperty("media")]
            public List<Ranobe>? Media { get; set; }
        }

        public class Ranobe
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

        public class AniListTitle
        {
            [JsonProperty("english")]
            public string? English { get; set; }

            [JsonProperty("romaji")]
            public string? Romaji { get; set; }
            [JsonProperty("native")]
            public string? Native { get; set; }
        }

        public class AniListCoverImage
        {
            [JsonProperty("large")]
            public string? Large { get; set; }
        }

        public class AniListDate
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

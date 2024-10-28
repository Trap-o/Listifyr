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
    public class AniListManga_service
    {
        private const string apiURL = "https://graphql.anilist.co";

        public async Task<List<Items>> SearchMangaAsync(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                var queryObject = new
                {
                    query = @"
                    query ($search: String) {
                      Page {
                        media(search: $search, type: MANGA, format: MANGA) {
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

                var mangaResponse = JsonConvert.DeserializeObject<AniListApiResponse>(responseString);

                // Перевірка на порожній результат
                if (mangaResponse?.Data?.Page?.Media == null || !mangaResponse.Data.Page.Media.Any())
                {
                    Console.WriteLine("No results found or response is null.");
                    return new List<Items>();
                }

                var mediaItems = mangaResponse.Data.Page.Media.Select(manga => new Items
                {
                    ItemName = manga.Title?.English ?? manga.Title?.Romaji ?? manga.Title?.Native ?? "N/A",
                    Description = manga.Description ?? null,
                    Poster = manga.CoverImage?.Large ?? null,
                    Release_Date = $"{manga.StartDate?.Year}-{manga.StartDate?.Month:D2}-{manga.StartDate?.Day:D2}" ?? null
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
            public List<Manga>? Media { get; set; }
        }

        public class Manga
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

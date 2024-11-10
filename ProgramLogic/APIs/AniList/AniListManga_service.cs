using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.PrivateData;
using Newtonsoft.Json;
using System.Text;

namespace Listifyr.ProgramLogic.APIs.AniList
{
    public class AniListManga_service
    {
        private const string apiURL = "https://graphql.anilist.co";

        public static async Task<(bool success, List<Items> results)> SearchMangaAsync(string query)
        {
            using HttpClient client = new();
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
            try
            {
                var response = await client.PostAsync(apiURL, content);

                if (!response.IsSuccessStatusCode)
                    return (false, new List<Items>());

                var responseString = await response.Content.ReadAsStringAsync();
                var mangaResponse = JsonConvert.DeserializeObject<AniListApiResponse>(responseString);

                if (mangaResponse?.Data?.Page?.Media == null || mangaResponse.Data.Page.Media.Count == 0)
                    return (true, new List<Items>());

                var mediaItems = mangaResponse.Data.Page.Media.Select(manga => new Items
                {
                    ItemName = manga.Title?.English ?? manga.Title?.Romaji ?? manga.Title?.Native ?? "N/A",
                    Description = (manga.Description ?? "No data in DB") + "\n\nPowered by AniList API",
                    Poster = manga.CoverImage?.Large ?? Data.noImageIcon,
                    Release_Date = $"{manga.StartDate?.Year}-{manga.StartDate?.Month:D2}-{manga.StartDate?.Day:D2}" ?? "No data in DB"
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
            public List<Manga>? Media { get; set; }
        }

        private class Manga
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

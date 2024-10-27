using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr.ProgramLogic.APIs
{
    public class AniList_service
    {
        private const string apiURL = "https://graphql.anilist.co";

        public async Task<List<Items>> SearchAnimeAsync(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                var queryObject = new
                {
                    query = @"
                        query ($search: String) {
                          Page {
                            media(search: $search, type: ANIME) {
                              title {
                                english
                              }
                              averageScore
                              bannerImage
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
                
                var response = await client.GetStringAsync(url);
                var moviesResponse = JsonConvert.DeserializeObject<MovieResponse>(response);

                var mediaItems = moviesResponse.results.Select(movie => new Items
                {
                    ItemName = movie.Title,
                    Description = movie.Overview,
                    Poster = "https://image.tmdb.org/t/p/w500" + movie.PosterPath,
                    Release_Date = movie.Release_Date
                }).ToList();
                return mediaItems;
            }
        }
        public class AnimeResponse
        {
            public List<Anime> results { get; set; }
        }

        public class Anime
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("bannerImage")]
            public string BannerImage { get; set; }

            [JsonProperty("startDate")]
            public string StartDate { get; set; }
        }
    }
}

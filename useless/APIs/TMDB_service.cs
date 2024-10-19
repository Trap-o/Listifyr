//using Listifyr.ItemTypes;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Listifyr.useless.APIs
//{
//    public class TMDB_service
//    {
//        private const string apiKey = "7ad7bd4b8453c43887a81bb992f2a1fc";
//        private const string apiUrl = "https://api.themoviedb.org/3/search/movie?api_key=" + apiKey + "&language=uk-UA&query=";
//        public async Task<List<MediaItems>> SearchMoviesAsync(string query)
//        {
//            using (HttpClient client = new HttpClient())
//            {
//                string url = apiUrl + query;
//                var response = await client.GetStringAsync(url);
//                var moviesResponse = JsonConvert.DeserializeObject<MovieResponse>(response);
//                foreach (var movie in moviesResponse.results)
//                {
//                    Debug.WriteLine("Name: " + movie.Name);
//                    Debug.WriteLine("Description: " + movie.Description);
//                    Debug.WriteLine("Poster: " + movie.Poster);
//                }
//                return moviesResponse.results;
//            }
//        }

//        public class MovieResponse
//        {
//            public List<MediaItems> results { get; set; }
//        }
//    }
//}

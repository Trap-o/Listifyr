using Listifyr.ItemTypes;
using Newtonsoft.Json;

namespace Listifyr.ProgramLogic.APIs.GoogleBooks
{
    public class GoogleBooks_service
    {
        private const string noImageIcon = "https://st3.depositphotos.com/1322515/35964/v/450/depositphotos_359648638-stock-illustration-image-available-icon.jpg";
        private const string apiKey = "AIzaSyAcZKQe_gDHRos57NFbj-NEWYhE7iacTiE";
        private const string apiUrl = "https://www.googleapis.com/books/v1/volumes/?key=" + apiKey + "&q=intitle:";

        public async Task<List<Items>> SearchBooksAsync(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = apiUrl + query;
                var response = await client.GetStringAsync(url);
                var booksResponse = JsonConvert.DeserializeObject<BooksResponse>(response);

                var mediaItems = booksResponse?.Items?.Select(books => {
                    var imageUrl = books.VolumeInfo?.ImageLinks?.Medium ??
                                    books.VolumeInfo?.ImageLinks?.Thumbnail ??
                                    books.VolumeInfo?.ImageLinks?.SmallThumbnail ??
                                    noImageIcon;
                    return new Items
                    {
                        ItemName = books.VolumeInfo?.Title ?? "No data in DB",
                        Description = books.VolumeInfo?.Description ?? "No data in DB",
                        Poster = imageUrl?.Replace("http://", "https://") ?? "No data in DB",
                        Release_Date = books.VolumeInfo?.PublishedDate ?? "No data in DB"
                    };
                }).ToList();

                return mediaItems;
            }
        }

        private class BooksResponse
        {
            public List<Books>? Items { get; set; }
        }

        private class Books
        {
            [JsonProperty("volumeInfo")]
            public VolumeInfo? VolumeInfo { get; set; }
        }

        private class VolumeInfo
        {
            [JsonProperty("title")]
            public string? Title { get; set; }

            [JsonProperty("description")]
            public string? Description { get; set; }

            [JsonProperty("publishedDate")]
            public string? PublishedDate { get; set; }

            [JsonProperty("imageLinks")]
            public ImageLinks? ImageLinks { get; set; }
        }

        private class ImageLinks
        {
            [JsonProperty("medium")]
            public string? Medium { get; set; }

            [JsonProperty("thumbnail")]
            public string? Thumbnail { get; set; }

            [JsonProperty("smallThumbnail")]
            public string? SmallThumbnail { get; set; }
        }
    }
}

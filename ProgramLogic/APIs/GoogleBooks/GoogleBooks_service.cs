using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.PrivateData;
using Newtonsoft.Json;

namespace Listifyr.ProgramLogic.APIs.GoogleBooks
{
    public class GoogleBooks_service
    {
        private const string apiUrl = "https://www.googleapis.com/books/v1/volumes/?key=" + Data.GoogleBooks_apiKey + "&q=intitle:";

        public static async Task<List<Items>> SearchBooksAsync(string query)
        {
            using HttpClient client = new();
            string url = apiUrl + query;
            var response = await client.GetStringAsync(url);
            var booksResponse = JsonConvert.DeserializeObject<BooksResponse>(response);

            var mediaItems = booksResponse?.Items?.Select(books =>
            {
                var imageUrl = books.VolumeInfo?.ImageLinks?.Medium ??
                               books.VolumeInfo?.ImageLinks?.Thumbnail ??
                               books.VolumeInfo?.ImageLinks?.SmallThumbnail ??
                               Data.noImageIcon;
                return new Items
                {
                    ItemName = books.VolumeInfo?.Title ?? "N/A",
                    Description = books.VolumeInfo?.Description + "\n\nPowered by Google Books API" ?? "No data in DB",
                    Poster = imageUrl?.Replace("http://", "https://"),
                    Release_Date = books.VolumeInfo?.PublishedDate ?? "No data in DB"
                };
            }).ToList();

            return mediaItems;
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

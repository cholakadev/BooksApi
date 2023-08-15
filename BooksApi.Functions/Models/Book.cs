using System.Text.Json.Serialization;

namespace BooksApi.Functions.Models
{
    public class Book
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }
    }
}

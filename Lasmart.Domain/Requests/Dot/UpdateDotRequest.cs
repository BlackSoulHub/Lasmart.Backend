using System.Text.Json.Serialization;

namespace Lastmart.Domain.Requests.Dot
{
    public class UpdateDotRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("x")]
        public int X { get; set; }
        [JsonPropertyName("y")]
        public int Y { get; set; }
        [JsonPropertyName("radius")]
        public int Radius { get; set; }
        [JsonPropertyName("color")]
        public string Color { get; set; }
    }
}
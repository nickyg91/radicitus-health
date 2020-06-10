using System.Text.Json.Serialization;

namespace Radicitus.Health.Dto.Dto
{
    public class Track
    {
        [JsonPropertyName("trackNumber")]
        public short TrackNumber { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("playUrl")]
        public string PlayUrl { get; set; }
        [JsonPropertyName("artist")]
        public string Artist { get; set; }
    }
}

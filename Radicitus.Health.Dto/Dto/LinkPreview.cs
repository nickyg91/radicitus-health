using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Radicitus.Health.Dto.Dto
{
    public class LinkPreview
    {
        [JsonPropertyName("imageUrl")]
        public Uri ImageUrl { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("html")]
        public string Html { get; set; }
        [JsonPropertyName("tracks")]
        public List<Track> Tracks { get; set; }
    }
}

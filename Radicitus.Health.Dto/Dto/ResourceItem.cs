using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Radicitus.Health.Dto.Dto
{
    public class ResourceItem
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        [JsonPropertyName("guid")]
        public Guid Guid { get; set; }
    }
}

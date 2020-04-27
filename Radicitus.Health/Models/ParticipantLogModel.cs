using System.Text.Json.Serialization;
using Radicitus.Health.Dto.Dto;

namespace Radicitus.Health.Models
{
    public class ParticipantLogModel
    {
        [JsonPropertyName("participantLog")]
        public ParticipantLogDto ParticipantLog { get; set; }
        [JsonPropertyName("photoBase64")]
        public string PhotoBase64 { get; set; }
        [JsonPropertyName("healthInitiativeId")]
        public int HealthInitiativeId { get; set; }
    }
}

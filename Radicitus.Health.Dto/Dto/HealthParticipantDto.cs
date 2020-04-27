using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Radicitus.Health.Dto.Dto
{
    public class HealthParticipantDto : IHealthParticipant
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("healthInitiativeId")]
        public int HealthInitiativeId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("individualWeightLossGoal")]
        public decimal IndividualWeightLossGoal { get; set; }
        [JsonPropertyName("participantLogs")]
        public List<ParticipantLogDto> ParticipantLogs { get; set; }
        [JsonPropertyName("startingWeight")]
        public decimal StartingWeight { get; set; }
    }
}

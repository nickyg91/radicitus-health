using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Radicitus.Health.Dto.Dto
{
    public class HealthInitiativeDto : IHealthInitiative
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("totalWeightLossGoal")]
        public decimal TotalWeightLossGoal { get; set; }

        [JsonPropertyName("participants")]
        public List<HealthParticipantDto> Participants { get; set; }

        [JsonPropertyName("startDateTime")]
        public DateTime? StartDateTime { get; set; }
        [JsonPropertyName("endDateTime")]
        public DateTime? EndDateTime { get; set; }
    }
}

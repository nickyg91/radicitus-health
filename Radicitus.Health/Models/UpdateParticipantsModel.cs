using System.Collections.Generic;
using System.Text.Json.Serialization;
using Radicitus.Health.Dto.Dto;

namespace Radicitus.Health.Models
{
    public class UpdateParticipantsModel
    {
        [JsonPropertyName("updatedParticipants")]
        public List<HealthParticipantDto> UpdatedParticipants { get; set; }
        [JsonPropertyName("removedParticipants")]
        public List<HealthParticipantDto> RemovedParticipants { get; set; }
    }
}

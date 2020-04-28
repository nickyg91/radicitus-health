using System.Collections.Generic;
using Radicitus.Health.Dto.Dto;

namespace Radicitus.Health.Models
{
    public class UpdateParticipantsModel
    {
        public List<HealthParticipantDto> UpdatedParticipants { get; set; }
        public List<HealthParticipantDto> RemovedParticipants { get; set; }
    }
}

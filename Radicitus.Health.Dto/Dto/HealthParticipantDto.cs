using System.Collections.Generic;

namespace Radicitus.Health.Dto.Dto
{
    public class HealthParticipantDto : IHealthParticipant
    {
        public int Id { get; set; }

        public int HealthInitiativeId { get; set; }

        public string Name { get; set; }

        public decimal IndividualWeightLossGoal { get; set; }

        public List<IParticipantLog> ParticipantLogs { get; set; }
    }
}

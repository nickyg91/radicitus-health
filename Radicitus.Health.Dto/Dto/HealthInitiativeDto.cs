using System;
using System.Collections.Generic;

namespace Radicitus.Health.Dto.Dto
{
    public class HealthInitiativeDto : IHealthInitiative
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalWeightLossGoal { get; set; }
        public List<HealthParticipantDto> Participants { get; set; }

        public bool IsCurrent { get; set; }

        public DateTime? StartDateTime { get; set; }
    }
}

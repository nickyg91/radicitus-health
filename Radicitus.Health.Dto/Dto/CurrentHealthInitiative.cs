using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Radicitus.Health.Dto.Dto
{
    public class CurrentHealthInitiative
    {
        [JsonPropertyName("healthInitiative")]
        public HealthInitiativeDto HealthInitiative { get; set; }
        [JsonPropertyName("leaderboard")]
        public List<LeaderboardParticipantDto> Leaderboard { get; set; }
        [JsonPropertyName("goal")]
        public int Goal { get; set; }
        [JsonPropertyName("percentFinished")]
        public decimal PercentFinished
            => Leaderboard == null ? 0.0m : Math.Round(
                Leaderboard.Sum(x => x.PoundsLost) / HealthInitiative.TotalWeightLossGoal, 2);
    }
}

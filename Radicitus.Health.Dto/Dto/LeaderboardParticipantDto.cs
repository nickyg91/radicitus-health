using System;
using System.Text.Json.Serialization;

namespace Radicitus.Health.Dto.Dto
{
    public class LeaderboardParticipantDto
    {
        [JsonPropertyName("place")]
        public int Place { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("proundsLost")]
        public decimal PoundsLost { get; set; }
        [JsonPropertyName("goalTotal")]
        public decimal GoalTotal { get; set; }
        [JsonPropertyName("percentTowardsGoal")]
        public decimal PercentTowardsGoal => Math.Round(PoundsLost / GoalTotal, 2);
        [JsonPropertyName("points")]
        public int Points { get; set; }
    }
}

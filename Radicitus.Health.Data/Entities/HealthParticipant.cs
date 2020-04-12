using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Radicitus.Health.Dto;

namespace Radicitus.Health.Data.Entities
{
    [Table("HealthParticipant")]
    public class HealthParticipant : IHealthParticipant
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("HealthInitiative")]
        public int HealthInitiativeId { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal IndividualWeightLossGoal { get; set; }

        public HealthInitiative HealthInitiative { get; set; }
    }
}

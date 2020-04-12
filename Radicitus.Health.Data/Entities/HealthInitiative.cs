using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Radicitus.Health.Dto;

namespace Radicitus.Health.Data.Entities
{
    [Table("HealthInitiative")]
    public class HealthInitiative : IHealthInitiative
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal TotalWeightLossGoal { get; set; }
    }
}

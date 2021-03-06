using System;
using System.Collections.Generic;
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
        public bool IsCurrent { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public List<HealthParticipant> HealthParticipants { get; set; } = new List<HealthParticipant>();
    }
}

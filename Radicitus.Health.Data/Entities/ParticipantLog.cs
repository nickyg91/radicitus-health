using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Radicitus.Health.Dto;

namespace Radicitus.Health.Data.Entities
{
    [Table("ParticipantLog")]
    public class ParticipantLog : IParticipantLog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("HealthParticipant")]
        public int ParticipantId { get; set; }
        [ForeignKey("HealthInitiative")]
        public int HealthInitiativeId { get; set; }
        public byte[] Photo { get; set; }
        [Column(TypeName = "decimal(4,1)")]
        public decimal CurrentWeight { get; set; }
        public HealthParticipant HealthParticipant { get; set; }
        public HealthInitiative HealthInitiative { get; set; }
        public DateTime DateSubmitted { get; set; }
        public byte Points { get; set; }
    }
}

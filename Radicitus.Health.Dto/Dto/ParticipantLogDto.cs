using System;

namespace Radicitus.Health.Dto.Dto
{
    public class ParticipantLogDto : IParticipantLog
    {
        public int Id { get; set; }

        public int ParticipantId { get; set; }
        public int HealthInitiativeId { get; set; }

        public byte[] Photo { get; set; }

        public decimal CurrentWeight { get; set; }

        public string PhotoBase64 => Photo != null && Photo.Length > 0 ? Convert.ToBase64String(Photo) : null;
    }
}

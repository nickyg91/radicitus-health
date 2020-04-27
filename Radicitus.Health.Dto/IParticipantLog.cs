using System;

namespace Radicitus.Health.Dto
{
    public interface IParticipantLog
    {
        int Id { get; }
        int ParticipantId { get; }
        int HealthInitiativeId { get; }
        byte[] Photo { get; }
        decimal CurrentWeight { get; }
        DateTime DateSubmitted { get; }
        byte Points { get; }
    }
}

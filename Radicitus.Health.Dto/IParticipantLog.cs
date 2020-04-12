namespace Radicitus.Health.Dto
{
    public interface IParticipantLog
    {
        int Id { get; }
        int ParticipantId { get; }
        int HealthInitiativeId { get; }
        byte[] Photo { get; }
        decimal CurrentWeight { get; }
    }
}

namespace Radicitus.Health.Dto
{
    public interface IHealthParticipant
    {
        int Id { get; }
        int HealthInitiativeId { get; }
        string Name { get; }
        decimal IndividualWeightLossGoal { get; }
        decimal StartingWeight { get; }
    }
}

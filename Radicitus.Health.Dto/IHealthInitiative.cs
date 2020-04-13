using System;

namespace Radicitus.Health.Dto
{
    public interface IHealthInitiative
    {
        int Id { get; }
        string Name { get; }
        decimal TotalWeightLossGoal { get; }
        bool IsCurrent { get; }
        DateTime StartDateTime { get; }
    }
}

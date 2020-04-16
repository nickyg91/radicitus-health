using System.Collections.Generic;
using System.Threading.Tasks;
using Radicitus.Health.Data.Entities;

namespace Radicitus.Health.Data.Repositories.Interfaces
{
    public interface IHealthInitiativeRepository
    {
        Task InsertHealthInitiativeAsync(HealthInitiative initiative);
        Task DeleteHealthInitiativeAsync(int id);
        Task<List<HealthInitiative>> GetHealthInitativesAsync();
        Task<HealthInitiative> GetCurrentHealthInitiativeAsync();
    }
}

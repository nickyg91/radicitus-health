using System.Collections.Generic;
using System.Threading.Tasks;
using Radicitus.Health.Data.Entities;

namespace Radicitus.Health.Data.Repositories.Interfaces
{
    public interface IHealthParticipantRepository
    {
        Task<List<HealthParticipant>> GetHealthParticipantsByInitiativeId(int id);
    }
}

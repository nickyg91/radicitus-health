using System.Collections.Generic;
using System.Threading.Tasks;
using Radicitus.Health.Data.Entities;
using Radicitus.Health.Dto;

namespace Radicitus.Health.Data.Repositories.Interfaces
{
    public interface IHealthParticipantRepository
    {
        Task<List<HealthParticipant>> GetHealthParticipantsByInitiativeId(int id);
        Task UpdateParticipants(List<IHealthParticipant> participants, int initiativeId);
        Task RemoveParticipants(List<int> participantIds);
    }
}

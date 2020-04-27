using System.Collections.Generic;
using System.Threading.Tasks;
using Radicitus.Health.Data.Entities;

namespace Radicitus.Health.Data.Repositories.Interfaces
{
    public interface IParticipantLogRepository
    {
        Task AddParticipantLog(ParticipantLog log);
        Task<List<ParticipantLog>> GetParticipantLogsByInitiativeId(int id);
        Task<ParticipantLog> GetLastParticipantLogForParticipantId(int id);
    }
}

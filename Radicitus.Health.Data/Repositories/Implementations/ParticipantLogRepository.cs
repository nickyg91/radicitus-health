using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Radicitus.Health.Data.Contexts;
using Radicitus.Health.Data.Entities;
using Radicitus.Health.Data.Repositories.Interfaces;

namespace Radicitus.Health.Data.Repositories.Implementations
{
    public class ParticipantLogRepository : IParticipantLogRepository
    {
        private readonly RadicitusHealthContext _context;
        public ParticipantLogRepository(RadicitusHealthContext context)
        {
            _context = context;
        }

        public async Task AddParticipantLog(ParticipantLog log)
        {
            await _context.ParticipantLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<ParticipantLog> GetLastParticipantLogForParticipantId(int id)
        {
            return await _context.ParticipantLogs
                .Include(x => x.HealthParticipant)
                .Where(x => x.ParticipantId == id)
                .OrderByDescending(x => x.DateSubmitted).FirstOrDefaultAsync();
        }

        public async Task<List<ParticipantLog>> GetParticipantLogsByInitiativeId(int id)
        {
            return await _context.ParticipantLogs.Where(x => x.HealthInitiativeId == id).ToListAsync();
        }
    }
}

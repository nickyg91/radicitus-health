using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Radicitus.Health.Data.Contexts;
using Radicitus.Health.Data.Entities;
using Radicitus.Health.Data.Repositories.Interfaces;

namespace Radicitus.Health.Data.Repositories.Implementations
{
    public class HealthParticipantRepository : IHealthParticipantRepository
    {
        private readonly RadicitusHealthContext _context;

        public HealthParticipantRepository(RadicitusHealthContext context)
        {
            _context = context;
        }

        public async Task<List<HealthParticipant>> GetHealthParticipantsByInitiativeId(int id)
        {
            return await _context.HealthParticipants
                .Include(x => x.ParticipantLogs)
                .Where(x => x.HealthInitiativeId == id).ToListAsync();
        }
    }
}

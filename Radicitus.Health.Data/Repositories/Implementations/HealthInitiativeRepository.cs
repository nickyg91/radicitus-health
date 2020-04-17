using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Radicitus.Health.Data.Contexts;
using Radicitus.Health.Data.Entities;
using Radicitus.Health.Data.Repositories.Interfaces;

namespace Radicitus.Health.Data.Repositories.Implementations
{
    public class HealthInitiativeRepository : IHealthInitiativeRepository
    {
        private readonly RadicitusHealthContext _db;
        public HealthInitiativeRepository(RadicitusHealthContext db)
        {
            _db = db;
        }

        public async Task DeleteHealthInitiativeAsync(int id)
        {
            var initiativeToDelete = _db.HealthInitiatives.SingleOrDefault(x => x.Id == id);
            _db.HealthInitiatives.Remove(initiativeToDelete);
            await _db.SaveChangesAsync();
        }

        public async Task<List<HealthInitiative>> GetHealthInitativesAsync()
        {
            return await Task.FromResult(_db.HealthInitiatives
                .Include(x => x.HealthParticipants).AsQueryable().ToList());
        }

        public async Task InsertHealthInitiativeAsync(HealthInitiative initiative)
        {
            await _db.HealthInitiatives.AddAsync(initiative);
            await _db.SaveChangesAsync();
        }

        public async Task<HealthInitiative> GetCurrentHealthInitiativeAsync()
        {
            var now = DateTime.Now;
            return await _db.HealthInitiatives
                .Include(x => x.HealthParticipants)
                .ThenInclude(x => x.ParticipantLogs)
                .SingleOrDefaultAsync(x => x.StartDateTime.Value.Date > now.Date
                    && x.EndDateTime.Value.Date <= now.Date);
        }
    }
}

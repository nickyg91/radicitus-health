using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Radicitus.Health.Data.Contexts;
using Radicitus.Health.Data.Entities;
using Radicitus.Health.Data.Repositories.Interfaces;
using Radicitus.Health.Dto;

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

        public async Task RemoveParticipants(List<int> participantIds)
        {
            var participants = await _context.HealthParticipants.Join(participantIds, x => x.Id, y => y, (x, y) => x).ToListAsync();
            _context.HealthParticipants.RemoveRange(participants);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateParticipants(List<IHealthParticipant> participants, int initiativeId)
        {
            var toUpdate = (await GetHealthParticipantsByInitiativeId(initiativeId)).ToDictionary(x => x.Id, x => x);
            var toAdd = new List<HealthParticipant>();
            foreach (var participant in participants)
            {
                if (toUpdate.ContainsKey(participant.Id))
                {
                    toUpdate[participant.Id].IndividualWeightLossGoal = participant.IndividualWeightLossGoal;
                    toUpdate[participant.Id].StartingWeight = participant.StartingWeight;
                    toUpdate[participant.Id].Name = participant.Name;
                }
                else
                {
                    toAdd.Add(new HealthParticipant
                    {
                        HealthInitiativeId = participant.HealthInitiativeId,
                        Name = participant.Name,
                        StartingWeight = participant.StartingWeight,
                        IndividualWeightLossGoal = participant.IndividualWeightLossGoal
                    });
                }

            }
            _context.HealthParticipants.AddRange(toAdd);
            await _context.SaveChangesAsync();
        }
    }
}

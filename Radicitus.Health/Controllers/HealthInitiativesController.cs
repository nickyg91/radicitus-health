using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Health.Data.Entities;
using Radicitus.Health.Data.Repositories.Interfaces;
using Radicitus.Health.Dto.Dto;

namespace Radicitus.Health.Controllers
{
    [ApiController, Route("/api/health/initiatives")]
    public class HealthInitiativesController : Controller
    {
        private readonly IHealthInitiativeRepository _repo;
        public HealthInitiativesController(IHealthInitiativeRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInitiative(HealthInitiativeDto model)
        {
            var dbEntity = new HealthInitiative
            {
                Id = model.Id,
                Name = model.Name,
                TotalWeightLossGoal = model.TotalWeightLossGoal,
                HealthParticipants = model.Participants?.Select(x => new HealthParticipant
                {
                    IndividualWeightLossGoal = x.IndividualWeightLossGoal,
                    Name = x.Name
                }).ToList(),
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime
            };
            await _repo.InsertHealthInitiativeAsync(dbEntity);

            return Ok(dbEntity.Id);
        }

        // [HttpGet("current")]
        // public async Task<IActionResult> GetCurrentInitiative()
        // {
        //     var dbInitiative = await _repo.GetCurrentHealthInitiativeAsync();
        //     var leaderboard = new CurrentHealthInitiative
        //     {
        //         HealthInitiative = new HealthInitiativeDto
        //         {
        //             Name = dbInitiative.Name,
        //             TotalWeightLossGoal = dbInitiative.TotalWeightLossGoal
        //         },
        //     };
        // }

        [HttpGet("all")]
        public async Task<IActionResult> GetHealthInitiatives()
        {
            var items = await _repo.GetHealthInitativesAsync();
            var dtos = items.Select(x =>
            {
                var participants = x.HealthParticipants.Select(y =>
                {
                    return new HealthParticipantDto
                    {
                        HealthInitiativeId = y.HealthInitiativeId,
                        Name = y.Name,
                        IndividualWeightLossGoal = y.IndividualWeightLossGoal,
                        Id = y.Id
                    };
                });

                var dto = new HealthInitiativeDto
                {
                    Id = x.Id,
                    Participants = participants.ToList(),
                    StartDateTime = x.StartDateTime,
                    EndDateTime = x.EndDateTime,
                    Name = x.Name,
                    TotalWeightLossGoal = x.TotalWeightLossGoal
                };
                return dto;
            });
            return Ok(dtos);
        }

        [HttpDelete("remove/{id:int}")]
        public async Task<IActionResult> RemoveHealthInitiative(int id)
        {
            await _repo.DeleteHealthInitiativeAsync(id);
            return Ok();
        }

        // private LeaderboardParticipantDto MapParticipantsToLeaderboard(HealthInitiative initiative)
        // {
        //     var dict = initiative
        //         .HealthParticipants
        //         .SelectMany(x => x.ParticipantLogs).GroupBy(x => x.ParticipantId, x => )
        // }
    }
}

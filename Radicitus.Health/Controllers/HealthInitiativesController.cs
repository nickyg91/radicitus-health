using System;
using System.Collections.Generic;
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
                    Name = x.Name,
                    StartingWeight = x.StartingWeight
                }).ToList(),
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime
            };
            await _repo.InsertHealthInitiativeAsync(dbEntity);

            return Ok(dbEntity.Id);
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentInitiative()
        {
            var dbInitiative = await _repo.GetCurrentHealthInitiativeAsync();
            if (dbInitiative == null)
            {
                return Ok(new CurrentHealthInitiative());
            }
            var leaderboard = TallyPointsAndMapParticipantsToLeaderboard(dbInitiative);
            var currentHealthInitiative = new CurrentHealthInitiative
            {
                HealthInitiative = new HealthInitiativeDto
                {
                    Name = dbInitiative.Name,
                    TotalWeightLossGoal = dbInitiative.TotalWeightLossGoal,
                    Id = dbInitiative.Id
                },
                Leaderboard = leaderboard,
                Goal = (int)leaderboard.Sum(x => x.PoundsLost),

            };
            var now = DateTime.Now;
            var daysSinceStart = (dbInitiative.StartDateTime.Value.Date - now.Date).Days;
            var daysUntilEnd = (dbInitiative.EndDateTime.Value.Date - now.Date).Days;
            currentHealthInitiative.CanSubmitPicture = daysSinceStart <= 7 && daysSinceStart >= 0 || daysUntilEnd <= 7 && daysUntilEnd >= 0;

            return Ok(currentHealthInitiative);
        }

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
                        StartingWeight = y.StartingWeight,
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

        private List<LeaderboardParticipantDto> TallyPointsAndMapParticipantsToLeaderboard(HealthInitiative initiative)
        {
            if (initiative.HealthParticipants != null)
            {
                var participantLogs = initiative.HealthParticipants
                                .SelectMany(x => x.ParticipantLogs)
                                .GroupBy(x => x.ParticipantId)
                                .ToDictionary(x => x.Key, x => x.ToList());
                var leaderboardParticipants = new List<LeaderboardParticipantDto>();
                foreach (var participant in initiative.HealthParticipants)
                {
                    if (participant.ParticipantLogs.Any())
                    {
                        var lostWeight = 0.0m;
                        var overallWeightLoss = participantLogs[participant.Id]
                                            .OrderByDescending(x => x.CurrentWeight)
                                            .Select(x => x.CurrentWeight)
                                            .Aggregate(participant.StartingWeight, (total, next) =>
                                            {
                                                lostWeight += total - next;
                                                return next;
                                            });
                        var postPoints = participantLogs[participant.Id].Sum(x => x.Points);
                        var leaderboardParticipantDto = new LeaderboardParticipantDto
                        {
                            Name = participant.Name,
                            Points = postPoints,
                            PoundsLost = lostWeight,
                            PercentTowardsGoal = Math.Round((lostWeight / (participant.StartingWeight - participant.IndividualWeightLossGoal)) * 100)
                        };
                        leaderboardParticipants.Add(leaderboardParticipantDto);
                    }
                    else
                    {
                        var leaderboardParticipantDto = new LeaderboardParticipantDto
                        {
                            Name = participant.Name,
                            Points = 0,
                            PoundsLost = 0,
                            PercentTowardsGoal = 0
                        };
                        leaderboardParticipants.Add(leaderboardParticipantDto);
                    }
                }
                return leaderboardParticipants.OrderByDescending(x => x.Points).Select((item, index) =>
                {
                    item.Place = index + 1;
                    return item;
                }).ToList();
            }
            return new List<LeaderboardParticipantDto>();
        }
    }
}

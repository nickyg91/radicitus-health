using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radicitus.Health.Data.Contexts;
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
                HealthParticipants = model.Participants.Select(x => new HealthParticipant
                {
                    IndividualWeightLossGoal = x.IndividualWeightLossGoal,
                    Name = x.Name
                }).ToList()
            };
            await _repo.InsertHealthInitiativeAsync(dbEntity);

            return Ok(dbEntity.Id);
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
                        Id = y.Id
                    };
                });

                var dto = new HealthInitiativeDto
                {
                    Id = x.Id,
                    Participants = participants.ToList(),
                    StartDateTime = x.StartDateTime,
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
    }
}

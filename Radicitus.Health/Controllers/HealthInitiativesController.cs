using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radicitus.Health.Data.Contexts;
using Radicitus.Health.Data.Entities;
using Radicitus.Health.Dto.Dto;

namespace Radicitus.Health.Controllers
{
    [ApiController, Route("/api/health/initiatives")]
    public class HealthInitiativesController : Controller
    {
        private readonly RadicitusHealthContext _db;
        public HealthInitiativesController(RadicitusHealthContext dbContext)
        {
            _db = dbContext;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInitiative(HealthInitiativeDto model)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var dbEntity = new HealthInitiative
                {
                    Id = model.Id,
                    Name = model.Name,
                    TotalWeightLossGoal = model.TotalWeightLossGoal
                };
                var participants = model.Participants.Select(x =>
                {
                    return new HealthParticipant
                    {
                        IndividualWeightLossGoal = x.IndividualWeightLossGoal,
                        Name = x.Name
                    };
                }).ToList();
                dbEntity.HealthParticipants.AddRange(participants);
                _db.HealthInitiatives.Add(dbEntity);
                await _db.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetHealthInitiatives()
        {
            var items = _db.HealthInitiatives.Include(x => x.HealthParticipants).AsEnumerable();
            return Ok();
        }
    }
}

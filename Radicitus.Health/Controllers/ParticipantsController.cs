using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Health.Data.Contexts;
using Radicitus.Health.Data.Repositories.Interfaces;
using Radicitus.Health.Dto;
using Radicitus.Health.Dto.Dto;
using Radicitus.Health.Models;

namespace Radicitus.Health.Controllers
{
    [ApiController, Route("/api/participants")]
    public class ParticipantsController : Controller
    {
        private readonly IHealthParticipantRepository _repo;
        private readonly RadicitusHealthContext _db;
        public ParticipantsController(IHealthParticipantRepository repo, RadicitusHealthContext db)
        {
            _repo = repo;
            _db = db;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHealthParticipants(int id)
        {
            var participants = (await _repo.GetHealthParticipantsByInitiativeId(id)).Select(x => new HealthParticipantDto
            {
                Name = x.Name,
                Id = x.Id,
                ParticipantLogs = x.ParticipantLogs != null ? x.ParticipantLogs.Select(y => new ParticipantLogDto
                {
                    CurrentWeight = y.CurrentWeight,
                    DateSubmitted = y.DateSubmitted,
                    Photo = y.Photo,
                    Points = y.Points,
                    Id = y.Id
                }).OrderBy(y => y.DateSubmitted).ToList() : new List<ParticipantLogDto>()
            });
            return Ok(participants);
        }

        [HttpPatch("update/{id:int}")]
        public async Task<IActionResult> UpdateParticipants(int id, [FromBody]UpdateParticipantsModel model)
        {
            using var transaction = _db.Database.BeginTransaction();
            await _repo.RemoveParticipants(model.RemovedParticipants.Select(x => x.Id).ToList());
            await _repo.UpdateParticipants(model.UpdatedParticipants.ToList<IHealthParticipant>(), id);
            await transaction.CommitAsync();
            return Ok();
        }
    }
}

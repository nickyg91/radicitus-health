using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Health.Data.Repositories.Interfaces;
using Radicitus.Health.Dto.Dto;

namespace Radicitus.Health.Controllers
{
    [ApiController, Route("/api/participants")]
    public class ParticipantsController : Controller
    {
        private readonly IHealthParticipantRepository _repo;

        public ParticipantsController(IHealthParticipantRepository repo)
        {
            _repo = repo;
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
    }
}
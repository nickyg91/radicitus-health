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
                Id = x.Id
            });
            return Ok(participants);
        }
    }
}

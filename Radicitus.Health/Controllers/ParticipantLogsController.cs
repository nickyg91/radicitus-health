using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Health.Data.Entities;
using Radicitus.Health.Data.Repositories.Interfaces;
using Radicitus.Health.Models;

namespace Radicitus.Health.Controllers
{
    [ApiController, Route("/api/health/participants/logs")]
    public class ParticipantLogsController : Controller
    {
        private readonly IParticipantLogRepository _repo;

        public ParticipantLogsController(IParticipantLogRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Save(ParticipantLogModel model)
        {
            byte[] photoBytes = null;
            if (!string.IsNullOrEmpty(model.PhotoBase64))
            {
                photoBytes = Convert.FromBase64String(model.PhotoBase64);
            }

            var participantLog = new ParticipantLog
            {
                CurrentWeight = model.ParticipantLog.CurrentWeight,
                HealthInitiativeId = model.HealthInitiativeId,
                ParticipantId = model.ParticipantLog.ParticipantId,
                Photo = photoBytes,
                DateSubmitted = DateTime.Now
            };

            await _repo.AddParticipantLog(participantLog);

            return Ok();
        }
    }
}

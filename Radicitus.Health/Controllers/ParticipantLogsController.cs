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

            var priorLog = await _repo.GetLastParticipantLogForParticipantId(model.ParticipantLog.ParticipantId);
            var currentDate = DateTime.Now;

            if (priorLog != null)
            {
                var dateDiffInDays = (currentDate - priorLog.DateSubmitted).Days;
                if (dateDiffInDays < 7)
                {
                    return BadRequest("You may not submit more than once a week.");
                }
            }

            var participantLog = new ParticipantLog
            {
                CurrentWeight = model.ParticipantLog.CurrentWeight,
                HealthInitiativeId = model.HealthInitiativeId,
                ParticipantId = model.ParticipantLog.ParticipantId,
                Photo = photoBytes,
                DateSubmitted = DateTime.Now,
                Points = 1
            };

            if (priorLog != null && priorLog.CurrentWeight > participantLog.CurrentWeight)
            {
                participantLog.Points += 1;
            }

            if (priorLog != null && participantLog.CurrentWeight <= priorLog.HealthParticipant.IndividualWeightLossGoal)
            {
                participantLog.Points += 5;
            }

            if (photoBytes != null)
            {
                participantLog.Points += 3;
            }

            await _repo.AddParticipantLog(participantLog);

            return Ok();
        }
    }
}

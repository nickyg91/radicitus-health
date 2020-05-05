using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Health.Dto.Dto;
using Radicitus.Health.Redis.RadicitusRedis;

namespace Radicitus.Health.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IRadicitusRedisDataRepository _repo;
        public ResourceController(IRadicitusRedisDataRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllResources()
        {
            var resources = await _repo.GetAllResourceItems();
            return Ok(resources);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit(ResourceItem item)
        {
            item.Guid = Guid.NewGuid();
            await _repo.StoreResourceItem(item);
            return Ok();
        }
    }
}

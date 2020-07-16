using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radicitus.Health.Redis.RadicitusRedis;
using Radicitus.Integration.Interfaces;

namespace Radicitus.Health.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkPreviewController : ControllerBase
    {
        private readonly IOpenGraphRepository _repo;
        private readonly IRadicitusRedisDataRepository _redis;
        public LinkPreviewController(IOpenGraphRepository repo, IRadicitusRedisDataRepository redis)
        {
            _repo = repo;
            _redis = redis;
        }

        [HttpGet("find/{guid}")]
        public async Task<IActionResult> GetLinkPreview(Guid guid)
        {
            var linkPreview = await _repo.GetLinkPreview(guid);
            if (linkPreview == null)
            {
                var resourceItem = await _redis.GetResourceItemByGuid(guid);
                if (!string.IsNullOrEmpty(resourceItem.Url))
                {
                    linkPreview = await _repo.ParseUrlAsync(new Uri(resourceItem.Url));
                    await _repo.StorePreview(linkPreview, guid);
                }
            }
            if (linkPreview != null)
            {
                linkPreview.Html = System.Web.HttpUtility.HtmlEncode(linkPreview.Html);
                if (linkPreview.Tracks != null && linkPreview.Tracks.Any())
                {
                    linkPreview.Tracks = linkPreview.Tracks.OrderBy(x => x.TrackNumber).ToList();
                }
            }
            return Ok(linkPreview);
        }
    }
}

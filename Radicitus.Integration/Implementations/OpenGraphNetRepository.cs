using System;
using System.Threading.Tasks;
using Radicitus.Integration.Interfaces;
using OpenGraphNet;
using Radicitus.Health.Dto.Dto;
using Radicitus.Health.Redis;
using Radicitus.Health.Redis.RadicitusRedis;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Radicitus.Integration.Implementations
{
    public class OpenGraphNetRepository : IOpenGraphRepository
    {
        private readonly IRadicitusRedisDataRepository _redis;
        public OpenGraphNetRepository(IRadicitusRedisDataRepository redis)
        {
            _redis = redis;
        }

        public async Task<LinkPreview> GetLinkPreview(Guid guid)
        {
            var json = await _redis.GetStringAsync($"{guid}:linkpreview");
            if (!string.IsNullOrEmpty(json))
            {
                return JsonSerializer.Deserialize<LinkPreview>(json);
            }
            return null;
        }

        public async Task<LinkPreview> ParseHtmlAsync(string html)
        {
            return await Task.Run(() =>
            {
                var openGraphData = OpenGraph.ParseHtml(html);
                if (openGraphData != null)
                {
                    return new LinkPreview
                    {
                        ImageUrl = openGraphData.Image,
                        Title = openGraphData.Title,
                        Html = openGraphData.OriginalHtml
                    };
                }
                return null;
            });
        }

        public async Task<LinkPreview> ParseUrlAsync(Uri url)
        {
            var openGraphData = await OpenGraph.ParseUrlAsync(url);
            if (openGraphData != null)
            {
                var tracks = new List<Track>();
                if (url.Host.ToLower().Contains("spotify"))
                {
                    var tracksToParse = openGraphData.Metadata["music:song"];
                    var trackCrawlerTasks = new List<Task>();
                    foreach (var track in tracksToParse)
                    {
                        var songLink = track.Value;
                        var songTask = Task.Run(async () =>
                        {
                            var data = await OpenGraph.ParseUrlAsync(songLink);
                            var trackToAdd = new Track
                            {
                                Name = data.Metadata["og:title"].FirstOrDefault()?.Value,
                                PlayUrl = data.Metadata["og:audio"].FirstOrDefault()?.Value
                            };
                            tracks.Add(trackToAdd);
                        });
                        trackCrawlerTasks.Add(songTask);
                    }
                    if (trackCrawlerTasks.Any())
                    {
                        await Task.WhenAll(trackCrawlerTasks);
                    }
                }
                return new LinkPreview
                {
                    ImageUrl = openGraphData.Image,
                    Title = openGraphData.Title,
                    Html = openGraphData.OriginalHtml,
                    Tracks = tracks.OrderBy(x => x.TrackNumber).ToList()
                };
            }
            return null;
        }

        public async Task StorePreview(LinkPreview preview, Guid guid)
        {
            var json = JsonSerializer.Serialize(preview);
            await _redis.StoreStringAsync($"{guid}:linkpreview", json);
        }
    }
}

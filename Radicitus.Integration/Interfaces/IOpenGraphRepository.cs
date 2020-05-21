using System;
using System.Threading.Tasks;
using Radicitus.Health.Dto.Dto;

namespace Radicitus.Integration.Interfaces
{
    public interface IOpenGraphRepository
    {
        Task<LinkPreview> ParseUrlAsync(Uri url);
        Task<LinkPreview> ParseHtmlAsync(string html);
        Task StorePreview(LinkPreview preview, Guid guid);
        Task<LinkPreview> GetLinkPreview(Guid guid);
    }
}

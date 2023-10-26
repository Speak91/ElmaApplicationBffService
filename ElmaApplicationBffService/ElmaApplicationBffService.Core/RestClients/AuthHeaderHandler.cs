using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ElmaApplicationBffService.Core.Options;
using Microsoft.Extensions.Options;

namespace ElmaApplicationBffService.Core.RestClients
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly ElmaOptions _options;

        public AuthHeaderHandler(IOptions<ElmaOptions> options)
        {
            _options = options.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.Token);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}

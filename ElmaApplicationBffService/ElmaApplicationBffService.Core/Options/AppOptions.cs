using Microsoft.AspNetCore.Cors.Infrastructure;

namespace ElmaApplicationBffService.Core.Options
{
    public class AppOptions
    {
        public bool IsSut { get; set; }
        public string Issuer { get; set; }
        public IDictionary<string, CorsPolicy> CorsPolicies { get; set; }
    }
}
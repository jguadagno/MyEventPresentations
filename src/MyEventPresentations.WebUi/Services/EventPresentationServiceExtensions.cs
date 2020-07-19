using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyEventPresentations.WebUi.Services
{
    public static class EventPresentationServiceExtensions
    {
        public static void AddEventPresentationService(this IServiceCollection services)
        {
            // https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddHttpClient<IEventPresentationService, EventPresentationService>();
        }
    }
}
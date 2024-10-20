using Sasayaku.Common.Api;
using Sasayaku.Endpoints.Authentication;
using Sasayaku.Endpoints.Clients;

namespace Sasayaku.Endpoints
{
    public static class EndpointMapper
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("api/")
                .WithOpenApi();

            endpoints.MapClientEndpoints();

            endpoints.MapPublicGroup(string.Empty)
                .WithTags("Authentication")
                .MapEndpoint<Authenticate>();
        }

        private static void MapClientEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPublicGroup("/clients")
                .WithTags("Clients")
                .MapEndpoint<GetClient>()
                .MapEndpoint<GetClients>();
        }

        private static RouteGroupBuilder MapPublicGroup(
            this IEndpointRouteBuilder app,
            string? prefix = null)
        {
            return app.MapGroup(prefix ?? string.Empty)
                .AllowAnonymous();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(
            this IEndpointRouteBuilder app)

            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}

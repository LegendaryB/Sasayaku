using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

using Sasayaku.Common.Api;
using Sasayaku.Endpoints.Authentication;
using Sasayaku.Endpoints.Clients;

namespace Sasayaku.Endpoints
{
    public static class EndpointMapper
    {
        private static readonly OpenApiSecurityScheme _securityScheme = new()
        {
            Type = SecuritySchemeType.Http,
            Name = JwtBearerDefaults.AuthenticationScheme,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Reference = new()
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        };

        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("api/")
                .WithOpenApi();

            endpoints.MapClientEndpoints();

            endpoints.MapPublicGroup(string.Empty)
                .WithTags("Authentication")
                .MapEndpoint<Authenticate>();

            endpoints.MapAuthorizedGroup(string.Empty)
                .WithTags("Authentication")
                .MapEndpoint<Register>();
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

        private static RouteGroupBuilder MapAuthorizedGroup(this IEndpointRouteBuilder app, string? prefix = null)
        {
            return app.MapGroup(prefix ?? string.Empty)
                .RequireAuthorization()
                .WithOpenApi(x => new(x)
                {
                    Security = [new() { [_securityScheme] = [] }],
                });
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

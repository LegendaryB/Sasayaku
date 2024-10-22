using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Sasayaku.Common.Api;
using Sasayaku.Data;
using Sasayaku.Services;

namespace Sasayaku.Endpoints.Authentication
{
    [Authorize(Roles = "admin")]
    public class Register : IEndpoint
    {
        private record Request();

        private record Response(ClientCredentials Credentials);

        public static void Map(IEndpointRouteBuilder app) => app
            .MapPost("/register", HandleAsync)
            .WithSummary("Creates a new client id and client secret");

        private static async Task<Ok<Response>> HandleAsync(
            [AsParameters] Request request,
            [FromServices] IUserService userService,
            CancellationToken cancellationToken)
        {
            var credentials = new ClientCredentials();
            var response = new Response(credentials);

            return TypedResults.Ok(response);
        }
    }
}

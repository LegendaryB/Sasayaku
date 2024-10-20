using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Sasayaku.Common.Api;
using Sasayaku.Data;
using Sasayaku.Services;

namespace Sasayaku.Endpoints.Authentication
{
    public class Authenticate : IEndpoint
    {
        private record Request(ClientCredentials Credentials);

        private record Response(string Token);

        public static void Map(IEndpointRouteBuilder app) => app
            .MapPost("/authenticate", HandleAsync)
            .WithSummary("Gets a token by client id and client secret");

        private static async Task<Results<Ok<Response>, ValidationProblem>> HandleAsync(
            [AsParameters] Request request,
            [FromServices] IUserService userService,
            [FromServices] JwtTokenService tokenService,
            CancellationToken cancellationToken)
        {


            var token = tokenService.GenerateToken(request.Credentials);

            return TypedResults.ValidationProblem(new Dictionary<string, string[]>());
        }
    }
}


using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Sasayaku.Common.Api;
using Sasayaku.Configuration;
using Sasayaku.Data;
using Sasayaku.Services;

namespace Sasayaku.Endpoints.Clients
{
    public class GetClient : IEndpoint
    {
        private record Request(int Id);

        private record Response(
            int Id);

        public static void Map(IEndpointRouteBuilder app) => app
            .MapGet("/{id}", HandleAsync)
            .WithSummary("Gets a client by id");

        private static async Task<Results<Ok<Response>, NotFound>> HandleAsync(
            [AsParameters] Request request,
            [FromServices] IUserService userService,
            CancellationToken cancellationToken)
        {

            return TypedResults.NotFound();
            //var client = await database
            //    .Clients
            //    .SingleOrDefaultAsync(
            //        c => c.Id == request.Id,
            //        cancellationToken);

            //return client is null
            //    ? TypedResults.NotFound()
            //    : TypedResults.Ok(
            //    new Response(
            //        client.Id));
        }
    }
}

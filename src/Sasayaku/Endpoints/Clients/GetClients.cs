using Microsoft.AspNetCore.Mvc;

using Sasayaku.Common.Api;
using Sasayaku.Common.Requests;
using Sasayaku.Data;

namespace Sasayaku.Endpoints.Clients
{
    public class GetClients : IEndpoint
    {
        private record Request(
            int? Page,
            int? PageSize) : IPagedRequest;

        private record Response(
            int Id,
            string Name);

        public static void Map(IEndpointRouteBuilder app) => app
            .MapGet("/", HandleAsync)
            .WithSummary("Gets all clients");

        private static async Task<PagedList<Response>> HandleAsync(
            [AsParameters] Request request,
            [FromServices] AppDbContext database,
            CancellationToken cancellationToken)
        {
            return await database
                .Clients
                .Select(c => new Response(c.Id, string.Empty))
                .ToPagedListAsync(request, cancellationToken);
        }
    }
}

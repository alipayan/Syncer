using Syncer.APIs.Models.Domain;
using Syncer.APIs.Persistence;

namespace Syncer.APIs.Endpoints;

public static class PresentationEndpoints
{
    public static void MapPresentationEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var group = endpoint.MapGroup("presentations")
            .WithTags("Presentation");

        group.MapPost("/{speaker}", static async (CreatePresentationRequest request, string speaker, SyncerDbContext dbContext) =>
        {
            try
            {
                var presentation = Presentation.Create(request.UnifiedId, request.Title, request.Description, speaker);

                await dbContext.Presentations.AddAsync(presentation);
                await dbContext.SaveChangesAsync();
                return Results.Ok();
            }
            catch (Exception)
            {
                return Results.BadRequest();
            }
        });
    }
}

public record CreatePresentationRequest
    (string UnifiedId, string Title, string Description, string Speaker);

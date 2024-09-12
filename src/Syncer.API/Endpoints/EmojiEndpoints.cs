using Syncer.APIs.Models.Domain;
using Syncer.APIs.Persistence;

namespace Syncer.APIs.Endpoints;

public static class EmojiEndpoints
{
    public static void MapEmojiEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var group = endpoint.MapGroup("emojies")
            .WithTags("Emoji");

        group.MapPost("/", static async (string code, string name, SyncerDbContext dbContext) =>
        {
            try
            {
                await dbContext.Emojis.AddAsync(Emoji.Create(code, name));
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

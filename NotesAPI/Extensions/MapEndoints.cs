using NotesAPI.Endpoints;

namespace NotesAPI.Extensions;

public static class MapEndoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api");

        group.MapNoteEndpoints();
        group.MapAuthEndpoints();
    }
}

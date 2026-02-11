using System.Security.Claims;

namespace NotesAPI.Endpoints
{
    public static class NoteEndpoints
    {
        public static void MapNoteEndpoints(this RouteGroupBuilder group)
        {
            var notesGroup = group.MapGroup("notes").RequireAuthorization();

            notesGroup.MapGet("", Handler);
        }

        private static Task Handler(ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return Task.CompletedTask;
        }
    }
}

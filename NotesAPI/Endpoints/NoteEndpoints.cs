using Application.Inputs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace NotesAPI.Endpoints
{
    public static class NoteEndpoints
    {
        public static void MapNoteEndpoints(this RouteGroupBuilder group)
        {
            var notesGroup = group.MapGroup("notes").RequireAuthorization();

            notesGroup.MapPost("", CreateNoteHandler);
        }

        private async static Task CreateNoteHandler([FromBody] CreateNoteRequest request, [FromServices] INoteService noteService)
        {
            await noteService.CreatNoteAsync(request);
        }
    }
}

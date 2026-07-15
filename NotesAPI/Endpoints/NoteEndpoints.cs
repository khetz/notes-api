using Application.Inputs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace NotesAPI.Endpoints
{
    public static class NoteEndpoints
    {
        public static void MapNoteEndpoints(this RouteGroupBuilder group)
        {
            var notesGroup = group.MapGroup("notes").RequireAuthorization();

            notesGroup.MapPost("", CreateNoteHandler);
            notesGroup.MapPut("{id}", UpdateNoteHandler);
            notesGroup.MapDelete("{id}", DeleteNoteHandler);
            notesGroup.MapGet("{id}", GetNoteHandler);
            notesGroup.MapGet("", GetAllNotesHandler);
        }

        private async static Task CreateNoteHandler([FromBody] CreateNoteRequest request, [FromServices] INoteService noteService)
        {
            await noteService.CreatNoteAsync(request);
        }

        private async static Task<IResult> UpdateNoteHandler([FromBody] UpdateNoteRequest request, [FromServices] INoteService noteService,
            [FromRoute] int id)
        {
            if (id != request.Id) return Results.Problem("Route id does not match note id");

            var updateResult = await noteService.UpdateNoteAsync(request);

            return updateResult.MatchFirst(
                value => Results.Ok(value),
                firstError => Results.Problem(firstError.ToString()));
        }

        private async static Task<IResult> DeleteNoteHandler([FromRoute] int id, [FromServices] INoteService noteService)
        {
            var deletionResult = await noteService.DeleteNoteAsync(id);

            return deletionResult.MatchFirst(
                value => Results.Ok(deletionResult),
                firstError => Results.Problem(firstError.ToString()));
        }

        private async static Task<IResult> GetNoteHandler([FromRoute] int id, [FromServices] INoteService noteService)
        {
            var note = await noteService.GetNoteAsync(id);
            return note.MatchFirst(
                value => Results.Ok(note),
                firstError => Results.Problem(firstError.ToString()));
        }

        private async static Task<IResult> GetAllNotesHandler([FromServices] INoteService noteService)
        {
            var notes = await noteService.GetAllNotesAsync();
            return notes.MatchFirst(
                value => Results.Ok(value),
                firstError => Results.Problem(firstError.ToString()));
        }
    }
}

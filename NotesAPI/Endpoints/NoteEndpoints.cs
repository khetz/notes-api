namespace NotesAPI.Endpoints
{
    public static class NoteEndpoints
    {
        public static void MapNoteEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("notes");

            group.MapGet("", Handler);
        }

        private static Task Handler()
        {
            return Task.CompletedTask;
        }
    }
}

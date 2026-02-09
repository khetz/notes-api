namespace NotesAPI.Endpoints
{
    public static class NoteEndpoints
    {
        public static void MapNoteEndpoints(this RouteGroupBuilder group)
        {
            var notesGroup = group.MapGroup("notes");

            notesGroup.MapGet("", Handler);
        }

        private static Task Handler()
        {
            return Task.CompletedTask;
        }
    }
}

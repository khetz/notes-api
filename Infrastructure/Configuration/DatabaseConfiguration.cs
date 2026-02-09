namespace Infrastructure.Configuration
{
    public class DatabaseConfiguration
    {
        public const string NotesDatabase = "NotesDatabase";
        public required string ConnectionString { get; set; }
    }
}

using NotesAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services
    .AddConfig(builder.Configuration)
    .RegisterDbContexts();

var app = builder.Build();
app.Run ();
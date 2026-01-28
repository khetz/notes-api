using NotesAPI.Endpoints;
using NotesAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services
    .AddConfig(builder.Configuration)
    .RegisterDbContexts(builder.Configuration);

// OpenAPI/Swagger config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapNoteEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run ();
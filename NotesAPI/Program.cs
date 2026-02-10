using NotesAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services
    .AddConfig(builder.Configuration)
    .RegisterDbContexts(builder.Configuration)
    .RegisterServices()
    .AddJwtAuthentication(builder.Configuration);

// OpenAPI/Swagger config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseHttpsRedirection();

app.Run ();
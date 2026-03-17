using NotesAPI.Extensions;

var corsPolicyName = "AllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services
    .AddConfig(builder.Configuration)
    .RegisterDbContexts(builder.Configuration)
    .RegisterRepositories()
    .RegisterServices()
    .AddJwtAuthentication(builder.Configuration)
    .AddCorsPolicy(corsPolicyName);

// OpenAPI/Swagger config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors(corsPolicyName);
app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.Run ();
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MultiTenantEMS.API.Extensions;
using MultiTenantEMS.API.Middleware;
using MultiTenantEMS.Application;
using MultiTenantEMS.Infrastructure;
using MultiTenantEMS.Infrastructure.Persistence.MasterDb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomApiBehaviors();
builder.Services.AddIdentityAndAuth(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddHealthChecks().AddDbContextCheck<MasterDbContext>();

var app = builder.Build();

app.UseExceptionHandler();

await DatabaseHelper.ApplyMasterDBMigrations(app);
await DatabaseHelper.SeedMasterDbIdentityDetails(app);

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

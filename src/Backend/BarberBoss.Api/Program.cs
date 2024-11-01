using BarberBoss.Infrastructure;
using BarberBoss.Application;
using BarberBoss.Api.Filters;
using BarberBoss.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BarberBoss.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if(!builder.Configuration.IsTestEnvironment())
{
    ApplyMigrations(app.Services);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ApplyMigrations(IServiceProvider serviceProvider)
{
    var scope = serviceProvider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<BarberBossDbContext>();

    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

public partial class Program { }
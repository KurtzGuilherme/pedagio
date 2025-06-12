using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using Thunders.TechTest.ApiService;
using Thunders.TechTest.ApiService.Config;
using Thunders.TechTest.ApiService.Infra.Data.SqlServer;
using Thunders.TechTest.OutOfBox.Database;
using Thunders.TechTest.OutOfBox.Queues;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();

var features = Features.BindFromConfiguration(builder.Configuration);

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddDependencyInjection();
builder.Services.AddScoped<IMessageSender, RebusMessageSender>();

if (features.UseMessageBroker)
{
    builder.Services.AddBus(builder.Configuration, new SubscriptionBuilder());
}

if (features.UseEntityFramework)
{
    builder.Services.AddSqlServerDbContext<SqlServerContext>(builder.Configuration);
}

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddDbContext<SqlServerContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConnectionStrings:ThundersTechTestDb"),
        sqlServerOptions =>
        {
            sqlServerOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: new List<int> { 4060, 40197, 40501, 49918, 49919 }); // Códigos comuns de erros transitórios
            sqlServerOptions.CommandTimeout(60); // Timeout de 60 segundos
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();

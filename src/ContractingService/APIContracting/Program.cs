using Domain.Factories;
using Domain.Repository;
using Infrastructure;
using Infrastructure.PostgreRepositories.ServiceContractingRepository;
using Infrastructure.Resources.RabbitMq;
using Infrastructure.Resources.RabbitMq.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service.UseCases.ProposalUseCase.Interfaces;
using Service.UseCases.ProposalUseCase;
using Service.UseCases.ServiceContractingUseCase;
using Service.UseCases.ServiceContractingUseCase.Interfaces;
using System.Text.Json.Serialization;
using Infrastructure.PostgreRepositories.ProposalRepository;
using Polly;

var builder = WebApplication.CreateSlimBuilder(args);

// Configuração de serialização JSON
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

// Service Contracting Dependence Injection
builder.Services.AddDbContext<ServiceContractingContext>();
builder.Services.AddScoped<ServiceContractingFactory>();
builder.Services.AddScoped<IServiceContractingRepository, ServiceContractingPostgreRepository>();
builder.Services.AddScoped<IInsertServiceContractingUseCase, InsertServiceContractingUseCase>();
builder.Services.AddScoped<IUpdateServiceContractingUseCase, UpdateServiceContractingUseCase>();
builder.Services.AddScoped<IDeleteServiceContractingUseCase, DeleteServiceContractingUseCase>();
builder.Services.AddScoped<IReadServiceContractingUseCase, ReadServiceContractingUseCase>();
builder.Services.AddHostedService<RabbitMqBackgroundConsumer>();
builder.Services.AddSingleton<IRabbitMqSubscriber, RabbitMqSubscriber>();
builder.Services.AddSingleton<IMessageStore, InMemoryMessageStore>();

// Proposal Dependence Injection
builder.Services.AddScoped<ProposalFactory>();
builder.Services.AddScoped<IProposalRepository, ProposalPostgreRepository>();
builder.Services.AddScoped<IInsertProposalUseCase, InsertProposalUseCase>();
builder.Services.AddScoped<IReadProposalUseCase, ReadProposalUseCase>();
builder.Services.AddScoped<IProposalProcessorService, ProposalProcessorService>();

builder.Services.AddHostedService<ProposalApprovedConsumer>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "INTD API", Version = "v1" });
});

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ServiceContractingContext>();

    var retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetry(
            retryCount: 5,
            sleepDurationProvider: attempt => TimeSpan.FromSeconds(10),
            onRetry: (exception, timespan) =>
            {
                Console.WriteLine($"[WARN] Falha ao conectar no banco, tentando novamente em {timespan.TotalSeconds}s...");
            });

    retryPolicy.Execute(() =>
    {
        db.Database.Migrate();
    });
}

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "INT.Service API V1");
    c.RoutePrefix = string.Empty; // Swagger abre direto na raiz "/"
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}

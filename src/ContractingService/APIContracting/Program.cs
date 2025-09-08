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
using System;
using System.Text.Json.Serialization;
using Infrastructure.PostgreRepositories.ProposalRepository;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

//Service Contracting Dependence Injection
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

builder.Services.AddScoped<ProposalFactory>();
builder.Services.AddScoped<IProposalRepository, ProposalPostgreRepository>();
builder.Services.AddScoped<IInsertProposalUseCase, InsertProposalUseCase>();
builder.Services.AddScoped<IReadProposalUseCase, ReadProposalUseCase>();


builder.Services.AddScoped<IProposalProcessorService, ProposalProcessorService>();
builder.Services.AddHostedService<RabbitMqBackgroundConsumer>();
builder.Services.AddHostedService<ProposalApprovedConsumer>();

// Adiciona servi�os do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "INTD API", Version = "v1" });
});
builder.Services.AddControllers();
builder.Services.AddScoped<IProposalProcessorService, ProposalProcessorService>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ServiceContractingContext>();
    db.Database.Migrate(); // aplica migrations automaticamente
}

// Ativa middleware do Swagger s� no Development

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "INT.Service API V1");
    c.RoutePrefix = string.Empty; // Swagger abre direto na raiz "/"
});


app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}

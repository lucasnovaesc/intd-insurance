using Domain.Factories;
using Domain.Repository;
using Infrastructure;
using Infrastructure.PostgreRepositories.ServiceContractingRepository;
using Microsoft.OpenApi.Models;
using Service.UseCases.ServiceContractingUseCase;
using Service.UseCases.ServiceContractingUseCase.Interfaces;
using System.Text.Json.Serialization;

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

// Adiciona serviços do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "INTD API", Version = "v1" });
});
builder.Services.AddControllers();

var app = builder.Build();

// Ativa middleware do Swagger só no Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "INT.Service API V1");
        c.RoutePrefix = string.Empty; // Swagger abre direto na raiz "/"
    });
}

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}

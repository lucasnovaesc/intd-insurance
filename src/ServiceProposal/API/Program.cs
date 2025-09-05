using Domain.Factories;
using Domain.Repositories;
using Infrastruture;
using Infrastruture.PostgreRepository.CustomerRepository;
using Infrastruture.PostgreRepository.ProductRepository;
using Infrastruture.PostgreRepository.ProductTypeRepository;
using Infrastruture.PostgreRepository.ProposalRepository;
using Infrastruture.Resources.RabbitMQ;
using Infrastruture.Resources.RabbitMQ.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service.UseCases.CustomerUseCase;
using Service.UseCases.CustomerUseCase.Interfaces;
using Service.UseCases.ProductTypeUseCase;
using Service.UseCases.ProductTypeUseCase.Interfaces;
using Service.UseCases.ProductUseCase;
using Service.UseCases.ProductUseCase.Interfaces;
using Service.UseCases.ProposalUseCase;
using Service.UseCases.ProposalUseCase.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddDbContext<ServiceProposalContext>();

//var connectionString = builder.Configuration.GetConnectionString("ServiceProposalDb");

//// Injetar DbContext
//builder.Services.AddDbContext<ServiceProposalContext>(options =>
//    options.UseNpgsql(connectionString));
//product Dependence Injection
builder.Services.AddSingleton<IRabbitMQClient, RabbitMQClient>();
builder.Services.AddScoped<ProductFactory>();
builder.Services.AddScoped<IProductRepository, ProductPostgreRepository>();
builder.Services.AddScoped<IInsertProductUseCase, InsertProductUseCase>();
builder.Services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
builder.Services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
builder.Services.AddScoped<IReadProductUseCase, ReadProductUseCase>();

//Product Type
builder.Services.AddScoped<ProductTypeFactory>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypePostgreRepository>();
builder.Services.AddScoped<IInsertProductTypeUseCase, InsertProductTypeUseCase>();
builder.Services.AddScoped<IUpdateProductTypeUseCase, UpdateProductTypeUseCase>();
builder.Services.AddScoped<IDeleteProductTypeUseCase, DeleteProductTypeUseCase>();
builder.Services.AddScoped<IReadProductTypeUseCase, ReadProductTypeUseCase>();

//Customer
builder.Services.AddScoped<CustomerFactory>();
builder.Services.AddScoped<ICustomerRepository, CustomerPostgreRepository>();
builder.Services.AddScoped<IInsertCustomerUseCase, InsertCustomerUseCase>();
builder.Services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();
builder.Services.AddScoped<IDeleteCustomerUseCase, DeleteCustomerUseCase>();
builder.Services.AddScoped<IReadCustomerUseCase, ReadCustomerUseCase>();

//proposal
builder.Services.AddScoped<ProposalFactory>();
builder.Services.AddScoped<IProposalRepository, ProposalPostgreRepository>();
builder.Services.AddScoped<IInsertProposalUseCase, InsertProposalUseCase>();
builder.Services.AddScoped<IUpdateProposalUseCase, UpdateProposalUseCase>();
builder.Services.AddScoped<IDeleteProposalUseCase, DeleteProposalUseCase>();
builder.Services.AddScoped<IReadProposalUseCase, ReadProposalUseCase>();

// Adiciona servi�os do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "INTD API", Version = "v1" });
});
builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ServiceProposalContext>();
    db.Database.Migrate(); // aplica migrations automaticamente
}


// Ativa middleware do Swagger s� no Development
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "INT.Service API V1");
    c.RoutePrefix = string.Empty;
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

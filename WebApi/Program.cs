using Application.Interfaces;
using Infra.Elasticsearch;
using Infra.RabbitMQ;
using Infra.RabbitMQ.Config;
using Infra.RabbitMQ.Consumers;
using AutoMapper;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(VehicleSpecMapper));

//Elasticsearch
var settings = new ConnectionSettings(new Uri(builder.Configuration["ElasticsearchSettings:uri"]!));
var defaultIndex = builder.Configuration["ElasticsearchSettings:defaultIndex"];

if (!string.IsNullOrEmpty(defaultIndex))
    settings = settings.DefaultIndex(defaultIndex);

var basicAuthUser = builder.Configuration["ElasticsearchSettings:username"];
var basicAuthPassword = builder.Configuration["ElasticsearchSettings:password"];

if (!string.IsNullOrEmpty(basicAuthUser) && !string.IsNullOrEmpty(basicAuthPassword))
    settings = settings.BasicAuthentication(basicAuthUser, basicAuthPassword);

var client = new ElasticClient(settings);
builder.Services.AddSingleton<IElasticClient>(client);
builder.Services.AddSingleton<IVehicleSpecIndex, VehicleSpecIndex>();

//Mediatr
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

//RabbitMQ
builder.Services.Configure<RabbitMQConfig>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton<RabbitMQConnection>();
builder.Services.AddSingleton<VehicleSpecConsumer>();
var vehicleConsumer = builder.Services.BuildServiceProvider().GetRequiredService<VehicleSpecConsumer>();
vehicleConsumer.StartConsume();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

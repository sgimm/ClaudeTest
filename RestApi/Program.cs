using Azure.Messaging.ServiceBus;
using RestApi.Services;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();

var settings = new ServiceBusSettings(
    ConnectionString: Environment.GetEnvironmentVariable("SERVICEBUS_CONNECTION_STRING")
        ?? throw new InvalidOperationException("SERVICEBUS_CONNECTION_STRING environment variable is not set."),
    TopicName: Environment.GetEnvironmentVariable("SERVICEBUS_TOPIC_NAME")
        ?? throw new InvalidOperationException("SERVICEBUS_TOPIC_NAME environment variable is not set.")
);

builder.Services.AddSingleton(settings);
builder.Services.AddSingleton(new ServiceBusClient(settings.ConnectionString));
builder.Services.AddScoped<IPublisherService, PublisherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();

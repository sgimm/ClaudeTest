using System.Text.Json;
using Azure.Messaging.ServiceBus;
using RestApi.Models;

namespace RestApi.Services;

public class PublisherService : IPublisherService, IAsyncDisposable
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSettings _settings;

    public PublisherService(ServiceBusClient client, ServiceBusSettings settings)
    {
        _client = client;
        _settings = settings;
    }

    public async Task PublishAsync(Message message, CancellationToken cancellationToken = default)
    {
        await using var sender = _client.CreateSender(_settings.TopicName);
        var serviceBusMessage = new ServiceBusMessage(JsonSerializer.Serialize(message));
        await sender.SendMessageAsync(serviceBusMessage, cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _client.DisposeAsync();
    }
}

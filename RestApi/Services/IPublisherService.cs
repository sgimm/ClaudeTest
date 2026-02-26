using RestApi.Models;

namespace RestApi.Services;

public interface IPublisherService
{
    Task PublishAsync(Message message, CancellationToken cancellationToken = default);
}

using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using RestApi.Services;

namespace RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublisherController : ControllerBase
{
    private readonly IPublisherService _publisherService;

    public PublisherController(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    [HttpPost]
    public async Task<IActionResult> Publish([FromBody] Message message, CancellationToken cancellationToken)
    {
        await _publisherService.PublishAsync(message, cancellationToken);
        return Accepted();
    }
}

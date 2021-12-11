using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;

namespace onlysats.web.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly ILogger<NotificationController> _Logger;
    private readonly INotificationService _NotificationService;

    public NotificationController(ILogger<NotificationController> logger, INotificationService NotificationService)
    {
        _Logger = logger;
        _NotificationService = NotificationService;
    }
}
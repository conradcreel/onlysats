using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;

namespace onlysats.web.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEngagementController : ControllerBase
{
    private readonly ILogger<UserEngagementController> _Logger;
    private readonly IUserEngagementService _UserEngagementService;

    public UserEngagementController(ILogger<UserEngagementController> logger, IUserEngagementService UserEngagementService)
    {
        _Logger = logger;
        _UserEngagementService = UserEngagementService;
    }
}
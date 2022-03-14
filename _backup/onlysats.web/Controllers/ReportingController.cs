using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;

namespace onlysats.web.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportingController : ControllerBase
{
    private readonly ILogger<ReportingController> _Logger;
    private readonly IReportingService _ReportingService;

    public ReportingController(ILogger<ReportingController> logger, IReportingService ReportingService)
    {
        _Logger = logger;
        _ReportingService = ReportingService;
    }
}
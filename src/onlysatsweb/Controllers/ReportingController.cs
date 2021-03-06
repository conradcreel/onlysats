using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;
using Microsoft.Extensions.Logging;

namespace onlysats.web.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReportingController : _BaseController
    {
        private readonly ILogger<ReportingController> _Logger;
        private readonly IReportingService _ReportingService;

        public ReportingController(ILogger<ReportingController> logger, IReportingService ReportingService)
        {
            _Logger = logger;
            _ReportingService = ReportingService;
        }
    }
}
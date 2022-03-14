using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;
using Microsoft.Extensions.Logging;

namespace onlysats.web.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FinderController : ControllerBase
    {
        private readonly ILogger<FinderController> _Logger;
        private readonly IFinderService _FinderService;

        public FinderController(ILogger<FinderController> logger, IFinderService FinderService)
        {
            _Logger = logger;
            _FinderService = FinderService;
        }
    }
}
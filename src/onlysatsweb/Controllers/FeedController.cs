using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;
using Microsoft.Extensions.Logging;

namespace onlysats.web.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FeedController : _BaseController
    {
        private readonly ILogger<FeedController> _Logger;
        private readonly IFeedService _FeedService;

        public FeedController(ILogger<FeedController> logger, IFeedService FeedService)
        {
            _Logger = logger;
            _FeedService = FeedService;
        }
    }
}
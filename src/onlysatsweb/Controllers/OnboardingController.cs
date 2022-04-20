using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;
using Microsoft.Extensions.Logging;

namespace onlysats.web.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OnboardingController : _BaseController
    {
        private readonly ILogger<OnboardingController> _Logger;
        private readonly IOnboardingService _OnboardingService;

        public OnboardingController(ILogger<OnboardingController> logger, IOnboardingService OnboardingService)
        {
            _Logger = logger;
            _OnboardingService = OnboardingService;
        }
    }
}
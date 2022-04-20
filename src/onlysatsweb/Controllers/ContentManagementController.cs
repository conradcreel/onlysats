using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;
using Microsoft.Extensions.Logging;

namespace onlysats.web.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ContentManagementController : _BaseController
    {
        private readonly ILogger<ContentManagementController> _Logger;
        private readonly IContentManagementService _ContentManagementService;

        public ContentManagementController(ILogger<ContentManagementController> logger, IContentManagementService ContentManagementService)
        {
            _Logger = logger;
            _ContentManagementService = ContentManagementService;
        }
    }
}
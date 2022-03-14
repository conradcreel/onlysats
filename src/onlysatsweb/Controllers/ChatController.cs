using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;
using Microsoft.Extensions.Logging;

namespace onlysats.web.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _Logger;
        private readonly IChatService _ChatService;

        public ChatController(ILogger<ChatController> logger, IChatService ChatService)
        {
            _Logger = logger;
            _ChatService = ChatService;
        }
    }
}
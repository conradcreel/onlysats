using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using onlysats.domain.Enums;
using onlysats.domain.Services;
using onlysats.domain.Services.Request.Chat;
using onlysats.web.Controllers;
using System.Threading.Tasks;

namespace onlysatsweb.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [Authorize]
    public class ChatApiController : _BaseController
    {
        private readonly ILogger<ChatApiController> _Logger;
        private readonly IChatService _ChatService;

        public ChatApiController(ILogger<ChatApiController> logger, IChatService ChatService)
        {
            _Logger = logger;
            _ChatService = ChatService;
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages(string roomId, string from = null)
        {
            var request = new GetRoomEventsRequest
            {
                RoomId = roomId,
                From = from
            };

            if (!request.IsValid())
            {
                return BadRequest();
            }

            SetRequest(request);

            var response = await _ChatService.GetRoomEvents(request).ConfigureAwait(continueOnCapturedContext: false);

            return MapResponse(response, success: () => Ok(new
            {
                end = response.End,
                messages = response.RoomEvents
            }));
        }

        [HttpPost("messages")]
        public async Task<IActionResult> SendMessage()
        {
            await Task.Delay(10);
            var obj = new
            {
                id = 33,
                my_data = "some string"
            };

            return Ok(obj);
        }

        [HttpPost("paid_messages")]
        public async Task<IActionResult> SendPaidMessage()
        {
            var userType = GetUserType();
            if (userType != EUserRole.CREATOR)
            {
                return BadRequest("Unsupported");
            }

            var request = new QueueMessageRequest
            {
                // TODO
            };

            SetRequest(request);

            var response = await _ChatService.QueueMessage(request).ConfigureAwait(continueOnCapturedContext: false);

            return MapResponse(response, success: () => Ok(new
            {
                Id = response.QueuedMessageId,
                Bolt11 = response.BOLT11
            }));
        }
    }
}

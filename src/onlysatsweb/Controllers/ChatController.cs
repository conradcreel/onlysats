using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using onlysats.domain.Services.Request.Chat;
using onlysatsweb.Models.Chat;
using System.Linq;
using onlysats.domain.Enums;

namespace onlysats.web.Controllers
{
    [Authorize]
    public class ChatController : _BaseController
    {
        private readonly ILogger<ChatController> _Logger;
        private readonly IChatService _ChatService;

        public ChatController(ILogger<ChatController> logger, IChatService ChatService)
        {
            _Logger = logger;
            _ChatService = ChatService;
        }

        public async Task<IActionResult> List()
        {
            var request = new GetRoomListRequest();
            SetRequest(request);

            var rooms = await _ChatService.GetRoomList(request);
            if (!rooms.ResponseDetails.IsSuccess)
            {
                return View();
            }

            var allRoomRequest = new GetRoomListRequest { AdminRequest = true };
            SetRequest(allRoomRequest);

            var allRooms = await _ChatService.GetRoomList(allRoomRequest);

            if (!allRooms.ResponseDetails.IsSuccess)
            {
                return View();
            }

            var vm = new RoomListModel
            {
                Rooms = allRooms.Rooms.Where(r => rooms.JoinedRooms.Contains(r.RoomId)).ToList()
            };

            return View(vm);
        }

        public async Task<IActionResult> Detail(string roomId)
        {
            // Load latest messages
            var request = new GetRoomEventsRequest
            {
                RoomId = roomId
            };

            SetRequest(request);
            var response = await _ChatService.GetRoomEvents(request).ConfigureAwait(continueOnCapturedContext: false);

            if (!response.ResponseDetails.IsSuccess)
            {
                ModelState.AddModelError("messages_failed", "Could not retrieve messages");
                // TODO
                return View();
            }

            return View(new ChatDetailModel
            {
                End = response.End,
                Messages = response.RoomEvents,
                RoomId = roomId
            });
        }

        public async Task<IActionResult> Temp(string roomId)
        {
            return View();
        }
    }
}
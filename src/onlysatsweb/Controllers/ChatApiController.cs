using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using onlysats.domain.Enums;
using onlysats.domain.Services;
using onlysats.domain.Services.Request.Chat;
using onlysats.web.Controllers;
using onlysatsweb.Models.Chat;
using QRCoder;
using System;
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
        public async Task<IActionResult> SendMessage([FromBody] SendMessageModel model)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                RoomId = model.RoomId,
                Body = model.Message,
                FormattedBody = model.Message
            };

            SetRequest(sendMessageRequest);

            var sendMessageResponse = await _ChatService.SendMessage(sendMessageRequest).ConfigureAwait(continueOnCapturedContext: false);

            if (!sendMessageResponse.ResponseDetails.IsSuccess)
            {
                return MapResponse(sendMessageResponse);
            }

            var getRoomEventRequest = new GetRoomEventRequest
            {
                EventId = sendMessageResponse.EventId,
                RoomId = model.RoomId
            };

            SetRequest(getRoomEventRequest);

            var getRoomEventResponse = await _ChatService.GetRoomEvent(getRoomEventRequest).ConfigureAwait(continueOnCapturedContext: false);

            return MapResponse(getRoomEventResponse, success: () => Ok(getRoomEventResponse));
        }

        [HttpPost("paid_messages")]
        public async Task<IActionResult> SendPaidMessage([FromBody] SendPaidMessageModel model)
        {
            var userType = GetUserType();
            if (userType != EUserRole.CREATOR)
            {
                return BadRequest("Unsupported");
            }

            var request = new QueueMessageRequest
            {
                PaymentRequired = true,
                SenderUserId = GetUserAccountId(),
                SynapseAccessToken = GetChatAccessToken(),
                RoomId = model.RoomId,
                Description = model.Description,
                AssetPackageId = model.AssetPackageId,
                CostInSatoshis = model.CostInSatoshis
            };

            SetRequest(request);

            var response = await _ChatService.QueueMessage(request).ConfigureAwait(continueOnCapturedContext: false);

            return MapResponse(response, success: () => Ok(new
            {
                Id = response.QueuedMessageId,
                Bolt11 = response.BOLT11
            }));
        }

        private static QRCodeGenerator qrGenerator = new QRCodeGenerator();

        [HttpGet("QR")]
        public IActionResult GetQRCode(string s)
        {
            var bolt11 = s.ToUpper();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(bolt11, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            var bytes = qrCode.GetGraphic(5, new byte[] { 0, 0, 0, 255 }, new byte[] { 0xf5, 0xf5, 0xf7, 255 }, true);
            var b64 = Convert.ToBase64String(bytes);

            return Ok(new
            {
                b64 = b64
            });
        }
    }
}

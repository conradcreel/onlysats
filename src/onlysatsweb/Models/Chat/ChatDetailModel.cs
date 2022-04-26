using onlysats.domain.Services.Response.Chat;
using System.Collections.Generic;

namespace onlysatsweb.Models.Chat
{
    public class ChatDetailModel
    {
        public string End { get; set; } // Consider renaming this to From
        public string RoomId { get; set; }
        public List<RoomEvent> Messages { get; set; }
    }
}

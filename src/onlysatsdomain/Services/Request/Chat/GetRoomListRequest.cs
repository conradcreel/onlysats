namespace onlysats.domain.Services.Request.Chat
{
    public class GetRoomListRequest : SynapseRequestBase
    {
        public override string GenerateUrl()
        {
            if (!AdminRequest)
                return "_matrix/client/v3/joined_rooms";

            return "_synapse/admin/v1/rooms";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
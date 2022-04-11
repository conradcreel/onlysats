namespace onlysats.domain.Services.Request.Chat
{
    public class GetRoomListRequest : SynapseRequestBase
    {
        public override string GenerateUrl()
        {
            return "_matrix/client/v3/joined_rooms";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
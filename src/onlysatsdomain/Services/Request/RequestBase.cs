namespace onlysats.domain.Services.Request
{

    public abstract class RequestBase
    {
        public AuthenticatedUserContext UserContext { get; set; }
        public bool AdminRequest { get; set; }
        public abstract bool IsValid();
    }
}
namespace onlysats.domain.Services.Response
{
    public abstract class ResponseBase
    {
        public ResponseEnvelope ResponseDetails { get; set; }
    }
    public class ResponseEnvelope
    {
        public bool IsSuccess { get; set; }
        public string StatusCode { get; set; } = "OK";
        public string ErrorInformation { get; set; } = string.Empty;
        public ResponseEnvelope(bool isSuccess = true)
        {
            IsSuccess = isSuccess;
        }
    }
}
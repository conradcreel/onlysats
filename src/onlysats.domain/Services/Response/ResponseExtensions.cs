using onlysats.domain.Constants;

namespace onlysats.domain.Services.Response;

public static class ResponseExtensions
{
    private static T FailedResponse<T>(T response, string errorCode, string? message = null) where T : ResponseBase
    {
        response.ResponseDetails = new ResponseEnvelope
        {
            StatusCode = errorCode,
            ErrorInformation = message,
            IsSuccess = false
        };

        return response;
    }

    private static T SuccessfulResponse<T>(T response, string statusCode) where T : ResponseBase
    {
        response.ResponseDetails = new ResponseEnvelope
        {
            StatusCode = statusCode,
            IsSuccess = true
        };

        return response;
    }

    public static T BadRequest<T>(this T response, string message) where T : ResponseBase
    {
        return FailedResponse(response, CResponseStatus.BAD_REQUEST, message);
    }

    public static T ServerError<T>(this T response, string message) where T : ResponseBase
    {
        return FailedResponse(response, CResponseStatus.SERVER_ERROR, message);
    }

    public static T NotFound<T>(this T response) where T : ResponseBase
    {
        return FailedResponse(response, CResponseStatus.NOT_FOUND);
    }

    public static T Unauthorized<T>(this T response) where T : ResponseBase
    {
        return FailedResponse(response, CResponseStatus.UNAUTHORIZED);
    }

    public static T Forbidden<T>(this T response) where T : ResponseBase
    {
        return FailedResponse(response, CResponseStatus.FORBIDDEN);
    }

    public static T Accepted<T>(this T response) where T : ResponseBase
    {
        return SuccessfulResponse(response, CResponseStatus.ACCEPTED);
    }

    public static T OK<T>(this T response) where T : ResponseBase
    {
        return SuccessfulResponse(response, CResponseStatus.OK);
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Constants;
using onlysats.domain.Services.Request;
using onlysats.domain.Services.Response;

namespace onlysats.web.Controllers
{
    public class _BaseController : ControllerBase
    {
        protected IActionResult MapResponse<T>(T response) where T : ResponseBase
        {
            if (response.ResponseDetails.StatusCode == CResponseStatus.UNAUTHORIZED)
            {
                return Unauthorized();
            }
            // TODO: Rest

            return BadRequest();
        }

        protected IActionResult MapResponse(ResponseBase response, Func<IActionResult> success)
        {
            if (response.ResponseDetails.IsSuccess)
            {
                return success();
            }

            if (response.ResponseDetails.StatusCode == CResponseStatus.UNAUTHORIZED)
            {
                return Unauthorized();
            }
            // TODO: Rest

            return BadRequest();
        }

        protected void SetRequest(RequestBase request)
        {
            request.UserContext = new AuthenticatedUserContext
            {

            };
        }
    }
}
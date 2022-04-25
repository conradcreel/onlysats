using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Constants;
using onlysats.domain.Enums;
using onlysats.domain.Services.Request;
using onlysats.domain.Services.Response;

namespace onlysats.web.Controllers
{
    public class _BaseController : Controller
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
            var identity = User.Identity as ClaimsIdentity;

            if (identity == null) return;

            var claims = identity.Claims.ToList();

            var userAccountIdName = claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            int userAccountId;
            if (!int.TryParse(userAccountIdName, out userAccountId)) return;

            var roleName = claims.FirstOrDefault(c => c.Type == "Role")?.Value;
            EUserRole role = (EUserRole)Enum.Parse(typeof(EUserRole), roleName);

            request.UserContext = new AuthenticatedUserContext
            {
                UserRole = role,
                UserAccountId = userAccountId,
                ChatAccessToken = claims.FirstOrDefault(c => c.Type == "ChatAccessToken")?.Value,
                Username = claims.FirstOrDefault(c => c.Type == "Username")?.Value
            };

            if (request.AdminRequest)
            {
                request.UserContext.ChatAccessToken = claims.FirstOrDefault(c => c.Type == "AdminChatAccessToken")?.Value;
            }
        }

        protected EUserRole GetUserType()
        {
            var identity = User.Identity as ClaimsIdentity;

            if (identity == null) return EUserRole.UNKNOWN;

            var roleName = identity.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;

            return (EUserRole)Enum.Parse(typeof(EUserRole), roleName);
        }
        protected int GetUserAccountId()
        {
            var identity = User.Identity as ClaimsIdentity;

            if (identity == null) return -1;

            var userAccountIdName = identity.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            int userAccountId;
            if (!int.TryParse(userAccountIdName, out userAccountId)) return -1;

            return userAccountId;
        }
    }
}
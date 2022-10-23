using Application.Interfaces.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace WebApi.Controllers.Base
{
    public class BaseAuthController : Controller
    {
        private readonly ITokenServices _tokenServives;
        private readonly IConfiguration _config;

        public BaseAuthController(ITokenServices tokenServives, IConfiguration configuration)
        {
            _tokenServives = tokenServives;
            _config = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(a => a.Value!.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors)
                    .ToList();
                context.Result = new BadRequestObjectResult(errors);
            }
            
            var headers = context.HttpContext.Request.Headers;
            if (headers.ContainsKey("Authorization"))
            {
                bool isTokenValidated = false;
                var accessToken = headers[HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty);
                if (!string.IsNullOrEmpty(accessToken))
                {
                    isTokenValidated = _tokenServives
                    .ValidateToken(_config["JwtSettings:AccessTokenSecret"].ToString(),
                            _config["JwtSettings:Issuer"].ToString(),
                             _config["JwtSettings:Audience"].ToString(),
                             accessToken);
                }
                else
                {
                    context.Result = new UnauthorizedObjectResult("Invalid Token");
                }

                if (!isTokenValidated)
                {
                    context.Result = new UnauthorizedObjectResult("Invalid Token");
                }
            }
            base.OnActionExecuting(context);
        }


    }
}

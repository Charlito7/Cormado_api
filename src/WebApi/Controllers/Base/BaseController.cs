using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Controllers.Base
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where (a => a.Value!.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors)
                    .ToList();
                context.Result = new BadRequestObjectResult(errors);
            }
            // check if the controller has the Authorize Filter
            //if yes, Validate token.
            base.OnActionExecuting(context);
        }
    }
}

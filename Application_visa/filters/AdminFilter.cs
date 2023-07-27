using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application_visa.filters
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32("userId") != null)
            {
                if (context.HttpContext.Session.GetString("userRole") == "agent" || context.HttpContext.Session.GetString("userRole")== "caissiere")
                {
                    context.Result = new RedirectResult("/Authentification/Index");
                    return;
                }
            }
            else
            {
                context.Result = new RedirectResult("/Authentification/Index");
                return;
            }
        }
    }
}

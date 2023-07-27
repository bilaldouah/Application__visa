using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application_visa.filters
{
    public class AuthentificationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Session.GetInt32("userId") == null)
            {
                context.HttpContext.Response.Redirect("/Authentification/Index");

            }
        }
    }
}

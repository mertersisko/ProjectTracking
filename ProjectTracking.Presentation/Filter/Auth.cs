using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectTracking.Bussiness.Session;

namespace ProjectTracking.Presentation.Filter;

public class Auth : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var user = context.HttpContext.Session.FetchUser();

        if (user == null)
            context.Result = new RedirectResult("/User/Login");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Session.FetchUser() == null)
            context.Result = new RedirectResult("/User/Login");
    }
}

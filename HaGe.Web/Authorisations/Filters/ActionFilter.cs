using HaGe.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HaGe.Web.Authorisations.Filters; 

public class ActionFilter {
    public class FilterUserAuthAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {

            HaGeContext db = new HaGeContext();
            if (filterContext.HttpContext.Request.Path != null) {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated) {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        { controller = "Auth", action = "SignIn" }));
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
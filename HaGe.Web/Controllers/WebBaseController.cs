using System.Security.Claims;
using HaGe.Core.Entities;
using HaGe.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace HaGe.Web.Controllers;

[Authorize(AuthenticationSchemes = "UserScheme")]
public class WebBaseController : Controller {
    protected HaGeContext db => (HaGeContext)HttpContext.RequestServices.GetService(typeof(HaGeContext));

    protected readonly IHostingEnvironment _hostingEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public static bool IsLoggedUser;
    public static Guid LoggedUserId;
    public static Guid LoggedProfileId;
    public static User? LoggedUser;
    public static Profile? LoggedProfile;

    public WebBaseController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor) {
        _hostingEnvironment = hostingEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }

    static WebBaseController() {
        using (var db = new HaGeContext()) {
        }
    }

    public WebBaseController() {
    }

    public override void OnActionExecuting(ActionExecutingContext ctx) {
        base.OnActionExecuting(ctx);
        if (HttpContext.User.Identity != null) {
            IsLoggedUser = HttpContext.User.Identity.IsAuthenticated;
        }

        if (IsLoggedUser) {
            var sidObj = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            if (sidObj != null) {
                LoggedUserId = Guid.Parse(sidObj.Value);

                var user = db.User.FirstOrDefault(x => x.Id == LoggedUserId);
                if (user == null) return;
                LoggedUser = user;
                var profile = db.Profile.FirstOrDefault(x => x.UserId == LoggedUserId);
                if (profile == null) return;
                LoggedProfileId = profile.Id;
                LoggedProfile = profile;
            }
        }
    }

    public override void OnActionExecuted(ActionExecutedContext ctx) {
        base.OnActionExecuted(ctx);
    }
}
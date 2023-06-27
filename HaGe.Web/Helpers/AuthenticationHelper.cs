using HaGe.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace HaGe.Application.Helpers; 

public class AuthenticationHelper : WebBaseController {
    private static IHttpContextAccessor _httpContextAccessor;
    private readonly IUrlHelperFactory _urlHelperFactory;
    private readonly IUrlHelper urlHelper;

    public AuthenticationHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public static bool IsUserAuthenticated()
    {
        return IsLoggedUser;
    }
}
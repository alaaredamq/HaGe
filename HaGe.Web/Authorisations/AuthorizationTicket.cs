using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using HaGe.Core.Entities;
using HaGe.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace HaGe.Web.Authorisations;

public class AuthorisationTicket {
    public static void InitialiseTicket(string userName, HttpResponse Response, HttpContext context, HaGeContext me, out User newUser,
        bool RememberMe = true, string Scheme = "UserScheme") {
        
        PrincipleModel serializeModel = new PrincipleModel();
        var claims = new List<Claim>();

        User user = me.User.FirstOrDefault(x => x.Email == userName)!;
        user.Role = me.Role.FirstOrDefault(x => x.Id == user.RoleId.Value);

        claims.Add(new Claim(ClaimTypes.Sid, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.Name!));
        claims.Add(new Claim(ClaimTypes.Role, user.RoleId.ToString()!));
        claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName.ToString() + " " + user.LastName.ToString()));
        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        var identity = new ClaimsIdentity(claims, Scheme);

        var principal = new ClaimsPrincipal(identity);

        var props = new AuthenticationProperties();
        props.IsPersistent = RememberMe;

        newUser = user;
        Response.HttpContext.SignInAsync(Scheme, principal, props).Wait();
    }
}
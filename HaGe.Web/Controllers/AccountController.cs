using System.Security.Claims;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using HaGe.Web.Authorisations;
using HaGe.Application.Interfaces;
using HaGe.Application.Mapper;
using HaGe.Application.Models;
using HaGe.Core.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HaGe.Web.Controllers;

public class AccountController : WebBaseController {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IAccountService _accountService;
    private readonly HttpContext _httpContext;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAccountService accountService, IHttpContextAccessor httpContextAccessor) {
        _userManager = userManager;
        _signInManager = signInManager;
        _accountService = accountService;
        _httpContext = httpContextAccessor.HttpContext!;
    }
    
    public IActionResult Login() {
        return View();
    }

    [HttpGet]
    public IActionResult Register() {
        return View();
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _accountService.GetUserByEmail(model.Email);

            if (user != null) {
                return Json(new { success = false, message = "User already exists." });
            }
            
            var defaultRole = await _accountService.GetDefaultRole();
            if (defaultRole == null) {
                return Json(new { success = false, message = "Invalid default role" });
            }
            
            user = _accountService.CreateUpdateUser(model);
            if (user == null) {
                return Json(new { success = false, message = "User creation failed, try again later" });
            }
            return Json(new { success = true, message = "User created successfully" });
            // return RedirectToAction(nameof(Login), "Account");
        }

        return NotFound();
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            try {
                var user = ObjectMapper.Mapper.Map<User>(model);
                var userModel = _accountService.Login(user).Result;
                if (userModel != null) {
                    AuthorisationTicket.InitialiseTicket(user.Email, Response, HttpContext, db);

                    IsLoggedUser = true;
                    LoggedUser = user;
                    LoggedUserId = user.Id;
                    return RedirectToPage(returnUrl);
                }
            }
            catch(Exception e) {
                return View(model);
            }
        }
        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync();
        IsLoggedUser = false;
        LoggedUserId = Guid.Empty;
        LoggedUser = null;
        
        Console.WriteLine($"LoggedOut with loggedUser:{LoggedUser}, Id:{LoggedUserId}, bool:{IsLoggedUser}");
        return RedirectToAction("Index", "Home");
    }
    

}
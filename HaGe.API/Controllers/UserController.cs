using HaGe.Application.Interfaces;
using HaGe.Application.Mapper;
using HaGe.Application.Models;
using HaGe.Core.Entities;
using HaGe.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swagger.Net.Annotations;

namespace HaGe.API.Controllers; 

[ApiController]
[Route("[controller]")]
public class UserController : WebBaseController {
    private readonly IAccountService _accountService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly HttpContext _httpContext;


    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAccountService accountService, IHttpContextAccessor httpContextAccessor) {
        _userManager = userManager;
        _signInManager = signInManager;
        _accountService = accountService;
        _httpContext = httpContextAccessor.HttpContext!;
    }

    #region Get Users

    [HttpGet("GetAllUsers", Name = "GetAllUsers")]
    [SwaggerResponse(200, "All Users returned", typeof(User))]
    [SwaggerResponse(400, "All Users not returned", typeof(User))]
    [ApiExplorerSettings(GroupName = "Get Users")]
    public IActionResult GetAllUsers(int number) {
        var users = _accountService.GetUsers(number);
        if (!users.Any()) {
            return Json(new {success = false, message = "All Users not returned"});
        }
        return Json(new { success = true, Users = users, message = "All Users returned" });
    }
    
    [HttpGet("GetUserById", Name = "GetUserById")]
    [SwaggerResponse(200, "All Users returned", typeof(User))]
    [SwaggerResponse(400, "All Users not returned", typeof(User))]
    [ApiExplorerSettings(GroupName = "Get Users")]
    public IActionResult GetUserById(Guid id) {
        var user = _accountService.GetUserBytId(id);
        if (user == null) {
            return Json(new {success = false, message = "No User returned"});
        }
        return Json(new { success = true, User = user, message = "User returned" });
    }
    
    [HttpGet("GetUserByEmail", Name = "GetUserByEmail")]
    [SwaggerResponse(200, "All Users returned", typeof(User))]
    [SwaggerResponse(400, "All Users not returned", typeof(User))]
    [ApiExplorerSettings(GroupName = "Get Users")]
    public IActionResult GetUserByEmail(string? email) {
        var user = _accountService.GetUserByEmail(email);
        if (user == null) {
            return Json(new {success = false, message = "No User returned"});
        }
        return Json(new { success = true, User = user, message = "User returned" });
    }

    #endregion

    #region User Manipulation

    [HttpPut("CreateUpdateUser", Name = "CreateUpdateUser")]
    [SwaggerResponse(200, "User created or updated", typeof(User))]
    [SwaggerResponse(400, "User not created or updated", typeof(User))]
    [ApiExplorerSettings(GroupName = "Users Manipulation")]
    public IActionResult CreateUpdateUser([FromBody] RegisterViewModel model) {
        var user = _accountService.CreateUpdateUser(model);
        if (user == null) {
            return Json(new {success = false, message = "User not created or updated"});
        }
        return Json(new { success = true, User = user, message = "User created or updated" });
    }

    #endregion

    #region User Authentication

    [HttpPost("Login", Name = "Login")]
    [SwaggerResponse(200, "User logged in", typeof(LoginViewModel))]
    [SwaggerResponse(400, "User not logged in", typeof(LoginViewModel))]
    [ApiExplorerSettings(GroupName = "User Authentication")]
    public IActionResult Login([FromBody] User userLoginModel) {
        var userLoginModelResult = _accountService.Login(userLoginModel);
        if (userLoginModelResult == null) {
            return Json(new {success = false, message = "User not logged in"});
        }
        LoggedUser = ObjectMapper.Mapper.Map<User>(userLoginModel);
        LoggedUserId = LoggedUser.Id;
        IsLoggedUser = true;
        
        return Json(new { success = true, User = userLoginModelResult, message = "User logged in" });
    }
    
    [HttpPost("Logout", Name = "Logout")]
    [SwaggerResponse(200, "User logged out", typeof(User))]
    [SwaggerResponse(400, "User not logged out", typeof(User))]
    [ApiExplorerSettings(GroupName = "User Authentication")]
    public IActionResult Logout([FromBody] User user) {
        if (user == null) {
            return Json(new {success = false, message = "User not logged out"});
        }

        LoggedUser = null;
        LoggedUserId = Guid.Empty;
        IsLoggedUser = false;

        _signInManager.SignOutAsync();
        
        return Json(new { success = true, User = user, message = "User logged out" });
    }

    #endregion

    #region User Role

    [HttpGet("GetDefaultRole", Name = "GetDefaultRole")]
    [SwaggerResponse(200, "Default Role returned", typeof(Role))]
    [SwaggerResponse(400, "Default Role not returned", typeof(Role))]
    [ApiExplorerSettings(GroupName = "User Roles")]
    public IActionResult GetDefaultRole() {
        var defaultRole = _accountService.GetDefaultRole();
        if (defaultRole == null) {
            return Json(new {success = false, message = "Default Role not returned"});
        }
        return Json(new { success = true, Role = defaultRole, message = "Default Role returned" });
    }

    #endregion

    #region Delete User

    [HttpDelete("DeleteUser", Name = "DeleteUser")]
    [SwaggerResponse(200, "User deleted", typeof(User))]
    [SwaggerResponse(400, "User not deleted", typeof(User))]
    [ApiExplorerSettings(GroupName = "Delete Users")]
    public IActionResult DeleteUser([FromBody] User user) {
        if (user == null) {
            return Json(new {success = false, message = "User not deleted"});
        }
        return Json(new { success = true, User = user, message = "User deleted" });
    }
    
    [HttpDelete("DeleteUserById", Name = "DeleteUserById")]
    [SwaggerResponse(200, "User deleted", typeof(User))]
    [SwaggerResponse(400, "User not deleted", typeof(User))]
    [ApiExplorerSettings(GroupName = "Delete Users")]
    public IActionResult DeleteUserById(Guid id) {
        var user = _accountService.GetUserBytId(id);
        if (user == null) {
            return Json(new {success = false, message = "User not deleted"});
        }
        return Json(new { success = true, User = user, message = "User deleted" });
    }
    
    [HttpDelete("DeleteUserByEmail", Name = "DeleteUserByEmail")]
    [SwaggerResponse(200, "User deleted", typeof(User))]
    [SwaggerResponse(400, "User not deleted", typeof(User))]
    [ApiExplorerSettings(GroupName = "Delete Users")]
    public IActionResult DeleteUserByEmail(string? email) {
        var user = _accountService.GetUserByEmail(email);
        if (user == null) {
            return Json(new {success = false, message = "User not deleted"});
        }
        return Json(new { success = true, User = user, message = "User deleted" });
    }
    
    [HttpDelete("DeleteUsers", Name = "DeleteUsers")]
    [SwaggerResponse(200, "Users deleted", typeof(User))]
    [SwaggerResponse(400, "Users not deleted", typeof(User))]
    [ApiExplorerSettings(GroupName = "Delete Users")]
    public IActionResult DeleteUsers([FromBody] List<User> users) {
        if (!users.Any()) {
            return Json(new {success = false, message = "Users not deleted"});
        }
        return Json(new { success = true, Users = users, message = "Users deleted" });
    }

    #endregion
}
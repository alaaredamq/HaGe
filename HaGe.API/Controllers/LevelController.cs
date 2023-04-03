using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Swagger.Net.Annotations;

namespace HaGe.API.Controllers; 

public class LevelController : WebBaseController {
    private readonly ILevelRepository _levelRepository;
    private readonly ILevelProgressionRepository _levelProgressionRepository;
    
    public LevelController(ILevelRepository levelRepository, ILevelProgressionRepository levelProgressionRepository) {
        _levelProgressionRepository = levelProgressionRepository;
        _levelRepository = levelRepository;
    }
    
    [HttpGet("GetLevels", Name = "GetLevels")]
    [SwaggerResponse(200, "All levels returned", typeof(User))]
    [SwaggerResponse(400, "Levels not returned", typeof(User))]
    [ApiExplorerSettings(GroupName = "Get levels")]
    public IActionResult GetLevels(int number) {
        // var users = _levelRepository.;
        // if (!users.Any()) {
        //     return Json(new {success = false, message = "All Users not returned"});
        // }
        // return Json(new { success = true, Users = users, message = "All Users returned" });
        return Ok();
    }
    
}
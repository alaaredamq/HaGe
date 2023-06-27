using HaGe.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HaGe.Web.Controllers; 

public class LevelController : WebBaseController {
    private readonly ILogger<LevelController> _logger;
    private readonly ILevelService _levelService;
    
    public LevelController(ILogger<LevelController> logger, ILevelService levelService) {
        _logger = logger;
        _levelService = levelService;
    }

    [HttpPost]
    [Route("/UnlockNextLevel")]
    public IActionResult UnlockNextLevel(int unlockedLevel) {
        if (LoggedProfile !=  null) {
            int level = - 2;
            if (unlockedLevel == LoggedProfile.LevelUnlocked) {
                level = _levelService.UnlockNextLevel(LoggedProfileId);
            }

            if (level == -1) return Json(new {success = false, message = "Profile not found"});
            if (level == -2) return Json(new {success = false, message = "Level reached"});
            return Json(new {success = true, level = level});    
        }
        else {
            return Json(new { success = false, message = "User not logged in" });
        }
    }
}
using HaGe.Application.Interfaces;
using HaGe.Core.Entities;
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
    public IActionResult UnlockNextLevel(int LockOrder) {
        if (LoggedProfile !=  null) {
            int level = - 2;
            if (LockOrder == LoggedProfile.LevelUnlocked) {
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

    [HttpPost]
    [Route("/updateStats")]
    public IActionResult UpdateStats(List<WordOccurence> stats) {
        var words = Request.Form["stats"][1];
        // [object Object],[{"Word":"Meat","Count":0},{"Word":"Pasta","Count":0},{"Word":"Thank You","Count":1}]
        // custom parsing of the string to get the list of words and their occurences
        var wordsList = words.Split("},{");
        var list = new List<WordOccurence>();
        foreach (var item in wordsList) {
            var word = item.Split(":")[1].Replace("\"", "").Replace(",Count", "");
            var count = item.Split(":")[2].Replace("}", "").Replace("]", "");
            list.Add(new WordOccurence(word, int.Parse(count)));
        }
        
        if (LoggedProfile !=  null) {
            foreach (var item in list) {
                if (item.Count == 0)
                    continue;
                var level = _levelService.GetAllLevels().FirstOrDefault(x => x.Name.ToLower() == item.Word.ToLower());
                if (level == null) return Json(new {success = false, message = "Level not found"});
                for (int i = 0; i < item.Count; i++) {
                    var prog = new LevelProgression(LoggedProfileId, level.Id);
                    db.LevelProgression.Add(prog);
                }
            }
            db.SaveChanges();
            return Json(new {success = true});
        }
        else {
            return Json(new { success = false, message = "User not logged in" });
        }
    }
    
    public class WordOccurence{
        public string Word { get; set; }
        public int Count { get; set; }
        public WordOccurence(string word, int count) {
            this.Word = word;
            this.Count = count;
        }
    }
}
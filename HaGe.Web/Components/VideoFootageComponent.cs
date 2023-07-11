using System.Security.Claims;
using System.Threading.Tasks;
using HaGe.Application.Interfaces;
using HaGe.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HaGe.Web.Components; 

public class VideoFootageComponent : ViewComponent {
    private readonly IProfileRepository _profileRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ILevelService _levelService;

    public VideoFootageComponent(IProfileRepository profileRepository, IAccountRepository accountRepository, ILevelService levelService) {
        _profileRepository = profileRepository;
        _accountRepository = accountRepository;
        _levelService = levelService;
    }

    public async Task<IViewComponentResult> InvokeAsync(HttpContext httpContext, Guid levelId) {
        var model = new LoggedUserModel();
        var IsLoggedUser = httpContext.User.Identity.IsAuthenticated;

        if (IsLoggedUser) {
             var LoggedUserId= HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
             if (LoggedUserId != null) {
                 model.LoggedUserId = Guid.Parse(LoggedUserId);
                 model.IsLoggedUser = IsLoggedUser;

                 var profile = _profileRepository.GetByUserId(model.LoggedUserId);
                 model.LoggedProfileId = profile?.Id ?? Guid.Empty;
                 var level = _levelService.GetById(levelId);
                 if (profile != null) {
                     if (level.ParentId == Guid.Parse("1bbcdeb7-85d4-4ac9-a642-25460ab89d19") && level.Name.ToLower().Contains("quiz")) {
                         model.LevelName = "Easy";
                     }
                     else {
                         model.LevelName = level.Name;
                     }
                     model.Level = profile?.LevelUnlocked ?? -1;
                     model.LockOrder = profile?.LevelUnlocked ?? -1;

                 }
                 // if (model.Level > 0) {
                     // var level = _levelService.GetByOrder(model.Level);
                     // model.LevelPath = level.TrainingPath;
                     // model.LevelName = level.Name;
                 // }
             }
        }

        return View(model);
    }

    public class LoggedUserModel{
        public Guid LoggedUserId { get; set; }
        public bool IsLoggedUser { get; set; }
        public Guid LoggedProfileId { get; set; }
        public int Level { get; set; }
        public string LevelPath { get; set; }
        public int LockOrder { get; set; }
        public string LevelName { get; set; }
    }
}
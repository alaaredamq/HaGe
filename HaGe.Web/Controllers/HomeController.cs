using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HaGe.Web.Models;

namespace HaGe.Web.Controllers;

public class HomeController : WebBaseController {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) {
        _logger = logger;
    }

    public IActionResult Index() {
        Console.WriteLine(LoggedUserId);
        Console.WriteLine(IsLoggedUser);
        // Console.WriteLine(LoggedUser.Name);
        // Console.WriteLine(LoggedUser.Email);
        return View();
    }

    public IActionResult Privacy() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
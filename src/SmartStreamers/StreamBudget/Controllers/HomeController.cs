using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StreamBudget.Models;
using StreamBudget.Services.Abstract;

namespace StreamBudget.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IStreamAvailService _streamAvailService;

    public HomeController(ILogger<HomeController> logger, IStreamAvailService streamAvailService)
    {
        _logger = logger;
        _streamAvailService = streamAvailService;
    }
    

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SeriesSearchResults()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

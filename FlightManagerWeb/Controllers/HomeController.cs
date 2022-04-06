using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FlightManagerWeb.Models;
using FlightManagerWeb.Data;
using Microsoft.AspNetCore.Authorization;

namespace FlightManagerWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly FlightDbContext _context;
    public HomeController(ILogger<HomeController> logger,FlightDbContext context)
    {
        _logger = logger;
        _context= context;
    }

    public IActionResult Index()
    {
        return View();
    }
    [Authorize(Roles="Admin")]
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

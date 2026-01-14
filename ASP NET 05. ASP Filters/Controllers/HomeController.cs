using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP_NET_05._ASP_Filters.Models;
using ASP_NET_05._ASP_Filters.Filters;

namespace ASP_NET_05._ASP_Filters.Controllers;

//[ApiKeyQueryFilter]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [LastEnterDate]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        //throw new KeyNotFoundException();
        int numb = 0;
        int numb1 = 25 / numb;
        return View();
    }

    [TypeFilter(typeof(MyAuthorizatonFilter))]
    public IActionResult Welcome()
    {
        //throw new NullReferenceException();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

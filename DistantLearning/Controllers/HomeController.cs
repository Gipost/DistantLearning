using DistantLearning.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DistantLearning.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            ViewData["Message"] = "Welcome!";
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
}
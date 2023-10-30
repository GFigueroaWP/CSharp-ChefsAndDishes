using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsAndDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsAndDishes.Controllers;

public class ChefController : Controller
{
    private readonly ILogger<ChefController> _logger;

    private MyContext _context;

    public ChefController(ILogger<ChefController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    [Route("/")]
    public IActionResult ChefList()
    {
        List<Chef> AllChefs = _context.Chefs.Include(c => c.CreatedDishes).ToList();
        return View(AllChefs);
    }

    [HttpGet]
    [Route("/chefs/new")]
    public IActionResult New()
    {
        return View();
    }

    [HttpPost]
    [Route("/chefs/create")]
    public IActionResult Create(Chef newChef)
    {
        if (ModelState.IsValid)
        {
            _context.Chefs.Add(newChef);
            _context.SaveChanges();
            return RedirectToAction("ChefList");
        }
        else
        {
            return View("New");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

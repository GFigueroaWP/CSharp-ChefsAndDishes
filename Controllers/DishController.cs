using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsAndDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsAndDishes.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;

    private MyContext _context;

    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet]
    [Route("/dishes")]
    public IActionResult DishList()
    {
        List<Dish> AllDishes = _context.Dishes.Include(d => d.Creator).ToList();
        return View(AllDishes);
    }

    [HttpGet]
    [Route("/dishes/new")]
    public IActionResult New()
    {
        ViewBag.Chefs = _context.Chefs.ToList();
        return View();
    }

    [HttpPost]
    [Route("/dishes/create")]
    public IActionResult Create(Dish newDish)
    {
        if (ModelState.IsValid)
        {
            _context.Dishes.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("DishList");
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

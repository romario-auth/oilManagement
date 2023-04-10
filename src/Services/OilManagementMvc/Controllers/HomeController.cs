using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OilManagementMvc.Data;
using OilManagementMvc.Models;
using System.Diagnostics;

namespace OilManagementMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.CollectPoint != null ?
                        View(await _context.CollectPoint.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.CollectPoint'  is null.");
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
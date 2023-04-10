using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OilManagementMvc.Data;
using OilManagementMvc.Models;
using System.Security.Claims;

namespace OilManagementMvc.Controllers
{
    [Authorize]
    public class CollectPointsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CollectPointsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: CollectPoints
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
              return _context.CollectPoint != null ? 
                          View(await _context.CollectPoint.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CollectPoint'  is null.");
        }

        // GET: CollectPoints/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CollectPoint == null)
            {
                return NotFound();
            }

            var collectPoint = await _context.CollectPoint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collectPoint == null)
            {
                return NotFound();
            }

            return View(collectPoint);
        }

        // GET: CollectPoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CollectPoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Address,City,State,ZipCode,Number,Name,Id")] CollectPoint collectPoint)
        {
            if (ModelState.IsValid)
            {
                collectPoint.Id = Guid.NewGuid();
                collectPoint.OwnPointCollect = _httpContextAccessor.HttpContext.User.Identity.Name;
                _context.Add(collectPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collectPoint);
        }

        // GET: CollectPoints/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CollectPoint == null)
            {
                return NotFound();
            }

            var collectPoint = await _context.CollectPoint.FindAsync(id);
            if (collectPoint == null)
            {
                return NotFound();
            }
            return View(collectPoint);
        }

        // POST: CollectPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Address,City,State,ZipCode,Number,Name,Id")] CollectPoint collectPoint)
        {
            if (id != collectPoint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collectPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectPointExists(collectPoint.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(collectPoint);
        }

        // GET: CollectPoints/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CollectPoint == null)
            {
                return NotFound();
            }

            var collectPoint = await _context.CollectPoint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collectPoint == null)
            {
                return NotFound();
            }

            return View(collectPoint);
        }

        // POST: CollectPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CollectPoint == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CollectPoint'  is null.");
            }
            var collectPoint = await _context.CollectPoint.FindAsync(id);
            if (collectPoint != null)
            {
                _context.CollectPoint.Remove(collectPoint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectPointExists(Guid id)
        {
          return (_context.CollectPoint?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

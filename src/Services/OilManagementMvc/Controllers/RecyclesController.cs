using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OilManagementMvc.Data;
using OilManagementMvc.DtoReturn;
using OilManagementMvc.Models;
using dto = OilManagementMvc.DtoReturn;

namespace OilManagementMvc.Controllers
{
    public class RecyclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecyclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recycles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recycle.Include(r => r.CollectPoint);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recycles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Recycle == null)
            {
                return NotFound();
            }

            var recycle = await _context.Recycle
                .Include(r => r.CollectPoint)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recycle == null)
            {
                return NotFound();
            }

            return View(recycle);
        }

        // GET: Recycles/Create
        public IActionResult Create()
        {
            dto.RecyclesViews recyclesChangeView = RecyclesChangeView();

            ViewData["CollectPointId"] = new SelectList(recyclesChangeView.CollectPoints, "Id", "Name");
            ViewData["TypeRecycle"] = new SelectList(recyclesChangeView.TypeRecycle, "Value", "Text");
            ViewData["UnitType"] = new SelectList(recyclesChangeView.UnitTypes, "Value", "Text");
            return View();
        }

        // POST: Recycles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CollectPointId,TypeRecycle,Count,UnitType,Colletor,Id")] Recycle recycle)
        {
            if (ModelState.IsValid)
            {
                recycle.Id = Guid.NewGuid();
                _context.Add(recycle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            dto.RecyclesViews recyclesChangeView = RecyclesChangeView();

            ViewData["CollectPointId"] = new SelectList(recyclesChangeView.CollectPoints, "Id", "Name");
            ViewData["TypeRecycle"] = new SelectList(recyclesChangeView.TypeRecycle, "Value", "Text");
            ViewData["UnitType"] = new SelectList(recyclesChangeView.UnitTypes, "Value", "Text");
            return View(recycle);
        }

        // GET: Recycles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Recycle == null)
            {
                return NotFound();
            }

            var recycle = await _context.Recycle.FindAsync(id);
            if (recycle == null)
            {
                return NotFound();
            }

            dto.RecyclesViews recyclesChangeView = RecyclesChangeView();

            ViewData["CollectPointId"] = new SelectList(recyclesChangeView.CollectPoints, "Id", "Name");
            ViewData["TypeRecycle"] = new SelectList(recyclesChangeView.TypeRecycle, "Value", "Text");
            ViewData["UnitType"] = new SelectList(recyclesChangeView.UnitTypes, "Value", "Text");
            return View(recycle);
        }

        // POST: Recycles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CollectPointId,TypeRecycle,Count,UnitType,Colletor,Id")] Recycle recycle)
        {
            if (id != recycle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recycle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecycleExists(recycle.Id))
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
            ViewData["CollectPointId"] = new SelectList(_context.CollectPoint, "Id", "Id", recycle.CollectPointId);
            return View(recycle);
        }

        // GET: Recycles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Recycle == null)
            {
                return NotFound();
            }

            var recycle = await _context.Recycle
                .Include(r => r.CollectPoint)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recycle == null)
            {
                return NotFound();
            }

            return View(recycle);
        }

        // POST: Recycles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Recycle == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Recycle'  is null.");
            }
            var recycle = await _context.Recycle.FindAsync(id);
            if (recycle != null)
            {
                _context.Recycle.Remove(recycle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecycleExists(Guid id)
        {
            return (_context.Recycle?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private dto.RecyclesViews RecyclesChangeView()
        {
            dto.RecyclesViews recycles = new dto.RecyclesViews() { };

            var collectPoint = (_context.CollectPoint != null) ?
                                    _context
                                    .CollectPoint
                                    .Select(c => new dto.SelectListItemCollectPoint
                                    {
                                        Id = c.Id,
                                        Name = c.Name
                                    }
                                    )
                                    .ToList() : new List<dto.SelectListItemCollectPoint>().ToList();
            recycles.CollectPoints = collectPoint;

            var typeRecycle = new[]
            {
                new SelectListItem { Value = "0", Text = "Oil Kitchen" }
            };
            recycles.TypeRecycle = typeRecycle;

            var unitType = new[]
            {
                new SelectListItem { Value = "0", Text = "Kilogramas" },
                new SelectListItem { Value = "1", Text = "Liters"},
                new SelectListItem { Value = "2", Text = "Tons"}
            };
            recycles.UnitTypes = unitType;

            return recycles;
        }
    }
}

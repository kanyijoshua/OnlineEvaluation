using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineEvaluation.Data;
using OnlineEvaluation.Models;

namespace OnlineEvaluation.Controllers
{
    public class StateCorporationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StateCorporationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StateCorporations
        public async Task<IActionResult> Index()
        {
            return View(await _context.StateCorporation.ToListAsync());
        }

        // GET: StateCorporations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateCorporation = await _context.StateCorporation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stateCorporation == null)
            {
                return NotFound();
            }

            return View(stateCorporation);
        }

        // GET: StateCorporations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StateCorporations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] StateCorporation stateCorporation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stateCorporation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stateCorporation);
        }

        // GET: StateCorporations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateCorporation = await _context.StateCorporation.FindAsync(id);
            if (stateCorporation == null)
            {
                return NotFound();
            }
            return View(stateCorporation);
        }

        // POST: StateCorporations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StateCorporation stateCorporation)
        {
            if (id != stateCorporation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stateCorporation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateCorporationExists(stateCorporation.Id))
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
            return View(stateCorporation);
        }

        // GET: StateCorporations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateCorporation = await _context.StateCorporation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stateCorporation == null)
            {
                return NotFound();
            }

            return View(stateCorporation);
        }

        // POST: StateCorporations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stateCorporation = await _context.StateCorporation.FindAsync(id);
            _context.StateCorporation.Remove(stateCorporation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateCorporationExists(int id)
        {
            return _context.StateCorporation.Any(e => e.Id == id);
        }
    }
}

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
    public class EvaluationTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EvaluationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EvaluationType.ToListAsync());
        }

        // GET: EvaluationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationType = await _context.EvaluationType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationType == null)
            {
                return NotFound();
            }

            return View(evaluationType);
        }

        // GET: EvaluationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EvaluationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StatusOpen,OpeningDate,ClosingDate")] EvaluationType evaluationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evaluationType);
        }

        // GET: EvaluationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationType = await _context.EvaluationType.FindAsync(id);
            if (evaluationType == null)
            {
                return NotFound();
            }
            return View(evaluationType);
        }

        // POST: EvaluationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StatusOpen,OpeningDate,ClosingDate")] EvaluationType evaluationType)
        {
            if (id != evaluationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluationTypeExists(evaluationType.Id))
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
            return View(evaluationType);
        }

        // GET: EvaluationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationType = await _context.EvaluationType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationType == null)
            {
                return NotFound();
            }

            return View(evaluationType);
        }

        // POST: EvaluationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluationType = await _context.EvaluationType.FindAsync(id);
            _context.EvaluationType.Remove(evaluationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluationTypeExists(int id)
        {
            return _context.EvaluationType.Any(e => e.Id == id);
        }
    }
}

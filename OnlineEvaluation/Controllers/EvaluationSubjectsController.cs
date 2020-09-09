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
    public class EvaluationSubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationSubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EvaluationSubjects
        public async Task<IActionResult> Index()
        {
            var onlineEvaluationContext = _context.EvaluationSubject.Include(e => e.CEO).Include(e => e.Chairperson).Include(e => e.Director).Include(e => e.EvaluationType).Include(e => e.StateCorporations).Include(e => e.personBeingEvaluated);
            return View(await onlineEvaluationContext.ToListAsync());
        }

        // GET: EvaluationSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationSubject = await _context.EvaluationSubject
                .Include(e => e.CEO)
                .Include(e => e.Chairperson)
                .Include(e => e.Director)
                .Include(e => e.EvaluationType)
                .Include(e => e.StateCorporations)
                .Include(e => e.personBeingEvaluated)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationSubject == null)
            {
                return NotFound();
            }

            return View(evaluationSubject);
        }

        // GET: EvaluationSubjects/Create
        public IActionResult Create()
        {
            // ViewData["CEOId"] = new SelectList(_context.CEO, "Id", "Id");
            // ViewData["ChairpersonId"] = new SelectList(_context.Chairperson, "Id", "Id");
            // ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "Id");
            ViewData["EvaluationTypeId"] = new SelectList(_context.Set<EvaluationType>(), "Id", "Id");
            ViewData["StateCorporationId"] = new SelectList(_context.StateCorporation, "Id", "Id");
            ViewData["personBeingEvaluatedId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: EvaluationSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StateCorporationId,personBeingEvaluatedId,DateOfAppointment,EnablingLegalInstument,ChairpersonId,CEOId,DirectorId,EndofTerm,EvaluationTypeId")] EvaluationSubject evaluationSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluationSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["CEOId"] = new SelectList(_context.CEO, "Id", "Id", evaluationSubject.CEOId);
            // ViewData["ChairpersonId"] = new SelectList(_context.Chairperson, "Id", "Id", evaluationSubject.ChairpersonId);
            // ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "Id", evaluationSubject.DirectorId);
            ViewData["EvaluationTypeId"] = new SelectList(_context.Set<EvaluationType>(), "Id", "Id", evaluationSubject.EvaluationTypeId);
            ViewData["StateCorporationId"] = new SelectList(_context.StateCorporation, "Id", "Id", evaluationSubject.StateCorporationId);
            ViewData["personBeingEvaluatedId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", evaluationSubject.personBeingEvaluatedId);
            return View(evaluationSubject);
        }

        // GET: EvaluationSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationSubject = await _context.EvaluationSubject.FindAsync(id);
            if (evaluationSubject == null)
            {
                return NotFound();
            }
            // ViewData["CEOId"] = new SelectList(_context.CEO, "Id", "Id", evaluationSubject.CEOId);
            // ViewData["ChairpersonId"] = new SelectList(_context.Chairperson, "Id", "Id", evaluationSubject.ChairpersonId);
            // ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "Id", evaluationSubject.DirectorId);
            ViewData["EvaluationTypeId"] = new SelectList(_context.Set<EvaluationType>(), "Id", "Id", evaluationSubject.EvaluationTypeId);
            ViewData["StateCorporationId"] = new SelectList(_context.StateCorporation, "Id", "Id", evaluationSubject.StateCorporationId);
            ViewData["personBeingEvaluatedId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", evaluationSubject.personBeingEvaluatedId);
            return View(evaluationSubject);
        }

        // POST: EvaluationSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StateCorporationId,personBeingEvaluatedId,DateOfAppointment,EnablingLegalInstument,ChairpersonId,CEOId,DirectorId,EndofTerm,EvaluationTypeId")] EvaluationSubject evaluationSubject)
        {
            if (id != evaluationSubject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluationSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluationSubjectExists(evaluationSubject.Id))
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
            // ViewData["CEOId"] = new SelectList(_context.CEO, "Id", "Id", evaluationSubject.CEOId);
            // ViewData["ChairpersonId"] = new SelectList(_context.Chairperson, "Id", "Id", evaluationSubject.ChairpersonId);
            // ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "Id", evaluationSubject.DirectorId);
            ViewData["EvaluationTypeId"] = new SelectList(_context.Set<EvaluationType>(), "Id", "Id", evaluationSubject.EvaluationTypeId);
            ViewData["StateCorporationId"] = new SelectList(_context.StateCorporation, "Id", "Id", evaluationSubject.StateCorporationId);
            ViewData["personBeingEvaluatedId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", evaluationSubject.personBeingEvaluatedId);
            return View(evaluationSubject);
        }

        // GET: EvaluationSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationSubject = await _context.EvaluationSubject
                .Include(e => e.CEO)
                .Include(e => e.Chairperson)
                .Include(e => e.Director)
                .Include(e => e.EvaluationType)
                .Include(e => e.StateCorporations)
                .Include(e => e.personBeingEvaluated)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationSubject == null)
            {
                return NotFound();
            }

            return View(evaluationSubject);
        }

        // POST: EvaluationSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluationSubject = await _context.EvaluationSubject.FindAsync(id);
            _context.EvaluationSubject.Remove(evaluationSubject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluationSubjectExists(int id)
        {
            return _context.EvaluationSubject.Any(e => e.Id == id);
        }
    }
}

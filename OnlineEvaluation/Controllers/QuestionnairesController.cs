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
    public class QuestionnairesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionnairesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Questionnaires
        public async Task<IActionResult> Index()
        {
            var onlineEvaluationContext = _context.Questionnaire.Include(q => q.EvaluationType);
            return View(await onlineEvaluationContext.ToListAsync());
        }

        // GET: Questionnaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.Questionnaire
                .Include(q => q.EvaluationType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            return View(questionnaire);
        }

        // GET: Questionnaires/Create
        public IActionResult Create()
        {
            //ViewData[""] = new SelectList(_context.EvaluationType, "Id", "Id");
            ViewData["EvaluationTypeId"] = _context.EvaluationType.Select(a => new SelectListItem
                {
                    Text = $"{a.Name} -- {a.OpeningDate} to {a.ClosingDate}",
                    Value = a.Id.ToString()
                });
            return View();
        }

        // POST: Questionnaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EvaluationTypeId")] Questionnaire questionnaire, string[] Question)
        {
            if (ModelState.IsValid)
            {
                if (Question.Length > 0)
                {
                    foreach (var newquiz in Question)
                    {
                        if (newquiz != null)
                        {
                            var newQuiz = new Questionnaire()
                            {
                                EvaluationTypeId = questionnaire.EvaluationTypeId,
                                Question = newquiz
                            };
                            _context.Add(newQuiz);
                        }

                    }
                    await _context.SaveChangesAsync();
                    TempData["FeedbackMessage"] = $"Question(s) added successfully";
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EvaluationTypeId"] = new SelectList(_context.EvaluationType, "Id", "Id", questionnaire.EvaluationTypeId);
            return View(questionnaire);
        }

        // GET: Questionnaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.Questionnaire.FindAsync(id);
            if (questionnaire == null)
            {
                return NotFound();
            }
            ViewData["EvaluationTypeId"] = new SelectList(_context.EvaluationType, "Id", "Id", questionnaire.EvaluationTypeId);
            return View(questionnaire);
        }

        // POST: Questionnaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,EvaluationTypeId")] Questionnaire questionnaire)
        {
            if (id != questionnaire.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnaireExists(questionnaire.Id))
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
            ViewData["EvaluationTypeId"] = new SelectList(_context.EvaluationType, "Id", "Id", questionnaire.EvaluationTypeId);
            return View(questionnaire);
        }

        // GET: Questionnaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaire = await _context.Questionnaire
                .Include(q => q.EvaluationType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            return View(questionnaire);
        }

        // POST: Questionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnaire = await _context.Questionnaire.FindAsync(id);
            _context.Questionnaire.Remove(questionnaire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnaireExists(int id)
        {
            return _context.Questionnaire.Any(e => e.Id == id);
        }
    }
}

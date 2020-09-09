using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineEvaluation.Data;
using OnlineEvaluation.Models;

namespace OnlineEvaluation.Controllers
{
    [Authorize()]
    public class EvaluationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public readonly UserManager<ApplicationUser> _usermanager;

        public EvaluationController(ApplicationDbContext context, UserManager<ApplicationUser>userManager)
        {
            _context = context;
            _usermanager = userManager;
        }
        [Authorize(Roles= "CEO,Director,Chairperson")]
        public async Task<IActionResult> Index(string type, int EvaluationTypeId)
        {
            ViewBag.EvaluationTypeId = EvaluationTypeId;
            ViewBag.type = type;
            var questions = _context.Questionnaire.Where(x=>x.EvaluationTypeId == EvaluationTypeId);
            return View(await questions.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="CEO,Director,Chairperson")]
        public async Task<IActionResult> Create(Questionnaire cEO, string[] quizresponse, int CategoryId, int corporationId, string LegalInstrument, string type, string ChairmanId, DateTime? DateofAppointment, string Endofterm, string DirectorId, string CEOId)
        {
            if (quizresponse.Length>0)
            {
                IQueryable<EvaluationSubject> subject = _context.EvaluationSubject;
                var personBeingEvaluatedId = _usermanager.GetUserId(User);
                if (type == "Individual")
                {
                    subject = subject.Where(x=>x.EvaluationTypeId == CategoryId && x.StateCorporationId == corporationId && x.personBeingEvaluatedId == _usermanager.GetUserId(User));
                }
                if (type == "Chairperson")
                {
                    if (_usermanager.GetUserId(User) == ChairmanId)
                    {
                        TempData["errorMessage"] = $"You are a Chairperson, you are not allowed to evalaute yourself!!!, THIS WON'T COUNT";
                        return RedirectToAction("Index", "Home");
                    }
                    personBeingEvaluatedId = null;
                    personBeingEvaluatedId =null;
                    subject = subject.Where(x=>x.EvaluationTypeId == CategoryId && x.StateCorporationId == corporationId && x.ChairpersonId == ChairmanId);
                }
                if (type == "Ceos")
                {
                    if (_usermanager.GetUserId(User) == CEOId)
                    {
                        TempData["errorMessage"] = $"You are a C.E.O, you are not allowed to evalaute yourself!!!, THIS WON'T COUNT";
                        return RedirectToAction("Index", "Home");
                    }
                    personBeingEvaluatedId =null;
                    subject = subject.Where(x=>x.EvaluationTypeId == CategoryId && x.StateCorporationId == corporationId && x.CEOId == CEOId);
                }
                if (type == "Director")
                {
                    personBeingEvaluatedId =null;
                    subject = subject.Where(x=>x.EvaluationTypeId == CategoryId && x.StateCorporationId == corporationId && x.DirectorId == DirectorId);
                }
                int EvaluationSubjectId = 0;
                if (subject.Any())
                {
                    EvaluationSubjectId = subject.Take(1).SingleOrDefault().Id;
                }else
                {
                    EvaluationSubject newSubject = new EvaluationSubject()
                    {
                        EvaluationTypeId = CategoryId,
                        EnablingLegalInstument = LegalInstrument,
                        StateCorporationId = corporationId,
                        CEOId = CEOId,
                        ChairpersonId = ChairmanId,
                        DirectorId = DirectorId,
                        DateOfAppointment = DateofAppointment,
                        EndofTerm = Endofterm,
                        personBeingEvaluatedId = personBeingEvaluatedId
                    };
                    _context.Add(newSubject);
                    _context.SaveChanges();
                    EvaluationSubjectId = newSubject.Id;
                }
                var responseforSubject = _context.Response.Where(x=>x.EvaluationSubjectId == EvaluationSubjectId && x.RespondantId == _usermanager.GetUserId(User));
                //checked if user has taken questionaier for the subjectcandidate
                if (responseforSubject.Any())
                {
                    TempData["flashMessage"] = $"Evaluation Had Already been Taken !! Submition Not Saved , THIS WON'T COUNT";
                    return RedirectToAction("Index","Home");
                }
                foreach (var quizeAndResponse in quizresponse)
                {
                    string[] ids = quizeAndResponse.Split(new char[] { '|' });
                    int response = (int.Parse(ids[0]));
                    int quizId = int.Parse(ids[1]);
                    Response newResponse = new Response()
                    {
                        QuestionId=quizId,
                        Responses = (Respond)response,
                        RespondantId = _usermanager.GetUserId(User),
                        EvaluationSubjectId = EvaluationSubjectId
                    };
                    _context.Add(newResponse);
                }
                await _context.SaveChangesAsync();
                TempData["errorMessage"] = $"Evaluation Taken successfully";
                return RedirectToAction("Index","Home");
            }
            return View(cEO);
        }

        [Authorize(Roles="Assessor")]
        public IActionResult EvaluationTypeResult()
        {
            var EvaluationTypes = _context.EvaluationType.Include(x=>x.EvaluationSubject).Where(x=>x.StatusOpen ==true);
            return View(EvaluationTypes);
        }

        [Authorize(Roles="Assessor")]
        public IActionResult Responses(int id, string Subjectname)
        {
            ViewBag.Subjectname = Subjectname;
            var EvaluationTypes = _context.Response.Include(x=>x.EvaluationSubject).Include(x=>x.question).Where(x=>x.EvaluationSubjectId ==id);
            return View(EvaluationTypes);
        }
    }
}

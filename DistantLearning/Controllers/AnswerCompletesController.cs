using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DistantLearning.Models;

namespace DistantLearning.Controllers
{
    public class AnswerCompletesController : Controller
    {
        private readonly DBcontext _context;

        public AnswerCompletesController(DBcontext context)
        {
            _context = context;
        }

        // GET: AnswerCompletes
        public async Task<IActionResult> Index()
        {
            var dBcontext = _context.answersCompleted.Include(a => a.Question).Include(a => a.TestComplete);
            return View(await dBcontext.ToListAsync());
        }

        // GET: AnswerCompletes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerComplete = await _context.answersCompleted
                .Include(a => a.Question)
                .Include(a => a.TestComplete)
                .FirstOrDefaultAsync(m => m.AnswerCompleteId == id);
            if (answerComplete == null)
            {
                return NotFound();
            }

            return View(answerComplete);
        }

        // GET: AnswerCompletes/Create
        public IActionResult Create()
        {
            ViewData["QuestionID"] = new SelectList(_context.questions, "QuestionId", "QuestionId");
            ViewData["TestCompleteID"] = new SelectList(_context.testsCompleted, "TestCompleteId", "TestCompleteId");
            return View();
        }

        // POST: AnswerCompletes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("AnswerCompleteId,Answer,TestCompleteID,QuestionID,RightAnswer")] AnswerComplete answerComplete)
        {   
            answerComplete.AnswerCompleteId = id;
            if (ModelState.IsValid)
            {
                _context.Add(answerComplete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionID"] = new SelectList(_context.questions, "QuestionId", "QuestionId", answerComplete.QuestionID);
            ViewData["TestCompleteID"] = new SelectList(_context.testsCompleted, "TestCompleteId", "TestCompleteId", answerComplete.TestCompleteID);
            return View(answerComplete);
        }

        // GET: AnswerCompletes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerComplete = await _context.answersCompleted.FindAsync(id);
            if (answerComplete == null)
            {
                return NotFound();
            }
            return View(answerComplete);
        }

        // POST: AnswerCompletes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswerCompleteId,Answer,TestCompleteID,QuestionID,RightAnswer")] AnswerComplete answerComplete)
        {
            if (id != answerComplete.AnswerCompleteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var answer = answerComplete.Answer;
                    answerComplete = await _context.answersCompleted.FindAsync(id);
                    if (answerComplete == null)
                    {
                        return NotFound();
                    }
                    answerComplete.Answer = answer;
                    _context.Update(answerComplete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerCompleteExists(answerComplete.AnswerCompleteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(TestCompletesController.Index));
            }
            ViewData["QuestionID"] = new SelectList(_context.questions, "QuestionId", "QuestionId", answerComplete.QuestionID);
            ViewData["TestCompleteID"] = new SelectList(_context.testsCompleted, "TestCompleteId", "TestCompleteId", answerComplete.TestCompleteID);
            return View(answerComplete);
        }

        // GET: AnswerCompletes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerComplete = await _context.answersCompleted
                .Include(a => a.Question)
                .Include(a => a.TestComplete)
                .FirstOrDefaultAsync(m => m.AnswerCompleteId == id);
            if (answerComplete == null)
            {
                return NotFound();
            }

            return View(answerComplete);
        }

        // POST: AnswerCompletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answerComplete = await _context.answersCompleted.FindAsync(id);
            _context.answersCompleted.Remove(answerComplete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerCompleteExists(int id)
        {
            return _context.answersCompleted.Any(e => e.AnswerCompleteId == id);
        }
    }
}

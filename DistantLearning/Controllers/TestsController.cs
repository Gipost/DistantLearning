using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DistantLearning.Models;
using Microsoft.AspNetCore.Authorization;

namespace DistantLearning.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TestsController : Controller
    {
        private readonly DBcontext _context;

        public TestsController(DBcontext context)
        {
            _context = context;
        }

        // GET: Tests
        public async Task<IActionResult> Index()
        {
            var dBcontext = _context.tests.Include(t => t.Subject);
            return View(await dBcontext.ToListAsync());
        }

        // GET: Tests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.tests
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.TestId == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: Tests/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.subjects, "SubjectId", "SubjectName");
            return View();
        }

        // POST: Tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestId,TestName,SubjectId")] Test test)
        {
            if (ModelState.IsValid)
            {
                _context.Add(test);
                await _context.SaveChangesAsync(); 
                Question QuestionTest = new Question();
                QuestionTest.TestId = test.TestId;
                QuestionTest.QuestionName = "hiddenanswer";
                QuestionTest.QuestionAnswer = "hiddenanswer";
                _context.questions.Add(QuestionTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.subjects, "SubjectId", "SubjectId", test.SubjectId);
            return View(test);
        }




        // GET: Tests/Edit/5
        public async Task<IActionResult> Edit(int? id, [Bind("QuestionId,QuestionName,QuestionAnswer,TestId")] Question question)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.tests.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            var dBcontext = _context.questions.Where(c => c.TestId == id);
            return View(await dBcontext.ToListAsync());
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestId,TestName,SubjectId")] Test test)
        {
            if (id != test.TestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.TestId))
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
            ViewData["SubjectId"] = new SelectList(_context.subjects, "SubjectId", "SubjectId", test.SubjectId);
            return View(test);
        }

        // GET: Tests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.tests
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.TestId == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            foreach (var answercomp in _context.questions)
            {
                if (answercomp.TestId == id)
                {
                    _context.questions.Remove(answercomp);
                }
            }
            var test = await _context.tests.FindAsync(id);
            _context.tests.Remove(test);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestExists(int id)
        {
            return _context.tests.Any(e => e.TestId == id);
        }
    }
}

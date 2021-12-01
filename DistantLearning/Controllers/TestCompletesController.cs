using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DistantLearning.Models;
using DistantLearning.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
namespace DistantLearning.Controllers
{
    public class TestCompletesController : Controller
    {
        private readonly DBcontext _context;
        private readonly UserManager<User> _userManager;
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public TestCompletesController(DBcontext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TestCompletes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.UserID == user.Id);
            if (student == null)
            {
                var teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.UserID == user.Id);
                var dBcontext1 = _context.testsCompleted.Include(t => t.Subject).Include(t => t.Test);
                ViewData["Teacher"] = "1";
                return View(await dBcontext1.ToListAsync());
            }
            else
            {
                var dBcontext = _context.testsCompleted.Where(t => t.Studentid == student.ID).Include(t => t.Subject).Include(t => t.Test);
                return View(await dBcontext.ToListAsync());
            }
        }
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Mark(int? id)
        {
            var Test = await _context.testsCompleted.FindAsync(id);
            return View(Test);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Mark(int? id, User user)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testComplete = await _context.testsCompleted
                .Include(t => t.Student)
                .Include(t => t.Subject)
                .Include(t => t.Test)
                .FirstOrDefaultAsync(m => m.TestCompleteId == id);
            if (testComplete == null)
            {
                return NotFound();
            }
            if (testComplete.Mark == -1) 
            {
                testComplete.Mark = 0;
                foreach (var question in _context.answersCompleted.Where(t => t.TestCompleteID == testComplete.TestCompleteId))
                {
                    if (question.Answer.Equals(question.RightAnswer))
                    {
                        testComplete.Mark += 1;
                    }
                }
            }
             _context.testsCompleted.Update(testComplete);
            await _context.SaveChangesAsync();
            return View(testComplete);
        }

        // GET: TestCompletes/Create
        [Authorize(Roles = "Student")]
        public IActionResult Create()
        {
            ViewData["Studentid"] = new SelectList(_context.Students, "ID", "Name");
            ViewData["Testid"] = new SelectList(_context.tests, "TestId", "TestName");
            return View();
        }

        // POST: TestCompletes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestCompleteId,Studentid,Subjectid,Testid,Mark")] TestComplete testComplete)
        {
            testComplete.Mark = -1;
            var user = await GetCurrentUserAsync();
            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.UserID == user.Id);
            testComplete.Studentid = student.ID;
            var test = await _context.tests
    .FirstOrDefaultAsync(m => m.TestId == testComplete.Testid);
            testComplete.Subjectid = test.SubjectId;
            if (ModelState.IsValid)
            {
                _context.Add(testComplete);
                await _context.SaveChangesAsync();
            }
            AnswerComplete answer = new AnswerComplete();
            var questions = _context.questions.ToList();
            foreach (var question in questions)
            {
                if (question.TestId == testComplete.Testid && question.QuestionName != "hiddenanswer")
                {
                    answer.TestCompleteID = testComplete.TestCompleteId;
                    answer.QuestionID = question.QuestionId;
                    answer.RightAnswer = question.QuestionAnswer;
                    _context.answersCompleted.Add(answer);
                    answer = new AnswerComplete();
                }
                await _context.SaveChangesAsync();
            }
            ViewData["Studentid"] = new SelectList(_context.Students, "ID", "ID", testComplete.Studentid);
            ViewData["Subjectid"] = new SelectList(_context.subjects, "SubjectId", "SubjectId", testComplete.Subjectid);
            ViewData["Testid"] = new SelectList(_context.tests, "TestId", "TestId", testComplete.Testid);
            return RedirectToAction(nameof(Index));
        }

        // GET: TestCompletes/Edit/5
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testComplete = await _context.testsCompleted.FindAsync(id);
            if (testComplete == null)
            {
                return NotFound();
            }
            var dBcontext = _context.answersCompleted.Where(c => c.TestCompleteID == id).Include(c => c.Question);
            return View(await dBcontext.ToListAsync());
            
        }

        // POST: TestCompletes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestCompleteId,Studentid,Subjectid,Testid,Mark")] TestComplete testComplete)
        {
            if (id != testComplete.TestCompleteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testComplete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestCompleteExists(testComplete.TestCompleteId))
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
            ViewData["Studentid"] = new SelectList(_context.Students, "ID", "ID", testComplete.Studentid);
            ViewData["Subjectid"] = new SelectList(_context.subjects, "SubjectId", "SubjectId", testComplete.Subjectid);
            ViewData["Testid"] = new SelectList(_context.tests, "TestId", "TestId", testComplete.Testid);
            return View(testComplete);
        }

        // GET: TestCompletes/Delete/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testComplete = await _context.testsCompleted
                .Include(t => t.Student)
                .Include(t => t.Subject)
                .Include(t => t.Test)
                .FirstOrDefaultAsync(m => m.TestCompleteId == id);
            if (testComplete == null)
            {
                return NotFound();
            }

            return View(testComplete);
        }

        // POST: TestCompletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            foreach (var answercomp in _context.answersCompleted)
            {
                if (answercomp.TestCompleteID == id)
                {
                    _context.answersCompleted.Remove(answercomp);
                }
            }

            var testComplete = await _context.testsCompleted.FindAsync(id);
            _context.testsCompleted.Remove(testComplete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestCompleteExists(int id)
        {
            return _context.testsCompleted.Any(e => e.TestCompleteId == id);
        }
    }
}

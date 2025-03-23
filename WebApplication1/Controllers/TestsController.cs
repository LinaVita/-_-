using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;
using МайстерНМТ.Data;
using МайстерНМТ.Model;



namespace МайстерНМТ.Controllers
{
    public class TestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestsController(ApplicationDbContext context)
        {
            _context = context;
        }

  
        public async Task<IActionResult> Index()
        {
            var subjects = await _context.Subjects.ToListAsync();
            return View(subjects);
        }

        public async Task<IActionResult> Subject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subject == null)
            {
                return NotFound();
            }

            // Отримати всі питання за цим предметом
            var questions = await _context.Questions
                .Where(q => q.SubjectId == id)
                .Include(q => q.QuestionType)
                .ToListAsync();

            ViewBag.Subject = subject;
            return View(questions);
        }

        // GET: /Tests/Start/5
        public async Task<IActionResult> Start(int? subjectId, int questionCount = 10)
        {
            if (subjectId == null)
            {
                return NotFound();
            }

            // Отримати випадкові питання за предметом
            var questions = await _context.Questions
                .Where(q => q.SubjectId == subjectId)
                .Include(q => q.Answers)
                .OrderBy(q => Guid.NewGuid()) // Випадковий порядок
                .Take(questionCount)
                .ToListAsync();

            if (questions.Count == 0)
            {
                return NotFound("Немає питань для цього предмету");
            }

            ViewBag.SubjectId = subjectId;
            return View(questions);
        }

        // POST: /Tests/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(int subjectId, Dictionary<int, int> answers)
        {
            if (answers == null || answers.Count == 0)
            {
                return RedirectToAction(nameof(Start), new { subjectId });
            }

            // Отримати всі питання та правильні відповіді
            var questionIds = answers.Keys.ToList();
            var questions = await _context.Questions
                .Where(q => questionIds.Contains(q.Id))
                .Include(q => q.Answers)
                .ToListAsync();

            // Підрахувати правильні відповіді
            int correctCount = 0;
            var results = new Dictionary<int, bool>();

            foreach (var question in questions)
            {
                if (answers.ContainsKey(question.Id))
                {
                    int answerId = answers[question.Id];
                    bool isCorrect = question.Answers.Any(a => a.Id == answerId && a.IsCorrect);
                    results[question.Id] = isCorrect;

                    if (isCorrect)
                    {
                        correctCount++;
                    }
                }
            }

            // Зберегти результат тесту
            var testResult = new TestResult
            {
               // UserId = 1, // В майбутньому тут буде ID поточного користувача
                //SubjectId = subjectId,
               // CorrectAnswers = correctCount,
                //TotalQuestions = questions.Count,
                //Percentage = (int)((double)correctCount / questions.Count * 100),
                //CompletedDate = DateTime.Now
            };

            _context.TestResults.Add(testResult);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Results), new { id = testResult.Id });
        }

        // GET: /Tests/Results/5
        public async Task<IActionResult> Results(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testResult = await _context.TestResults
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (testResult == null)
            {
                return NotFound();
            }

            return View(testResult);
        }
    }
}



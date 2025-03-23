using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Data;
using WebApplication1.Model;
using МайстерНМТ.Model;
namespace МайстерНМТ.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, QuestionType singleChoice)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // Перевіряємо, чи є дані в базі
            if (context.Subjects.Any())
            {
                return; // База даних вже заповнена
            }

            var multipleChoice = new QuestionType { Name = "Множинний вибір", Description = "Вибір кількох правильних відповідей" };
            var openAnswer = new QuestionType { Name = "Відкрита відповідь", Description = "Введення відповіді вручну" };

            var singleChoiceQuestion = new QuestionType { Name = "Одиночний вибір", Description = "Вибір однієї правильної відповіді" };

            context.SaveChanges();

            // Додаємо предмети
            var mathSubject = new Subject { Name = "Математика", Description = "Підготовка до НМТ з математики", IconClass = "fas fa-calculator" };
            var ukrSubject = new Subject { Name = "Українська мова", Description = "Підготовка до НМТ з української мови", IconClass = "fas fa-book" };
            var historySubject = new Subject { Name = "Історія України", Description = "Підготовка до НМТ з історії України", IconClass = "fas fa-landmark" };

            context.Subjects.AddRange(mathSubject, ukrSubject, historySubject);
            context.SaveChanges();

            // Додаємо теми для математики
            var algebraTopic = new Topic
            {
                Name = "Алгебра",
                Description = "Алгебра та початки аналізу",
                SubjectId = mathSubject.Id,
                OrderIndex = 1
            };

            var geometryTopic = new Topic
            {
                Name = "Геометрія",
                Description = "Планіметрія та стереометрія",
                SubjectId = mathSubject.Id,
                OrderIndex = 2
            };

            context.Topics.AddRange(algebraTopic, geometryTopic);
            context.SaveChanges();

            // Додаємо підтеми для алгебри
            var numbersTopic = new Topic
            {
                Name = "Числа і вирази",
                Description = "Дійсні числа, тотожні перетворення виразів",
                SubjectId = mathSubject.Id,
                ParentTopicId = algebraTopic.Id,
                OrderIndex = 1
            };

            var equationsTopic = new Topic
            {
                Name = "Рівняння та нерівності",
                Description = "Лінійні, квадратні, раціональні, ірраціональні та інші типи рівнянь і нерівностей",
                SubjectId = mathSubject.Id,
                ParentTopicId = algebraTopic.Id,
                OrderIndex = 2
            };

            var functionsTopic = new Topic
            {
                Name = "Функції",
                Description = "Властивості функцій, графіки, похідна та інтеграл",
                SubjectId = mathSubject.Id,
                ParentTopicId = algebraTopic.Id,
                OrderIndex = 3
            };

            context.Topics.AddRange(numbersTopic, equationsTopic, functionsTopic);
            context.SaveChanges();

            // Додаємо питання з алгебри (приклади)
            var question1 = new Question
            {
                Text = "Обчисліть вираз: 2^3 + 4^2 - 3*5",
                SubjectId = mathSubject.Id,
                QuestionTypeId = new QuestionType { Name = "Одиночний вибір", Description = "Вибір однієї правильної відповіді" }.Id
            };

            context.Questions.Add(question1);
            context.SaveChanges();

            // Додаємо відповіді на питання
            var answers1 = new[]
            {
                new Answer { QuestionId = question1.Id, Text = "9", IsCorrect = true },
                new Answer { QuestionId = question1.Id, Text = "11", IsCorrect = false },
                new Answer { QuestionId = question1.Id, Text = "7", IsCorrect = false },
                new Answer { QuestionId = question1.Id, Text = "13", IsCorrect = false }
            };

            context.Answers.AddRange(answers1);

            // Додаємо питання з геометрії (приклади)
            var question2 = new Question
            {
                Text = "Яка площа круга з радіусом 5 см?",
                SubjectId = mathSubject.Id,
                QuestionTypeId = new QuestionType { Name = "Одиночний вибір", Description = "Вибір однієї правильної відповіді" }.Id
            };

            context.Questions.Add(question2);
            context.SaveChanges();

            // Додаємо відповіді на питання
            var answers2 = new[]
            {
                new Answer { QuestionId = question2.Id, Text = "25π см²", IsCorrect = true },
                new Answer { QuestionId = question2.Id, Text = "10π см²", IsCorrect = false },
                new Answer { QuestionId = question2.Id, Text = "5π см²", IsCorrect = false },
                new Answer { QuestionId = question2.Id, Text = "20π см²", IsCorrect = false }
            };

            context.Answers.AddRange(answers2);
            context.SaveChanges();

            // Додаємо урок з математики
            var lesson1 = new Lesson
            {
                Title = "Квадратні рівняння",
                Content = @"<h2>Квадратні рівняння</h2>
                <p>Квадратне рівняння має вигляд ax² + bx + c = 0, де a, b, c – задані числа, a ≠ 0.</p>
                <p>Дискримінант квадратного рівняння обчислюється за формулою D = b² - 4ac.</p>
                <p>Корені квадратного рівняння знаходять за формулою:</p>
                <p>x₁ = (-b + √D) / (2a)<br>x₂ = (-b - √D) / (2a)</p>
                <p>Якщо D > 0, то рівняння має два різних дійсних корені.<br>
                Якщо D = 0, то рівняння має один корінь (два однакових): x = -b / (2a).<br>
                Якщо D < 0, то рівняння не має дійсних коренів.</p>",
                SubjectId = mathSubject.Id,
                TopicId = equationsTopic.Id
            };

            context.Lessons.Add(lesson1);
            context.SaveChanges();

            // Додаємо користувача для тестування
            var testUser = new User
            {
                FirstName = "Тест",
                LastName = "Користувач",
                Email = "test@example.com",
                PasswordHash = "5f4dcc3b5aa765d61d8327deb882cf99", // "password" в MD5 (в реальному проекті треба використовувати безпечніший метод)
                RegisteredDate = DateTime.Now,
                LastLoginDate = DateTime.Now,
                IsActive = true,
                EducationLevelId = 1
            };

            context.Users.Add(testUser);
            context.SaveChanges();
        }
    }
}

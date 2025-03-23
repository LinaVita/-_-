using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Model;
using МайстерНМТ.Model;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

            public DbSet<Answer> Answers { get; set; }
            public DbSet<Question> Questions { get; set; }
            public DbSet<Type> QuestionTypes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
            public DbSet<Topic> Topics { get; set; }
            public DbSet<Lesson> Lessons { get; set; }
            public DbSet<TestResult> TestResults { get; set; }
            public DbSet<User> Users { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Налаштування відносин між таблицями
                modelBuilder.Entity<Answer>()
                    .HasOne(a => a.Question)
                    .WithMany(q => q.Answers)
                    .HasForeignKey(a => a.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Question>()
                    .HasOne(q => q.Subject)
                    .WithMany()
                    .HasForeignKey(q => q.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                
                   modelBuilder.Entity<Question>()
                     .Property(q => q.QuestionType)
                      .HasConversion<int>();

            modelBuilder.Entity<Lesson>()
                    .HasOne(l => l.Subject)
                    .WithMany()
                    .HasForeignKey(l => l.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Lesson>()
                    .HasOne(l => l.Topic)
                    .WithMany()
                    .HasForeignKey(l => l.TopicId)
                    .OnDelete(DeleteBehavior.SetNull);
            }
        }
    }
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using МайстерНМТ.Model;

namespace WebApplication1.Model
{
    public class TestResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        public int? TopicId { get; set; }

        public int CorrectAnswers { get; set; }

        public int TotalQuestions { get; set; }

        public int Percentage { get; set; }

        public DateTime CompletedDate { get; set; }

        [StringLength(2000)]
        public string DetailedResults { get; set; } // JSON зі списком відповідей та правильності

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("TopicId")]
        public virtual Topic Topic { get; set; }
    }
}


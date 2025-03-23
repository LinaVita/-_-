using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    public class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }
        public int Id { get; set; }
        public int SubjectId { get; set; }

        [Required]
        public string Text { get; set; }
        public Question Type { get; set; }
        public int Difficulty { get; set; }
        public int Score { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("QuestionTypeId")]
        public virtual QuestionType QuestionType { get; set; }
    public virtual ICollection<Answer> Answers
        {
            get; set;
        }
        public int QuestionTypeId { get; internal set; }
    }
}
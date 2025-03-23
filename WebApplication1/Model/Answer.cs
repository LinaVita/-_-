using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}

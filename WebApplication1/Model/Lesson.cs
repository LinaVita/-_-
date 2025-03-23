using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Model;

namespace МайстерНМТ.Model
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int SubjectId { get; set; }

        public int? TopicId { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("TopicId")]
        public virtual Topic Topic { get; set; }
    }
}

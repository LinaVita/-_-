using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Model;

namespace МайстерНМТ.Model
{
    public class Topic
    {
        public Topic()
        {
            Questions = new HashSet<Question>();
            Lessons = new HashSet<Lesson>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int SubjectId { get; set; }

        public int? ParentTopicId { get; set; }

        public int OrderIndex { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("ParentTopicId")]
        public virtual Topic ParentTopic { get; set; }

        public virtual ICollection<Topic> SubTopics { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
   
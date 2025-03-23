using System.ComponentModel.DataAnnotations;
using МайстерНМТ.Model;

namespace WebApplication1.Model
{
    public class Subject
    {
        public Subject()
        {
            Topics = new HashSet<Topic>();
            Questions = new HashSet<Question>();
            Lessons = new HashSet<Lesson>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(100)]
        public string IconClass { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class QuestionType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

       
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class User
    {
        public User()
        {
            TestResults = new HashSet<TestResult>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Phone]
        [StringLength(20)]
        public string Phone { get; set; }

        public DateTime RegisteredDate { get; set; }

        public DateTime LastLoginDate { get; set; }

        public bool IsActive { get; set; }

        [StringLength(100)]
        public string ActivationCode { get; set; }

        public int EducationLevelId { get; set; } // Рівень освіти (11 клас, студент тощо)

        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}



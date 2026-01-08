using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Universities")]
    public class University
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = null!; // DU, CU, RU, BUET, KUET, etc.

        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public string? BannerUrl { get; set; }
        public string? Website { get; set; }
        public string? Location { get; set; }
        public string? EstablishedYear { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }

        // Statistics
        public int TotalCourses { get; set; } = 0;
        public int TotalStudents { get; set; } = 0;
        public int TotalTeachers { get; set; } = 0;
        public int TotalEnrollments { get; set; } = 0;

        // University Ranking
        public int UniversityRank { get; set; } = 0;
        public decimal AverageCourseRating { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public bool IsVerified { get; set; } = false;

        // Navigation Properties
        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();
    }
}
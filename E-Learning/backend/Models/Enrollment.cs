using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Enrollments")]
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [MaxLength(20)]
        public string EnrollmentType { get; set; } = "Student"; // Student, Teacher, Auditor

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public DateTime? LastAccessed { get; set; }
        public DateTime? ExpiryDate { get; set; } // For time-limited access

        // Progress Tracking (Module-based)
        public int CompletedModules { get; set; } = 0;
        public int TotalModules { get; set; } = 0;
        public int CompletedLessons { get; set; } = 0;
        public int TotalLessons { get; set; } = 0;
        public decimal ProgressPercentage { get; set; } = 0;

        // Grades & Scores (Exam-oriented)
        public decimal? QuizAverage { get; set; }
        public decimal? AssignmentAverage { get; set; }
        public decimal? FinalGrade { get; set; }
        public string? GradeLetter { get; set; } // A, B, C, etc.

        // Ranking Points
        public int PointsEarned { get; set; } = 0;
        public int QuizPoints { get; set; } = 0;
        public int AssignmentPoints { get; set; } = 0;
        public int CompletionPoints { get; set; } = 0;

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Completed, Dropped, Suspended

        // Payment Info
        public decimal? AmountPaid { get; set; }
        public int? PaymentId { get; set; }
        public string? PaymentStatus { get; set; }

        // Certificate
        public bool CertificateEarned { get; set; } = false;
        public string? CertificateUrl { get; set; }

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; } = null!;

        [ForeignKey("PaymentId")]
        public virtual Payment? Payment { get; set; }

        public virtual ICollection<QuizSubmission> QuizSubmissions { get; set; } = new List<QuizSubmission>();
        public virtual ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; } = new List<AssignmentSubmission>();
        public virtual ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();
    }
}
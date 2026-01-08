using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Comment { get; set; }

        // Detailed ratings (Exam-focused)
        public int? ContentRating { get; set; } // 1-5
        public int? TeachingRating { get; set; } // 1-5
        public int? ExamPreparationRating { get; set; } // 1-5
        public int? MaterialQualityRating { get; set; } // 1-5

        // Helpfulness
        public int HelpfulCount { get; set; } = 0;
        public int UnhelpfulCount { get; set; } = 0;

        // Teacher Response
        public bool HasTeacherResponse { get; set; } = false;
        [Column(TypeName = "text")]
        public string? TeacherResponse { get; set; }
        public DateTime? TeacherRespondedAt { get; set; }

        // Verification
        public bool IsVerifiedPurchase { get; set; } = false;
        public int? EnrollmentId { get; set; }

        // Moderation
        public bool IsReported { get; set; } = false;
        public bool IsApproved { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        [ForeignKey("EnrollmentId")]
        public virtual Enrollment? Enrollment { get; set; }
    }
}
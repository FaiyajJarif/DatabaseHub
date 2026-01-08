using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("AssignmentSubmissions")]
    public class AssignmentSubmission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int AssignmentId { get; set; }

        // Submission Content
        [Column(TypeName = "text")]
        public string? AnswerText { get; set; }

        public string? AttachmentUrl { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public long? FileSize { get; set; }

        // Grading
        public decimal? Score { get; set; }
        public decimal? MaxScore { get; set; }
        public string? Grade { get; set; }
        public bool IsGraded { get; set; } = false;
        public int? GradedBy { get; set; } // Teacher ID

        // Teacher Feedback
        [Column(TypeName = "text")]
        public string? Feedback { get; set; }

        public string? RubricScores { get; set; } // JSON

        // Timing
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public bool IsLate { get; set; } = false;
        public decimal? LatePenalty { get; set; }

        // Status
        [MaxLength(20)]
        public string Status { get; set; } = "Submitted"; // Submitted, Graded, Returned

        // Resubmission
        public bool IsResubmission { get; set; } = false;
        public int? OriginalSubmissionId { get; set; }

        // Ranking Points
        public int PointsEarned { get; set; } = 0;

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("AssignmentId")]
        public virtual Assignment Assignment { get; set; }

        [ForeignKey("GradedBy")]
        public virtual User? Grader { get; set; }
    }
}
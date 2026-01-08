using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("QuizSubmissions")]
    public class QuizSubmission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int QuizId { get; set; }

        public int AttemptNumber { get; set; } = 1;

        // Results
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public decimal Percentage { get; set; }

        // Timing
        public DateTime StartedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public int? TimeTaken { get; set; } // in seconds

        // Status
        public bool IsPassed { get; set; } = false;
        public bool IsGraded { get; set; } = true; // Auto-graded for MCQs

        // Answers stored as JSON
        [Column(TypeName = "jsonb")]
        public string? UserAnswers { get; set; }

        // For manual grading
        public decimal? ManualGrade { get; set; }
        public string? TeacherFeedback { get; set; }

        // Ranking Points
        public int PointsEarned { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; } = null!;
    }
}
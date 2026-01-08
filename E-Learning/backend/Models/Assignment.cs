using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Assignments")]
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        [Column(TypeName = "text")]
        public string Description { get; set; } = null!;

        [Required]
        public int CourseId { get; set; }

        public int? ModuleId { get; set; }
        public int? LessonId { get; set; }

        // Exam-oriented Assignment
        public int MaxScore { get; set; } = 100;
        public int PassingScore { get; set; } = 50;
        public DateTime DueDate { get; set; }

        // Submission Settings
        public bool AllowLateSubmission { get; set; } = false;
        public int? LateSubmissionDays { get; set; }
        public decimal? LatePenaltyPercentage { get; set; } = 10;

        public string? AttachmentUrl { get; set; }
        public string? SampleSolutionUrl { get; set; }
        
        [MaxLength(50)]
        public string SubmissionFormat { get; set; } = "Any"; // PDF, DOC, ZIP, Image, Text, Code

        // Instructions
        [Column(TypeName = "text")]
        public string? Instructions { get; set; }

        public string? Rubric { get; set; } // JSON for grading criteria

        // Points for Ranking
        public int AssignmentPoints { get; set; } = 20;

        public bool IsPublished { get; set; } = false;
        public bool IsMandatory { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; } = null!;

        [ForeignKey("ModuleId")]
        public virtual Module? Module { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson? Lesson { get; set; }

        public virtual ICollection<AssignmentSubmission> Submissions { get; set; } = new List<AssignmentSubmission>();
    }
}
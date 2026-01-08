using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Modules")]
    public class Module
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [Column(TypeName = "text")]
        public string? Description { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int Order { get; set; } // Module sequence

        public int EstimatedDuration { get; set; } // in minutes
        public string? LearningObjectives { get; set; } // JSON array

        // Module can contain different content types
        public bool HasVideo { get; set; } = false;
        public bool HasPDF { get; set; } = false;
        public bool HasSlides { get; set; } = false;
        public bool HasQuiz { get; set; } = false;
        public bool HasAssignment { get; set; } = false;

        // Completion Tracking
        public bool IsRequired { get; set; } = true;
        public int CompletionPoints { get; set; } = 10;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; } = null!;

        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
        public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
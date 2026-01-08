using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Quizzes")]
    public class Quiz
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

        // Quiz can be at Course, Module, or Lesson level
        public int? ModuleId { get; set; }
        public int? LessonId { get; set; }

        // Exam-oriented settings
        public int TimeLimit { get; set; } = 0; // in minutes, 0 = no limit
        public int TotalQuestions { get; set; } = 0;
        public int PassingScore { get; set; } = 70; // percentage
        public int MaxScore { get; set; } = 100;

        // Question Settings
        public bool ShuffleQuestions { get; set; } = true;
        public bool ShuffleOptions { get; set; } = true;
        public bool ShowCorrectAnswers { get; set; } = false;
        public bool ShowResultsImmediately { get; set; } = false;

        // Attempt Settings
        public bool AllowRetake { get; set; } = true;
        public int MaxAttempts { get; set; } = 3;
        public bool RequirePassword { get; set; } = false;
        public string? QuizPassword { get; set; }

        // Scheduling
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableUntil { get; set; }

        // Status
        public bool IsPublished { get; set; } = false;
        public bool IsMandatory { get; set; } = true;

        // Points for Ranking
        public int QuizPoints { get; set; } = 10;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }

        // Navigation Properties
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; } = null!;

        [ForeignKey("ModuleId")]
        public virtual Module? Module { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson? Lesson { get; set; }

        public virtual ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
        public virtual ICollection<QuizSubmission> Submissions { get; set; } = new List<QuizSubmission>();
    }
}
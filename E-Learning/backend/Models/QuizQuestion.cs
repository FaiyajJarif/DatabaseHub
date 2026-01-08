using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("QuizQuestions")]
    public class QuizQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string QuestionText { get; set; }

        [MaxLength(20)]
        public string QuestionType { get; set; } = "MultipleChoice"; // MultipleChoice, TrueFalse, ShortAnswer, Essay

        // Options for MultipleChoice
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? OptionE { get; set; }

        [Required]
        public string CorrectAnswer { get; set; } // For MCQ: "A", "B", etc.

        // For multiple correct answers
        public string? MultipleCorrectAnswers { get; set; } // JSON array

        public int Points { get; set; } = 1;
        public int DifficultyLevel { get; set; } = 1; // 1-5

        // Explanation for answer (Exam-focused learning)
        [Column(TypeName = "text")]
        public string? Explanation { get; set; }

        public string? Tags { get; set; } // JSON array

        public int Order { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }
    }
}
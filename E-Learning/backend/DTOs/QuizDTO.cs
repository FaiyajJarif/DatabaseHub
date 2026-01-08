// DTOs/QuizDTOs.cs
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    // Quiz Create
    public class QuizCreateDTO
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public int CourseId { get; set; }

        public int? ModuleId { get; set; }
        public int? LessonId { get; set; }

        public int TimeLimit { get; set; } = 0; // 0 = no limit
        public int PassingScore { get; set; } = 70;
        public int MaxScore { get; set; } = 100;
        public bool ShuffleQuestions { get; set; } = true;
        public bool ShuffleOptions { get; set; } = true;
        public bool ShowCorrectAnswers { get; set; } = false;
        public bool ShowResultsImmediately { get; set; } = false;
        public bool AllowRetake { get; set; } = true;
        public int MaxAttempts { get; set; } = 3;
        public bool RequirePassword { get; set; } = false;
        public string? QuizPassword { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableUntil { get; set; }
        public bool IsMandatory { get; set; } = true;
        public int QuizPoints { get; set; } = 10;
        public List<QuizQuestionCreateDTO> Questions { get; set; }
    }

    // Quiz Update
    public class QuizUpdateDTO
    {
        [MaxLength(200)]
        public string? Title { get; set; }

        public string? Description { get; set; }
        public int? TimeLimit { get; set; }
        public int? PassingScore { get; set; }
        public int? MaxScore { get; set; }
        public bool? ShuffleQuestions { get; set; }
        public bool? ShuffleOptions { get; set; }
        public bool? ShowCorrectAnswers { get; set; }
        public bool? ShowResultsImmediately { get; set; }
        public bool? AllowRetake { get; set; }
        public int? MaxAttempts { get; set; }
        public bool? RequirePassword { get; set; }
        public string? QuizPassword { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableUntil { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsMandatory { get; set; }
        public int? QuizPoints { get; set; }
    }

    // Quiz Response
    public class QuizDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int? ModuleId { get; set; }
        public string? ModuleTitle { get; set; }
        public int? LessonId { get; set; }
        public string? LessonTitle { get; set; }
        public int TimeLimit { get; set; }
        public int PassingScore { get; set; }
        public int MaxScore { get; set; }
        public int TotalQuestions { get; set; }
        public bool ShuffleQuestions { get; set; }
        public bool ShuffleOptions { get; set; }
        public bool ShowCorrectAnswers { get; set; }
        public bool ShowResultsImmediately { get; set; }
        public bool AllowRetake { get; set; }
        public int MaxAttempts { get; set; }
        public bool RequirePassword { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableUntil { get; set; }
        public bool IsPublished { get; set; }
        public bool IsMandatory { get; set; }
        public int QuizPoints { get; set; }
        public int AttemptCount { get; set; }
        public decimal? BestScore { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public List<QuizQuestionDTO> Questions { get; set; }
    }

    // Quiz Question Create
    public class QuizQuestionCreateDTO
    {
        [Required]
        public string QuestionText { get; set; }

        [Required]
        [MaxLength(20)]
        public string QuestionType { get; set; } = "MultipleChoice";

        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? OptionE { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }

        public List<string>? MultipleCorrectAnswers { get; set; }
        public int Points { get; set; } = 1;
        public int DifficultyLevel { get; set; } = 1;
        public string? Explanation { get; set; }
        public List<string>? Tags { get; set; }
        public int Order { get; set; } = 1;
    }

    // Quiz Question Response
    public class QuizQuestionDTO
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? OptionE { get; set; }
        public string? CorrectAnswer { get; set; }
        public List<string>? MultipleCorrectAnswers { get; set; }
        public int Points { get; set; }
        public int DifficultyLevel { get; set; }
        public string? Explanation { get; set; }
        public List<string>? Tags { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // Quiz Submission
    public class SubmitQuizDTO
    {
        [Required]
        public Dictionary<int, string> Answers { get; set; } // QuestionId -> Answer

        public int? TimeTaken { get; set; } // in seconds
        public DateTime StartedAt { get; set; }
    }

    // Quiz Result
    public class QuizResultDTO
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int AttemptNumber { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public decimal Percentage { get; set; }
        public bool IsPassed { get; set; }
        public bool IsGraded { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public int? TimeTaken { get; set; }
        public decimal? ManualGrade { get; set; }
        public string? TeacherFeedback { get; set; }
        public int PointsEarned { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<QuestionResultDTO> QuestionResults { get; set; }
    }

    // Alias for service signatures
    public class QuizSubmissionDTO : SubmitQuizDTO { }

    // Question Result
    public class QuestionResultDTO
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string? UserAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public int PointsEarned { get; set; }
        public int MaxPoints { get; set; }
        public string? Explanation { get; set; }
    }

    // Quiz Statistics
    public class QuizStatsDTO
    {
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int TotalAttempts { get; set; }
        public int TotalParticipants { get; set; }
        public decimal AverageScore { get; set; }
        public decimal PassRate { get; set; }
        public decimal AverageTimeTaken { get; set; }
        public Dictionary<int, int> ScoreDistribution { get; set; }
        public List<QuestionStatsDTO> QuestionStats { get; set; }
        public List<TopPerformerDTO> TopPerformers { get; set; }
    }

    public class QuestionStatsDTO
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int TotalAttempts { get; set; }
        public int CorrectAnswers { get; set; }
        public decimal CorrectRate { get; set; }
        public Dictionary<string, int> AnswerDistribution { get; set; }
    }

    public class TopPerformerDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? ProfileImage { get; set; }
        public int Score { get; set; }
        public decimal Percentage { get; set; }
        public int TimeTaken { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
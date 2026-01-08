using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    // Learning Content DTO
    public class ModuleContentDTO
    {
        public int ModuleId { get; set; }
        public string ModuleTitle { get; set; } = string.Empty;
        public string ModuleDescription { get; set; } = string.Empty;
        public List<LessonDTO> Lessons { get; set; } = new();
        public int TotalLessons { get; set; }
        public int CompletedLessons { get; set; }
        public float ProgressPercentage { get; set; }
    }

    // Lesson DTOs
    public class CreateLessonDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }
        public int DurationMinutes { get; set; }
        public int? ModuleId { get; set; }
        public int? OrderIndex { get; set; }
    }

    public class UpdateLessonDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }
        public int? DurationMinutes { get; set; }
        public int? OrderIndex { get; set; }
    }

    // Assignment DTOs
    public class CreateAssignmentDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public int MaxScore { get; set; }
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
    }

    public class UpdateAssignmentDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int? MaxScore { get; set; }
    }

    // Quiz DTOs
    public class CreateQuizDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int PassingScore { get; set; }
        public int? TimeLimit { get; set; }
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
    }

    public class UpdateQuizDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? PassingScore { get; set; }
        public int? TimeLimit { get; set; }
    }

    public class QuizAttemptDTO
    {
        public int AttemptId { get; set; }
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public int TimeSpentSeconds { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    // Note DTOs
    public class CreateNoteDTO
    {
        public string Content { get; set; } = string.Empty;
        public int? HighlightStart { get; set; }
        public int? HighlightEnd { get; set; }
        public string? Color { get; set; }
    }

    // Analytics DTOs
    public class DailyAnalyticsDTO
    {
        public DateTime Date { get; set; }
        public int LessonsCompleted { get; set; }
        public int QuizzesAttempted { get; set; }
        public int AssignmentsSubmitted { get; set; }
        public int TimeSpentMinutes { get; set; }
        public float DailyScore { get; set; }
    }

    public class WeeklyAnalyticsDTO
    {
        public DateTime WeekStartDate { get; set; }
        public int LessonsCompleted { get; set; }
        public int QuizzesCompleted { get; set; }
        public int AssignmentsCompleted { get; set; }
        public int TimeSpentHours { get; set; }
        public float WeeklyAverage { get; set; }
        public List<DailyAnalyticsDTO> DailyBreakdown { get; set; } = new();
    }

    // Payment DTOs
    public class ApplyCouponResultDTO
    {
        public bool IsValid { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public string? Message { get; set; }
    }

    public class CouponValidationDTO
    {
        public bool IsValid { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal MaxDiscount { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class UpdatePaymentSettingsDTO
    {
        public decimal? MinimumWithdrawalAmount { get; set; }
        public decimal? MaximumWithdrawalAmount { get; set; }
        public decimal? PlatformCommissionPercentage { get; set; }
        public List<string>? EnabledPaymentMethods { get; set; }
        public bool? Is3DSecureEnabled { get; set; }
    }

    public class CreateSupportTicketDTO
    {
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? PaymentId { get; set; }
        public string? AttachmentUrl { get; set; }
    }

    public class PaymentSecurityDTO
    {
        public bool RequiresVerification { get; set; }
        public string VerificationType { get; set; } = string.Empty;
        public string SecurityLevel { get; set; } = string.Empty;
    }

    // Admin/Content Moderation DTO
    public class ContentModerationDTO
    {
        public int Id { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public int ContentId { get; set; }
        public string ContentPreview { get; set; } = string.Empty;
        public int ReporterId { get; set; }
        public string ReporterName { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ReportCount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public int? ResolvedBy { get; set; }
        public string? ResolutionNotes { get; set; }
    }
}

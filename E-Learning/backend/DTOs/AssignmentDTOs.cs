// DTOs/AssignmentDTOs.cs
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    // Assignment Create
    public class AssignmentCreateDTO
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CourseId { get; set; }

        public int? ModuleId { get; set; }
        public int? LessonId { get; set; }

        public int MaxScore { get; set; } = 100;
        public int PassingScore { get; set; } = 50;
        
        [Required]
        public DateTime DueDate { get; set; }

        public bool AllowLateSubmission { get; set; } = false;
        public int? LateSubmissionDays { get; set; }
        public decimal? LatePenaltyPercentage { get; set; } = 10;
        public string? AttachmentUrl { get; set; }
        public string? SampleSolutionUrl { get; set; }
        
        [MaxLength(50)]
        public string SubmissionFormat { get; set; } = "Any";

        public string? Instructions { get; set; }
        public Dictionary<string, decimal>? Rubric { get; set; } // Criteria -> MaxScore
        public int AssignmentPoints { get; set; } = 20;
        public bool IsMandatory { get; set; } = true;
    }

    // Assignment Update
    public class AssignmentUpdateDTO
    {
        [MaxLength(200)]
        public string? Title { get; set; }

        public string? Description { get; set; }
        public int? MaxScore { get; set; }
        public int? PassingScore { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? AllowLateSubmission { get; set; }
        public int? LateSubmissionDays { get; set; }
        public decimal? LatePenaltyPercentage { get; set; }
        public string? AttachmentUrl { get; set; }
        public string? SampleSolutionUrl { get; set; }
        public string? SubmissionFormat { get; set; }
        public string? Instructions { get; set; }
        public Dictionary<string, decimal>? Rubric { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsMandatory { get; set; }
    }

    // Assignment Response
    public class AssignmentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int? ModuleId { get; set; }
        public string? ModuleTitle { get; set; }
        public int? LessonId { get; set; }
        public string? LessonTitle { get; set; }
        public int MaxScore { get; set; }
        public int PassingScore { get; set; }
        public DateTime DueDate { get; set; }
        public bool AllowLateSubmission { get; set; }
        public int? LateSubmissionDays { get; set; }
        public decimal? LatePenaltyPercentage { get; set; }
        public string? AttachmentUrl { get; set; }
        public string? SampleSolutionUrl { get; set; }
        public string SubmissionFormat { get; set; }
        public string? Instructions { get; set; }
        public Dictionary<string, decimal>? Rubric { get; set; }
        public int AssignmentPoints { get; set; }
        public bool IsPublished { get; set; }
        public bool IsMandatory { get; set; }
        public int SubmissionCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool HasSubmitted { get; set; }
        public AssignmentSubmissionDTO? MySubmission { get; set; }
    }

    // Assignment Submission
    public class SubmitAssignmentDTO
    {
        public string? AnswerText { get; set; }
        
        public string? AttachmentUrl { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public long? FileSize { get; set; }
        
        public Dictionary<string, decimal>? RubricSelfAssessment { get; set; }
    }

    // Grade Assignment
    public class GradeAssignmentDTO
    {
        [Required]
        public decimal Score { get; set; }

        public string? Feedback { get; set; }
        public Dictionary<string, decimal>? RubricScores { get; set; }
        public bool ReturnForResubmission { get; set; } = false;
        public string? ResubmissionInstructions { get; set; }
        public DateTime? ResubmissionDeadline { get; set; }
    }

    // Assignment Submission Response
    public class AssignmentSubmissionDTO
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public string AssignmentTitle { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? AnswerText { get; set; }
        public string? AttachmentUrl { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public long? FileSize { get; set; }
        public decimal? Score { get; set; }
        public decimal? MaxScore { get; set; }
        public string? Grade { get; set; }
        public bool IsGraded { get; set; }
        public int? GradedBy { get; set; }
        public string? GraderName { get; set; }
        public string? Feedback { get; set; }
        public Dictionary<string, decimal>? RubricScores { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsLate { get; set; }
        public decimal? LatePenalty { get; set; }
        public string Status { get; set; }
        public bool IsResubmission { get; set; }
        public int? OriginalSubmissionId { get; set; }
        public int PointsEarned { get; set; }
    }

    // Assignment Statistics
    public class AssignmentStatsDTO
    {
        public int AssignmentId { get; set; }
        public string AssignmentTitle { get; set; }
        public int TotalSubmissions { get; set; }
        public int TotalStudents { get; set; }
        public decimal SubmissionRate { get; set; }
        public decimal AverageScore { get; set; }
        public decimal PassRate { get; set; }
        public int LateSubmissions { get; set; }
        public int GradedSubmissions { get; set; }
        public int UngradedSubmissions { get; set; }
        public Dictionary<string, int> GradeDistribution { get; set; }
        public List<TopSubmissionDTO> TopSubmissions { get; set; }
    }

    public class TopSubmissionDTO
    {
        public int SubmissionId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? ProfileImage { get; set; }
        public decimal Score { get; set; }
        public decimal Percentage { get; set; }
        public bool IsLate { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? GradedAt { get; set; }
    }
}
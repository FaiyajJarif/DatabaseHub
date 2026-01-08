using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    // Base competition representation used across services
    public class CompetitionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? CompetitionType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "upcoming"; // upcoming, ongoing, completed
        public int? ClanId { get; set; }
        public int? CourseId { get; set; }
        public int CreatorId { get; set; }
        public bool IsPublic { get; set; } = true;
        public int ParticipantCount { get; set; }
        public int PrizePool { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // Lightweight DTO for listing competitions in dashboards
    public class CompetitionSummaryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ParticipantCount { get; set; }
        public bool IsRegistered { get; set; }
    }
}

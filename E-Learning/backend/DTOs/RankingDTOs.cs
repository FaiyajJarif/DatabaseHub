// DTOs/RankingDTOs.cs
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    // Update Score
    public class UpdateScoreDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string ScoreType { get; set; } // Quiz, Assignment, Completion, Forum, Competition

        [Required]
        public int Points { get; set; }

        public string? Description { get; set; }
        public int? CourseId { get; set; }
        public int? QuizId { get; set; }
        public int? AssignmentId { get; set; }
        public int? CompetitionId { get; set; }
        public int? PostId { get; set; }
    }

    // Ranking Response
    public class RankingDTO
    {
        public int Rank { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int TotalPoints { get; set; }
        public int WeeklyPoints { get; set; }
        public int MonthlyPoints { get; set; }
        public int QuizPoints { get; set; }
        public int AssignmentPoints { get; set; }
        public int CompletionPoints { get; set; }
        public int ForumPoints { get; set; }
        public int CompetitionPoints { get; set; }
        public int StreakDays { get; set; }
        public int TotalCoursesCompleted { get; set; }
        public decimal AverageGrade { get; set; }
        public List<string> Badges { get; set; }
        public bool IsCurrentUser { get; set; }
        public DateTime LastActivity { get; set; }
    }

    // Leaderboard
    public class LeaderboardDTO
    {
        public string Timeframe { get; set; } // daily, weekly, monthly, alltime
        public string Type { get; set; } // overall, quiz, assignment, forum, competition
        public int? UniversityId { get; set; }
        public int? DepartmentId { get; set; }
        public int? CourseId { get; set; }
        public int? ClanId { get; set; }
        public List<RankingDTO> Rankings { get; set; }
        public RankingDTO? CurrentUserRanking { get; set; }
        public LeaderboardStatsDTO Stats { get; set; }
        public PaginationInfoDTO Pagination { get; set; }
    }

    // Leaderboard Stats
    public class LeaderboardStatsDTO
    {
        public int TotalParticipants { get; set; }
        public int AveragePoints { get; set; }
        public int MaxPoints { get; set; }
        public int MinPoints { get; set; }
        public Dictionary<string, int> PointDistribution { get; set; }
        public List<TopBadgeDTO> TopBadges { get; set; }
        public List<RecentAchievementDTO> RecentAchievements { get; set; }
    }

    // Badge
    public class BadgeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // Course, Quiz, Assignment, Forum, Competition, Streak, Level
        public string IconUrl { get; set; }
        public int PointsRequired { get; set; }
        public int UsersEarned { get; set; }
        public bool IsEarned { get; set; }
        public DateTime? EarnedAt { get; set; }
    }

    // Top Badge
    public class TopBadgeDTO
    {
        public BadgeDTO Badge { get; set; }
        public int EarnedCount { get; set; }
        public decimal Percentage { get; set; }
    }

    // Recent Achievement
    public class RecentAchievementDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? ProfileImage { get; set; }
        public string AchievementType { get; set; }
        public string Description { get; set; }
        public int PointsEarned { get; set; }
        public DateTime EarnedAt { get; set; }
    }

    // User Ranking Stats
    public class UserRankingStatsDTO
    {
        public int CurrentRank { get; set; }
        public int TotalPoints { get; set; }
        public int PointsToNextRank { get; set; }
        public int NextRank { get; set; }
        public int QuizPoints { get; set; }
        public int AssignmentPoints { get; set; }
        public int CompletionPoints { get; set; }
        public int ForumPoints { get; set; }
        public int CompetitionPoints { get; set; }
        public int StreakDays { get; set; }
        public int BestStreak { get; set; }
        public List<BadgeDTO> Badges { get; set; }
        public List<PointsHistoryDTO> PointsHistory { get; set; }
        public RankingProgressDTO Progress { get; set; }
    }

    public class PointsHistoryDTO
    {
        public DateTime Date { get; set; }
        public int PointsEarned { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
    }

    public class RankingProgressDTO
    {
        public int CurrentLevel { get; set; }
        public int CurrentLevelPoints { get; set; }
        public int PointsForNextLevel { get; set; }
        public decimal LevelProgressPercentage { get; set; }
        public string NextLevelTitle { get; set; }
        public List<LevelDTO> Levels { get; set; }
    }

    public class LevelDTO
    {
        public int Level { get; set; }
        public string Title { get; set; }
        public int PointsRequired { get; set; }
        public string BadgeUrl { get; set; }
        public string Color { get; set; }
        public bool IsUnlocked { get; set; }
    }

    // Common leaderboard entry projections - aligned with service usage
    public class LeaderboardEntryDTO
    {
        public int Rank { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? ProfileImage { get; set; }
        public string? UniversityName { get; set; }
        public float Score { get; set; }
        public float ProgressPercentage { get; set; }
        public int QuizScore { get; set; }
        public int AssignmentScore { get; set; }
        public DateTime LastActivity { get; set; }
        public int Level { get; set; }
        public int PreviousRank { get; set; }
        public int Change { get; set; }
    }

    public class TeacherRankingDTO
    {
        public int Rank { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int CoursesCreated { get; set; }
        public int TotalStudents { get; set; }
        public decimal AverageRating { get; set; }
        public decimal TotalEarnings { get; set; }
    }

    // Clan Ranking
    public class ClanRankingDTO
    {
        public int Rank { get; set; }
        public ClanDTO Clan { get; set; }
        public int TotalPoints { get; set; }
        public int WeeklyPoints { get; set; }
        public int MonthlyPoints { get; set; }
        public int MemberCount { get; set; }
        public decimal AverageMemberPoints { get; set; }
        public int CompetitionWins { get; set; }
        public int ForumPosts { get; set; }
        public bool IsMyClan { get; set; }
    }

    // University Ranking
    public class UniversityRankingDTO
    {
        public int Rank { get; set; }
        public UniversityDTO University { get; set; }
        public int TotalPoints { get; set; }
        public int StudentCount { get; set; }
        public int TeacherCount { get; set; }
        public int CourseCount { get; set; }
        public decimal AverageCourseRating { get; set; }
        public decimal CompletionRate { get; set; }
        public int CompetitionWins { get; set; }
    }

    // Simple user score projection used in ranking services
    public class UserScoreDTO
    {
        public int UserId { get; set; }
        public int Score { get; set; }
    }
}
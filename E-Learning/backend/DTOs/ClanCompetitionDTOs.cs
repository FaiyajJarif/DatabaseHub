using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

// Competition DTOs
public class CompetitionDetailDTO : CompetitionDTO
{
    public List<CompetitionParticipantDTO> Participants { get; set; }
    public int TotalParticipants { get; set; }
    public new string Status { get; set; }
}

public class CreateCompetitionDTO
{
    [Required]
    public string Title { get; set; }

    public string? Description { get; set; }

    public string? CompetitionType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class UpdateCompetitionDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class CompetitionParticipantDTO
{
    public int ParticipantId { get; set; }
    public string ParticipantName { get; set; }
    public string ParticipantType { get; set; } // Individual, Team
    public int Score { get; set; }
    public int Rank { get; set; }
    public string Status { get; set; }
}

public class CompetitionResultDTO
{
    public int CompetitionId { get; set; }
    public int ParticipantId { get; set; }
    public int FinalScore { get; set; }
    public int FinalRank { get; set; }
    public string Result { get; set; } // Winner, Qualified, Participated
    public DateTime? SubmittedAt { get; set; }
}

public class CompetitionLeaderboardDTO
{
    public List<CompetitionParticipantDTO> Participants { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}

public class SubmitCompetitionDTO
{
    [Required]
    public string Submission { get; set; }

    public List<IFormFile>? Attachments { get; set; }
}

public class CompetitionStatsDTO
{
    public int TotalParticipants { get; set; }
    public int ActiveParticipants { get; set; }
    public decimal AverageScore { get; set; }
    public int HighestScore { get; set; }
}

public class UserCompetitionStatsDTO
{
    public int CompetitionsJoined { get; set; }
    public int CompetitionsWon { get; set; }
    public decimal WinRate { get; set; }
    public int TotalPrizeWinnings { get; set; }
}

public class CreateTeamDTO
{
    [Required]
    public string TeamName { get; set; }

    public List<int>? MemberIds { get; set; }
}

public class TeamDTO
{
    public int Id { get; set; }
    public int CompetitionId { get; set; }
    public string TeamName { get; set; }
    public int LeaderId { get; set; }
    public List<UserDTO> Members { get; set; }
    public int TotalScore { get; set; }
}

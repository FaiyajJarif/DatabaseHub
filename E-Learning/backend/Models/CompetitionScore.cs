namespace backend.Models;

public class CompetitionScore
{
    public int Id { get; set; }
    public int CompetitionId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
    public int Rank { get; set; }
    public DateTime SubmittedAt { get; set; }

    public Competition? Competition { get; set; }
    public User? User { get; set; }
}

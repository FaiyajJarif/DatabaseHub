namespace backend.Models;

public class Leaderboard
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GlobalRank { get; set; }
    public int TotalPoints { get; set; }
    public int CoursesCompleted { get; set; }
    public int QuizzesAttempted { get; set; }
    public int CorrectAnswers { get; set; }
    public DateTime LastUpdated { get; set; }

    public User? User { get; set; }
}

namespace backend.Models;

public class Ranking
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? CourseId { get; set; }
    public int? UniversityId { get; set; }
    public int? ClanId { get; set; }
    public int RankPosition { get; set; }
    public int TotalPoints { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
    public Course? Course { get; set; }
    public University? University { get; set; }
    public Clan? Clan { get; set; }
}

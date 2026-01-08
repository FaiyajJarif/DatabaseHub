namespace backend.Models;

public class PostVote
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public int VoteType { get; set; } // 1 for upvote, -1 for downvote
    public DateTime CreatedAt { get; set; }

    public Post? Post { get; set; }
    public User? User { get; set; }
}

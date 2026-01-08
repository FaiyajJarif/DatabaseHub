namespace backend.Models;

public class CommentVote
{
    public int Id { get; set; }
    public int CommentId { get; set; }
    public int UserId { get; set; }
    public int VoteType { get; set; } // 1 for upvote, -1 for downvote
    public DateTime CreatedAt { get; set; }

    public Comment? Comment { get; set; }
    public User? User { get; set; }
}

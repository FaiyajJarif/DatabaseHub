using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int PostId { get; set; }

        // Nested comments
        public int? ParentCommentId { get; set; }
        public int Depth { get; set; } = 0;

        // Stats
        public int UpvoteCount { get; set; } = 0;
        public int DownvoteCount { get; set; } = 0;
        public int ReplyCount { get; set; } = 0;

        // Special types
        public bool IsAnswer { get; set; } = false;
        public bool IsTeacherAnswer { get; set; } = false;
        public bool IsBestAnswer { get; set; } = false;

        // Moderation
        public bool IsReported { get; set; } = false;
        public int ReportCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        [ForeignKey("ParentCommentId")]
        public virtual Comment? ParentComment { get; set; }

        public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
        public virtual ICollection<CommentVote> Votes { get; set; } = new List<CommentVote>();
    }
}
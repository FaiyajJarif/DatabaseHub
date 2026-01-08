using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("CompetitionParticipants")]
    public class CompetitionParticipant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CompetitionId { get; set; }

        // For team competitions
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }

        // Participation Details
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        // Results
        public decimal? Score { get; set; }
        public int? Rank { get; set; }
        public int? TimeTaken { get; set; } // in seconds

        // Status
        [MaxLength(20)]
        public string Status { get; set; } = "Registered"; // Registered, Started, Completed, Disqualified

        // Prize
        public bool WonPrize { get; set; } = false;
        public string? PrizeWon { get; set; }
        public int PointsEarned { get; set; } = 0;

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("CompetitionId")]
        public virtual Competition Competition { get; set; }
    }
}
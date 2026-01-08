using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("ClanMembers")]
    public class ClanMember
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ClanId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "Member"; // Leader, CoLeader, Elder, Member

        // Member Stats within Clan
        public int ContributionPoints { get; set; } = 0;
        public int WeeklyPoints { get; set; } = 0;
        public int MonthlyPoints { get; set; } = 0;

        // Activity
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastActive { get; set; }
        public int TotalPosts { get; set; } = 0;
        public int TotalComments { get; set; } = 0;

        // Settings
        public bool ReceiveNotifications { get; set; } = true;

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("ClanId")]
        public virtual Clan Clan { get; set; } = null!;
    }
}
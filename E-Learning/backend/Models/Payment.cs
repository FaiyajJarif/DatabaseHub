using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("Payments")]
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        // What is being paid for
        public int? CourseId { get; set; }
        public string? PaymentFor { get; set; } // Course, Certificate, Competition, Donation

        // Payment Details
        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } // bKash, Nagad, Card, Bank, Rocket

        [Required]
        [MaxLength(100)]
        public string TransactionId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        // Platform Commission System
        public decimal PlatformCommission { get; set; } = 0;
        public decimal TeacherEarning { get; set; } = 0;
        public decimal PlatformEarning { get; set; } = 0;

        [Required]
        [MaxLength(10)]
        public string Currency { get; set; } = "BDT";

        // Status Tracking
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Processing, Completed, Failed, Refunded, Cancelled

        // Payment Provider Details
        public string? PaymentProvider { get; set; } // Stripe, SSLCommerz, bKash API
        public string? PaymentDetails { get; set; } // JSON response from provider

        // Refund System
        public bool IsRefunded { get; set; } = false;
        public decimal? RefundAmount { get; set; }
        public DateTime? RefundedAt { get; set; }
        public string? RefundReason { get; set; }

        // Invoice
        public string? InvoiceNumber { get; set; }
        public string? InvoiceUrl { get; set; }

        // Tax
        public decimal? TaxAmount { get; set; }
        public decimal? DiscountAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }
    }
}
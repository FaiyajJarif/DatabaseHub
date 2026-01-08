using System.ComponentModel.DataAnnotations;

// DTOs/PaymentDTOs.cs
namespace backend.DTOs
{
    // Core payment record
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string Status { get; set; } = "pending"; // pending, completed, failed, refunded
        public string PaymentMethod { get; set; } = null!;
        public string TransactionId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

    public class PaymentMethodDTO
    {
        public string Id { get; set; } = null!;
        public string MethodType { get; set; } = null!; // Card, bKash, Nagad, Bank
        public string MaskedDetails { get; set; } = null!;
        public bool IsDefault { get; set; }
    }

    public class SavedPaymentMethodDTO : PaymentMethodDTO
    {
        public DateTime SavedAt { get; set; }
        public string? Nickname { get; set; }
    }

    // Payment Initiation
    public class InitiatePaymentDTO
    {
        [Required]
        public string PaymentMethod { get; set; } = null!; // bKash, Nagad, Card, etc.
        
        public string? SavedMethodId { get; set; }
        public string? CouponCode { get; set; }
        
        // For bKash/Nagad
        public string? PhoneNumber { get; set; }
        
        // For Card
        public CardPaymentDTO? CardDetails { get; set; }
    }

    public class CardPaymentDTO
    {
        [Required]
        public string CardNumber { get; set; } = null!;
        
        [Required]
        public string CardHolderName { get; set; } = null!;
        
        [Required]
        public string ExpiryMonth { get; set; } = null!;
        
        [Required]
        public string ExpiryYear { get; set; } = null!;
        
        [Required]
        public string CVV { get; set; } = null!;
    }

    // Payment Verification
    public class VerifyPaymentDTO
    {
        [Required]
        public string TransactionId { get; set; } = null!;
        
        [Required]
        public string VerificationCode { get; set; } = null!; // OTP for mobile payments
    }

    // Webhook
    public class PaymentWebhookDTO
    {
        [Required]
        public string TransactionId { get; set; } = null!;
        
        [Required]
        public string Status { get; set; } = null!;
        
        public decimal? Amount { get; set; }
        public string? Currency { get; set; }
        public DateTime? PaymentTime { get; set; }
        public string? PaymentDetails { get; set; } // JSON
    }

    // Refund
    public class InitiateRefundDTO
    {
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public string Reason { get; set; } = null!;
        
        public string? AdditionalNotes { get; set; }
    }

    public class RefundDTO
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public DateTime RequestedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }

    // Withdrawal
    public class WithdrawalRequestDTO
    {
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public string PaymentMethod { get; set; } = null!; // Bank, bKash, Nagad
        
        [Required]
        public string AccountDetails { get; set; } = null!; // Account number/phone
        
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? AccountHolderName { get; set; }
    }

    public class RejectWithdrawalDTO
    {
        [Required]
        public string Reason { get; set; } = null!;
        
        public string? Notes { get; set; }
    }

    // Payment Method
    public class SavePaymentMethodDTO
    {
        [Required]
        public string MethodType { get; set; } = null!; // Card, bKash, etc.
        
        [Required]
        public string MethodDetails { get; set; } = null!; // JSON
        
        public bool IsDefault { get; set; } = false;
    }

    // Coupon & Discount
    public class ApplyCouponDTO
    {
        [Required]
        public int CourseId { get; set; }
        
        [Required]
        public string CouponCode { get; set; } = null!;
    }

    public class CreateCouponDTO
    {
        [Required]
        public string Code { get; set; } = null!;
        
        [Required]
        public string DiscountType { get; set; } = null!; // Percentage, Fixed
        
        [Required]
        public decimal DiscountValue { get; set; }
        
        public int? CourseId { get; set; }
        public int? MaxUses { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public decimal? MinimumPurchase { get; set; }
    }

    // Invoice
    public class InvoiceDTO
    {
        public string InvoiceNumber { get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public PaymentDTO Payment { get; set; } = null!;
        public CourseDTO Course { get; set; } = null!;
        public UserDTO Student { get; set; } = null!;
        public UserDTO Teacher { get; set; } = null!;
    }

    // Settings
    public class PaymentSettingsDTO
    {
        public decimal PlatformCommission { get; set; } = 10;
        public decimal TaxPercentage { get; set; } = 0;
        public string Currency { get; set; } = "BDT";
        public bool EnableTax { get; set; } = false;
        public bool EnableCoupons { get; set; } = true;
        public decimal MinimumWithdrawalAmount { get; set; } = 500;
        public int WithdrawalProcessingDays { get; set; } = 7;
        
        // Payment Gateways
        public bool EnablebKash { get; set; } = true;
        public bool EnableNagad { get; set; } = true;
        public bool EnableCard { get; set; } = false;
        public bool EnableBankTransfer { get; set; } = true;
    }

    // Support
    public class PaymentSupportDTO
    {
        [Required]
        public int PaymentId { get; set; }
        
        [Required]
        public string IssueType { get; set; } = null!; // Refund, FailedPayment, Billing, Other
        
        [Required]
        public string Description { get; set; } = null!;
        
        public string? AttachmentUrl { get; set; }
    }

    // Validation
    public class ValidatePaymentDTO
    {
        [Required]
        public string SessionId { get; set; } = null!;
        
        [Required]
        public string PaymentToken { get; set; } = null!;
        
        public string? DeviceFingerprint { get; set; }
        public string? IpAddress { get; set; }
    }

    // Checkout
    public class CheckoutDTO
    {
        public CourseDTO Course { get; set; } = null!;
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal PlatformFee { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public bool IsFree { get; set; }
        public List<CouponDTO> AvailableCoupons { get; set; } = null!;
        public List<PaymentMethodDTO> PaymentMethods { get; set; } = null!;
    }

    // Response DTOs
    public class PaymentResponseDTO
    {
        public int Id { get; set; }
        public string TransactionId { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public CourseDTO Course { get; set; } = null!;
        public UserDTO User { get; set; } = null!;
        public string InvoiceUrl { get; set; } = null!;
    }

    public class EarningsResponseDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = null!;
        public DateTime EarnedAt { get; set; }
        public string Status { get; set; } = null!; // Pending, Available, Withdrawn
        public CourseDTO Course { get; set; } = null!;
        public PaymentDTO Payment { get; set; } = null!;
    }

    public class WithdrawalResponseDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string Status { get; set; } = null!; // Pending, Approved, Rejected, Completed
        public DateTime RequestedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public string AccountDetails { get; set; } = null!;
        public string? RejectionReason { get; set; }
    }

    // Additional Payment DTOs
    public class PaymentCreateDTO
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string? CouponCode { get; set; }
    }

    public class EarningsSummaryDTO
    {
        public decimal TotalEarnings { get; set; }
        public decimal EarningsThisMonth { get; set; }
        public decimal EarningsThisYear { get; set; }
        public int TotalStudents { get; set; }
        public int ActiveCourses { get; set; }
        public List<CourseEarningsDTO> CourseEarnings { get; set; } = null!;
    }

    public class CourseEarningsDTO
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public decimal TotalEarnings { get; set; }
        public int StudentCount { get; set; }
    }

    public class RevenueDistributionDTO
    {
        public decimal PlatformShare { get; set; }
        public decimal TeacherShare { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class PaymentSummaryDTO
    {
        public int TotalTransactions { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalRefunds { get; set; }
        public int SuccessfulPayments { get; set; }
        public int FailedPayments { get; set; }
        public List<PaymentMethodStatsDTO> PaymentMethodStats { get; set; } = null!;
    }

    public class PaymentMethodStatsDTO
    {
        public string PaymentMethod { get; set; } = null!;
        public int TransactionCount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class PaymentStatsDTO
    {
        public int TotalTransactions { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ActiveUsers { get; set; }
        public decimal AverageTransactionValue { get; set; }
    }

    public class ValidationResultDTO
    {
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class DiscountDTO
    {
        public string CouponCode { get; set; } = null!;
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    public class CouponDTO
    {
        public string Code { get; set; } = null!;
        public decimal DiscountPercentage { get; set; }
        public decimal MaxDiscountAmount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class TransactionDTO
    {
        public int Id { get; set; }
        public string TransactionType { get; set; } = null!; // Payment, Refund, Withdrawal
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

    public class UserPaymentStatsDTO
    {
        public int TotalPurchases { get; set; }
        public decimal TotalSpent { get; set; }
        public int CoursesOwned { get; set; }
        public decimal TotalEarnings { get; set; }
    }

}
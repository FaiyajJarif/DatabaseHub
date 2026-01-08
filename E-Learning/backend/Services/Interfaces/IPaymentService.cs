using backend.DTOs;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IPaymentService
    {
        // Course Payments
        Task<ServiceResult<PaymentResponseDTO>> InitiateCoursePayment(int courseId, int userId, InitiatePaymentDTO paymentDto);
        Task<ServiceResult<CheckoutDTO>> GetCourseCheckout(int courseId, int userId);
        
        // Payment Processing & Webhooks
        Task<ServiceResult<PaymentDTO>> ProcessPaymentWebhook(PaymentWebhookDTO webhookDto);
        Task<ServiceResult<PaymentDTO>> VerifyPayment(int paymentId, int userId, VerifyPaymentDTO verifyDto);
        
        // Payment History & Details
        Task<ServiceResult<PaymentDTO>> GetPaymentById(int paymentId, int userId);
        Task<ServiceResult<PaginatedResponse<PaymentDTO>>> GetPaymentHistory(int userId, int page = 1, int pageSize = 20, string? status = null, string? paymentMethod = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<ServiceResult<List<PaymentDTO>>> GetAllPayments(int page = 1, int pageSize = 20, string? status = null, string? paymentMethod = null);
        
        // Refund Operations
        Task<ServiceResult<RefundDTO>> InitiateRefund(int paymentId, int userId, InitiateRefundDTO refundDto);
        Task<ServiceResult<RefundDTO>> GetRefundStatus(int refundId, int userId);
        Task<ServiceResult<bool>> ProcessRefund(int refundId, int adminId);
        
        // Invoice Operations
        Task<ServiceResult<InvoiceDTO>> GetPaymentInvoice(int paymentId, int userId);
        Task<ServiceResult<bool>> SendInvoiceEmail(int paymentId, int userId);
        
        // Withdrawal Operations
        Task<ServiceResult<WithdrawalResponseDTO>> RequestWithdrawal(int teacherId, WithdrawalRequestDTO withdrawalDto);
        Task<ServiceResult<PaginatedResponse<WithdrawalResponseDTO>>> GetWithdrawalHistory(int teacherId, int page = 1, int pageSize = 20, string? status = null);
        Task<ServiceResult<PaginatedResponse<WithdrawalResponseDTO>>> GetPendingWithdrawals(int page = 1, int pageSize = 20);
        Task<ServiceResult<bool>> ApproveWithdrawal(int withdrawalId, int adminId);
        Task<ServiceResult<bool>> RejectWithdrawal(int withdrawalId, int adminId, RejectWithdrawalDTO rejectDto);
        
        // Teacher Earnings
        Task<ServiceResult<PaginatedResponse<EarningsResponseDTO>>> GetTeacherEarnings(int teacherId, int page = 1, int pageSize = 20, DateTime? startDate = null, DateTime? endDate = null, string? timeframe = null);
        Task<ServiceResult<EarningsSummaryDTO>> GetEarningsSummary(int teacherId);
        Task<ServiceResult<PaginatedResponse<EarningsResponseDTO>>> GetPlatformEarnings(DateTime? startDate = null, DateTime? endDate = null, string? timeframe = null);
        
        // Payment Methods
        Task<ServiceResult<List<PaymentMethodDTO>>> GetAvailablePaymentMethods();
        Task<ServiceResult<List<SavedPaymentMethodDTO>>> GetSavedPaymentMethods(int userId);
        Task<ServiceResult<SavedPaymentMethodDTO>> SavePaymentMethod(int userId, SavePaymentMethodDTO methodDto);
        Task<ServiceResult<bool>> RemoveSavedPaymentMethod(int userId, string methodId);
        
        // Statistics
        Task<ServiceResult<UserPaymentStatsDTO>> GetUserPaymentStats(int userId);
        Task<ServiceResult<PaymentSummaryDTO>> GetPlatformPaymentStats(DateTime? startDate = null, DateTime? endDate = null);
        
        // Coupons
        Task<ServiceResult<DiscountDTO>> ApplyCoupon(int userId, string couponCode);
        Task<ServiceResult<CouponDTO>> ValidateCoupon(string couponCode);
        Task<ServiceResult<CouponDTO>> CreateCoupon(CreateCouponDTO couponDto);
        
        // Transaction History
        Task<ServiceResult<PaginatedResponse<TransactionDTO>>> GetTransactionHistory(int userId, string? type = null, int page = 1, int pageSize = 20);
        
        // Security & Settings
        Task<ServiceResult<ValidationResultDTO>> ValidatePaymentSecurity(int userId, string validationData);
        Task<ServiceResult<PaymentSettingsDTO>> GetPaymentSettings();
        Task<ServiceResult<bool>> UpdatePaymentSettings(int adminId, PaymentSettingsDTO settingsDto);
        
        // Export & Support
        Task<ServiceResult<PaymentDTO>> ExportPayments(DateTime startDate, DateTime endDate, string format);
        Task<ServiceResult<PaymentDTO>> CreatePaymentSupportTicket(int userId, PaymentSupportDTO supportDto);
        Task<ServiceResult<PaginatedResponse<PaymentDTO>>> GetPaymentSupportTickets(int userId, string? status = null, int page = 1, int pageSize = 20);
    }
}

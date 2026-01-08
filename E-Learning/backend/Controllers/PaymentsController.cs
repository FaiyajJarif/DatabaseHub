using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // COURSE PAYMENTS

        [HttpPost("course/{courseId}")]
        public async Task<IActionResult> InitiateCoursePayment(int courseId, [FromBody] InitiatePaymentDTO paymentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.InitiateCoursePayment(courseId, userId, paymentDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Payment initiated successfully",
                payment = result.Data
            });
        }

        [HttpGet("course/{courseId}/checkout")]
        public async Task<IActionResult> GetCourseCheckout(int courseId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetCourseCheckout(courseId, userId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                checkout = result.Data
            });
        }

        // PAYMENT PROCESSING

        [HttpPost("process")]
        [AllowAnonymous] // Payment gateways will call this
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentWebhookDTO webhookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _paymentService.ProcessPaymentWebhook(webhookDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Payment processed successfully",
                payment = result.Data
            });
        }

        [HttpPost("verify/{paymentId}")]
        public async Task<IActionResult> VerifyPayment(int paymentId, [FromBody] VerifyPaymentDTO verifyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.VerifyPayment(paymentId, userId, verifyDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Payment verified successfully",
                payment = result.Data
            });
        }

        // PAYMENT HISTORY

        [HttpGet]
        public async Task<IActionResult> GetPaymentHistory(
            [FromQuery] string? status = null,
            [FromQuery] string? paymentMethod = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetPaymentHistory(
                userId, page, pageSize, status, paymentMethod, startDate, endDate);
            
            return Ok(new {
                success = true,
                payments = result.Data?.Items ?? new List<PaymentDTO>(),
                totalCount = result.Data?.TotalCount ?? 0,
                totalAmount = 0, // Not in PaginatedResponse
                page = result.Data?.PageNumber ?? page,
                pageSize = result.Data?.PageSize ?? pageSize,
                totalPages = result.Data?.TotalPages ?? 0
            });
        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPayment(int paymentId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetPaymentById(paymentId, userId);
            
            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                payment = result.Data
            });
        }

        [HttpGet("invoice/{paymentId}")]
        public async Task<IActionResult> GetInvoice(int paymentId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetPaymentInvoice(paymentId, userId);
            
            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                invoice = result.Data
            });
        }

        [HttpPost("invoice/{paymentId}/send")]
        public async Task<IActionResult> SendInvoice(int paymentId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.SendInvoiceEmail(paymentId, userId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Invoice sent to email successfully"
            });
        }

        // REFUND SYSTEM

        [HttpPost("{paymentId}/refund")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InitiateRefund(int paymentId, [FromBody] InitiateRefundDTO refundDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.InitiateRefund(paymentId, adminId, refundDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Refund initiated successfully",
                refund = result.Data
            });
        }

        [HttpGet("{paymentId}/refund-status")]
        public async Task<IActionResult> GetRefundStatus(int paymentId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetRefundStatus(paymentId, userId);
            
            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                refund = result.Data
            });
        }

        [HttpPost("refund/{refundId}/process")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProcessRefund(int refundId)
        {
            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.ProcessRefund(refundId, adminId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Refund processed successfully",
                refund = result.Data
            });
        }

        // TEACHER EARNINGS

        [Authorize(Roles = "Teacher")]
        [HttpGet("earnings")]
        public async Task<IActionResult> GetTeacherEarnings(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var teacherId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (teacherId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetTeacherEarnings(
                teacherId, page, pageSize, startDate, endDate);
            
            return Ok(new {
                success = true,
                earnings = result.Data?.Items ?? new List<EarningsResponseDTO>(),
                totalEarnings = 0, // Not in PaginatedResponse
                pendingBalance = 0, // Not in PaginatedResponse
                availableBalance = 0, // Not in PaginatedResponse
                page = result.Data?.PageNumber ?? page,
                pageSize = result.Data?.PageSize ?? pageSize,
                totalPages = result.Data?.TotalPages ?? 0
            });
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("earnings/summary")]
        public async Task<IActionResult> GetEarningsSummary()
        {
            var teacherId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (teacherId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetEarningsSummary(teacherId);
            
            return Ok(new {
                success = true,
                summary = result.Data
            });
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("withdraw")]
        public async Task<IActionResult> RequestWithdrawal([FromBody] WithdrawalRequestDTO withdrawalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacherId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (teacherId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.RequestWithdrawal(teacherId, withdrawalDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Withdrawal request submitted successfully",
                withdrawal = result.Data
            });
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("withdrawals")]
        public async Task<IActionResult> GetWithdrawalHistory(
            [FromQuery] string? status = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var teacherId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (teacherId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetWithdrawalHistory(teacherId, page, pageSize, status);
            
            return Ok(new {
                success = true,
                withdrawals = result.Data?.Items ?? new List<WithdrawalResponseDTO>(),
                totalCount = result.Data?.TotalCount ?? 0,
                totalAmount = 0, // Not in PaginatedResponse
                page = result.Data?.PageNumber ?? page,
                pageSize = result.Data?.PageSize ?? pageSize,
                totalPages = result.Data?.TotalPages ?? 0
            });
        }

        // ADMIN PAYMENT MANAGEMENT

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/payments")]
        public async Task<IActionResult> GetAllPayments(
            [FromQuery] string? status = null,
            [FromQuery] string? paymentMethod = null,
            [FromQuery] int? teacherId = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50)
        {
            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetAllPayments(
                page, pageSize, status, paymentMethod);
            
            return Ok(new {
                success = true,
                payments = result.Data ?? new List<PaymentDTO>(),
                totalCount = result.Data?.Count ?? 0,
                totalAmount = 0,
                platformRevenue = 0,
                page = page,
                pageSize = pageSize,
                totalPages = 0
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/earnings")]
        public async Task<IActionResult> GetPlatformEarnings(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string? timeframe = "daily") // daily, weekly, monthly
        {
            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetPlatformEarnings(startDate, endDate, timeframe);
            
            return Ok(new {
                success = true,
                earnings = result.Data?.Items ?? new List<EarningsResponseDTO>()
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/withdrawals/pending")]
        public async Task<IActionResult> GetPendingWithdrawals(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetPendingWithdrawals(page, pageSize);
            
            return Ok(new {
                success = true,
                withdrawals = result.Data?.Items ?? new List<WithdrawalResponseDTO>(),
                totalCount = result.Data?.TotalCount ?? 0,
                totalAmount = 0,
                page = result.Data?.PageNumber ?? page,
                pageSize = result.Data?.PageSize ?? pageSize,
                totalPages = result.Data?.TotalPages ?? 0
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("admin/withdrawals/{withdrawalId}/approve")]
        public async Task<IActionResult> ApproveWithdrawal(int withdrawalId)
        {
            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.ApproveWithdrawal(withdrawalId, adminId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Withdrawal approved and processed",
                withdrawal = result.Data
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("admin/withdrawals/{withdrawalId}/reject")]
        public async Task<IActionResult> RejectWithdrawal(int withdrawalId, [FromBody] RejectWithdrawalDTO rejectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.RejectWithdrawal(withdrawalId, adminId, rejectDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Withdrawal rejected",
                withdrawal = result.Data
            });
        }

        // PAYMENT METHODS

        [HttpGet("payment-methods")]
        public async Task<IActionResult> GetAvailablePaymentMethods()
        {
            var result = await _paymentService.GetAvailablePaymentMethods();
            
            return Ok(new {
                success = true,
                methods = result.Data
            });
        }

        [Authorize]
        [HttpGet("saved-methods")]
        public async Task<IActionResult> GetSavedPaymentMethods()
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetSavedPaymentMethods(userId);
            
            return Ok(new {
                success = true,
                methods = result.Data
            });
        }

        [Authorize]
        [HttpPost("save-method")]
        public async Task<IActionResult> SavePaymentMethod([FromBody] SavePaymentMethodDTO methodDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.SavePaymentMethod(userId, methodDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Payment method saved successfully",
                method = result.Data
            });
        }

        [Authorize]
        [HttpDelete("saved-methods/{methodId}")]
        public async Task<IActionResult> RemoveSavedMethod(string methodId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.RemoveSavedPaymentMethod(userId, methodId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Payment method removed successfully"
            });
        }

        // PAYMENT STATISTICS

        [Authorize]
        [HttpGet("stats")]
        public async Task<IActionResult> GetUserPaymentStats()
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetUserPaymentStats(userId);
            
            return Ok(new {
                success = true,
                stats = result.Data
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/stats")]
        public async Task<IActionResult> GetPlatformPaymentStats(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetPlatformPaymentStats(startDate, endDate);
            
            return Ok(new {
                success = true,
                stats = result.Data
            });
        }

        // COUPON & DISCOUNT SYSTEM

        [HttpPost("apply-coupon")]
        public async Task<IActionResult> ApplyCoupon([FromBody] ApplyCouponDTO couponDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.ApplyCoupon(userId, couponDto.CouponCode);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Coupon applied successfully",
                discount = result.Data
            });
        }

        [HttpGet("coupons/validate/{couponCode}")]
        public async Task<IActionResult> ValidateCoupon(string couponCode, [FromQuery] int courseId)
        {
            if (string.IsNullOrWhiteSpace(couponCode))
                return BadRequest(new { success = false, message = "Coupon code is required" });

            var result = await _paymentService.ValidateCoupon(couponCode);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                coupon = result.Data
            });
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost("coupons")]
        public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponDTO couponDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.CreateCoupon(couponDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Coupon created successfully",
                coupon = result.Data
            });
        }

        // TRANSACTION HISTORY

        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactionHistory(
            [FromQuery] string? type = null, // payment, refund, withdrawal
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetTransactionHistory(userId, type, page, pageSize);
            
            return Ok(new {
                success = true,
                transactions = result.Data?.Items ?? new List<TransactionDTO>(),
                totalCount = result.Data?.TotalCount ?? 0,
                page = result.Data?.PageNumber ?? page,
                pageSize = result.Data?.PageSize ?? pageSize,
                totalPages = result.Data?.TotalPages ?? 0
            });
        }

        // PAYMENT SECURITY

        [Authorize]
        [HttpPost("validate-payment")]
        public async Task<IActionResult> ValidatePaymentSecurity([FromBody] ValidatePaymentDTO validateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.ValidatePaymentSecurity(userId, "");
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Payment validation successful",
                validation = result.Data
            });
        }

        // PAYMENT SETTINGS

        [Authorize(Roles = "Admin")]
        [HttpGet("settings")]
        public async Task<IActionResult> GetPaymentSettings()
        {
            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetPaymentSettings();
            
            return Ok(new {
                success = true,
                settings = result.Data
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("settings")]
        public async Task<IActionResult> UpdatePaymentSettings([FromBody] PaymentSettingsDTO settingsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.UpdatePaymentSettings(adminId, settingsDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Payment settings updated successfully",
                settings = result.Data
            });
        }

        // EXPORT PAYMENT DATA

        [Authorize(Roles = "Admin")]
        [HttpGet("export")]
        public async Task<IActionResult> ExportPayments(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string format = "csv") // csv, excel, pdf
        {
            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.ExportPayments(startDate ?? DateTime.UtcNow.AddMonths(-1), endDate ?? DateTime.UtcNow, format);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            // Return as JSON response since we don't have file export support
            return Ok(new { success = true, data = result.Data });
        }

        // PAYMENT SUPPORT

        [Authorize]
        [HttpPost("support")]
        public async Task<IActionResult> CreatePaymentSupportTicket([FromBody] PaymentSupportDTO supportDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.CreatePaymentSupportTicket(userId, supportDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Support ticket created successfully",
                ticket = result.Data
            });
        }

        [Authorize]
        [HttpGet("support/tickets")]
        public async Task<IActionResult> GetPaymentSupportTickets(
            [FromQuery] string? status = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _paymentService.GetPaymentSupportTickets(userId, status, page, pageSize);
            
            return Ok(new {
                success = true,
                tickets = result.Data?.Items ?? new List<PaymentDTO>(),
                totalCount = result.Data?.TotalCount ?? 0,
                page = result.Data?.PageNumber ?? page,
                pageSize = result.Data?.PageSize ?? pageSize,
                totalPages = result.Data?.TotalPages ?? 0
            });
        }
    }
}
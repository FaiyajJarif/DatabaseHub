using backend.Data;
using backend.DTOs;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<ServiceResult<ReviewDTO>> CreateReview(CreateReviewDTO dto, int userId)
            => Task.FromResult(ServiceResult<ReviewDTO>.FailureResult("Not implemented"));

        public Task<ServiceResult<ReviewDTO>> GetReviewById(int reviewId)
            => Task.FromResult(ServiceResult<ReviewDTO>.FailureResult("Not implemented"));

        public Task<ServiceResult<List<ReviewDTO>>> GetCourseReviews(int courseId, int page, int pageSize)
            => Task.FromResult(ServiceResult<List<ReviewDTO>>.FailureResult("Not implemented"));

        public Task<ServiceResult<List<ReviewDTO>>> GetTeacherReviews(int teacherId, string? sortBy, int page, int pageSize)
            => Task.FromResult(ServiceResult<List<ReviewDTO>>.FailureResult("Not implemented"));

        public Task<ServiceResult<ReviewDTO>> UpdateReview(int reviewId, UpdateReviewDTO dto)
            => Task.FromResult(ServiceResult<ReviewDTO>.FailureResult("Not implemented"));

        public Task<ServiceResult<bool>> DeleteReview(int reviewId)
            => Task.FromResult(ServiceResult<bool>.FailureResult("Not implemented"));

        public Task<ServiceResult<bool>> ApproveReview(int reviewId, int adminId)
            => Task.FromResult(ServiceResult<bool>.FailureResult("Not implemented"));

        public Task<ServiceResult<bool>> RejectReview(int reviewId, int adminId, string reason)
            => Task.FromResult(ServiceResult<bool>.FailureResult("Not implemented"));

        public Task<ServiceResult<List<ReviewDTO>>> GetUserReviews(int userId, int page, int pageSize)
            => Task.FromResult(ServiceResult<List<ReviewDTO>>.FailureResult("Not implemented"));

        public Task<ServiceResult<decimal>> GetCourseAverageRating(int courseId)
            => Task.FromResult(ServiceResult<decimal>.FailureResult("Not implemented"));

        public Task<ServiceResult<decimal>> GetTeacherAverageRating(int teacherId)
            => Task.FromResult(ServiceResult<decimal>.FailureResult("Not implemented"));

        public Task<ServiceResult<ReviewStatsDTO>> GetReviewStats(int? courseId, int? teacherId)
            => Task.FromResult(ServiceResult<ReviewStatsDTO>.FailureResult("Not implemented"));

        public Task<ServiceResult<bool>> LikeReview(int reviewId, int userId)
            => Task.FromResult(ServiceResult<bool>.FailureResult("Not implemented"));

        public Task<ServiceResult<bool>> UnlikeReview(int reviewId, int userId)
            => Task.FromResult(ServiceResult<bool>.FailureResult("Not implemented"));
    }
}

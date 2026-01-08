using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetCourseReviews(int courseId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _reviewService.GetCourseReviews(courseId, page, pageSize);
            
            return Ok(new {
                success = result.Success,
                message = result.Message,
                data = result.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var result = await _reviewService.GetReviewById(id);
            
            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                data = result.Data
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDTO reviewDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _reviewService.CreateReview(reviewDto, userId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return CreatedAtAction(nameof(GetReview), new { id = result.Data?.Id }, new {
                success = true,
                message = "Review submitted successfully",
                data = result.Data
            });
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewDTO reviewDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _reviewService.UpdateReview(id, reviewDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Review updated successfully",
                data = result.Data
            });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _reviewService.DeleteReview(id);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Review deleted successfully"
            });
        }

        [Authorize]
        [HttpPost("{id}/like")]
        public async Task<IActionResult> LikeReview(int id)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _reviewService.LikeReview(id, userId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Review liked successfully"
            });
        }

        [Authorize]
        [HttpPost("{id}/unlike")]
        public async Task<IActionResult> UnlikeReview(int id)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _reviewService.UnlikeReview(id, userId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Review unliked successfully"
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveReview(int id)
        {
            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _reviewService.ApproveReview(id, adminId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Review approved"
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectReview(int id, [FromBody] RejectReviewDTO rejectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (adminId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _reviewService.RejectReview(id, adminId, rejectDto.Reason ?? "No reason provided");
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Review rejected"
            });
        }

        [Authorize]
        [HttpGet("my-reviews")]
        public async Task<IActionResult> GetMyReviews(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _reviewService.GetUserReviews(userId, page, pageSize);
            
            return Ok(new {
                success = result.Success,
                message = result.Message,
                data = result.Data
            });
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("teacher/reviews")]
        public async Task<IActionResult> GetTeacherCourseReviews(
            [FromQuery] string? sortBy = "newest",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var teacherId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (teacherId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _reviewService.GetTeacherReviews(teacherId, sortBy, page, pageSize);
            
            return Ok(new {
                success = result.Success,
                message = result.Message,
                data = result.Data
            });
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetReviewStats([FromQuery] int? courseId = null, [FromQuery] int? teacherId = null)
        {
            var result = await _reviewService.GetReviewStats(courseId, teacherId);
            
            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new {
                success = result.Success,
                message = result.Message,
                data = result.Data
            });
        }
    }
}
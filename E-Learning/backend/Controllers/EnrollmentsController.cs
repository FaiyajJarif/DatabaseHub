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
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserEnrollments(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.GetUserEnrollments(userId, page, pageSize);
            
            return Ok(new {
                success = result.Success,
                message = result.Message,
                data = result.Data
            });
        }

        [HttpGet("{enrollmentId}")]
        public async Task<IActionResult> GetEnrollment(int enrollmentId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.GetEnrollment(enrollmentId);
            
            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                data = result.Data
            });
        }

        [HttpPost("enroll/{courseId}")]
        public async Task<IActionResult> EnrollInCourse(int courseId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.EnrollInCourse(courseId, userId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new { 
                success = true,
                message = "Successfully enrolled in course",
                data = result.Data
            });
        }

        [HttpPost("unenroll/{courseId}")]
        public async Task<IActionResult> Unenroll(int courseId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.UnenrollFromCourse(courseId, userId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new { 
                success = true,
                message = "Successfully unenrolled from course"
            });
        }

        [HttpPut("{enrollmentId}/progress")]
        public async Task<IActionResult> UpdateProgress(int enrollmentId, [FromBody] UpdateProgressDTO progressDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.UpdateEnrollmentProgress(enrollmentId, progressDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Progress updated successfully"
            });
        }

        [HttpPost("{enrollmentId}/complete-lesson/{lessonId}")]
        public async Task<IActionResult> CompleteLesson(int enrollmentId, int lessonId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.CompleteLesson(enrollmentId, lessonId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Lesson marked as completed"
            });
        }

        [HttpPost("{enrollmentId}/complete-module/{moduleId}")]
        public async Task<IActionResult> CompleteModule(int enrollmentId, int moduleId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.CompleteModule(enrollmentId, moduleId, userId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Module marked as completed"
            });
        }

        [HttpPost("{enrollmentId}/complete")]
        public async Task<IActionResult> CompleteCourse(int enrollmentId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.CompleteCourse(enrollmentId, userId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Course completed successfully!"
            });
        }

        [HttpGet("{enrollmentId}/progress")]
        public async Task<IActionResult> GetEnrollmentProgress(int enrollmentId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.GetEnrollmentProgress(enrollmentId, userId);
            
            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                data = result.Data
            });
        }

        [HttpGet("course/{courseId}/progress")]
        public async Task<IActionResult> GetCourseProgress(int courseId)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.GetCourseProgress(courseId, userId);
            
            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                data = result.Data
            });
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetEnrollmentStats()
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.GetEnrollmentStats(userId);
            
            return Ok(new {
                success = true,
                data = result.Data
            });
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpGet("course/{courseId}/students")]
        public async Task<IActionResult> GetCourseStudents(int courseId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.GetCourseStudents(courseId, page, pageSize);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                data = result.Data
            });
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpGet("course/{courseId}/student/{studentId}/progress")]
        public async Task<IActionResult> GetStudentProgress(int courseId, int studentId)
        {
            var teacherId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            
            if (teacherId == 0)
                return Unauthorized(new { success = false, message = "Invalid token" });

            var result = await _enrollmentService.GetStudentProgress(courseId, studentId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                data = result.Data
            });
        }
    }
}
using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments(
            [FromQuery] int? universityId = null,
            [FromQuery] string? search = null,
            [FromQuery] string? departmentType = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _departmentService.GetAllDepartments(page, pageSize);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message, errors = result.Errors });
            
            return Ok(new {
                success = true,
                departments = result.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var result = await _departmentService.GetDepartmentById(id);
            
            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                department = result.Data
            });
        }

        [HttpGet("{id}/courses")]
        public async Task<IActionResult> GetDepartmentCourses(int id,
            [FromQuery] string? difficulty = null,
            [FromQuery] bool? isFree = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _departmentService.GetDepartmentCourses(id, page, pageSize);
            
            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                courses = result.Data
            });
        }

        // GetDepartmentTeachers method not implemented in service interface

        [HttpGet("{id}/stats")]
        public async Task<IActionResult> GetDepartmentStats(int id)
        {
            var result = await _departmentService.GetDepartmentStats(id);
            
            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                stats = result.Data
            });
        }

        // GetPopularDepartments method not implemented in service interface

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDTO departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            var result = await _departmentService.CreateDepartment(departmentDto, adminId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return CreatedAtAction(nameof(GetDepartment), new { id = result.Data.Id }, new {
                success = true,
                message = "Department created successfully",
                department = result.Data
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] UpdateDepartmentDTO departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            var result = await _departmentService.UpdateDepartment(id, adminId, departmentDto);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Department updated successfully",
                department = result.Data
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var adminId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            var result = await _departmentService.DeleteDepartment(id, adminId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new {
                success = true,
                message = "Department deleted successfully"
            });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchDepartments(
            [FromQuery] string query,
            [FromQuery] int? universityId = null)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(new { success = false, message = "Search query is required" });

            var result = await _departmentService.SearchDepartments(query, universityId);
            
            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });
            
            return Ok(new {
                success = true,
                departments = result.Data
            });
        }
    }
}
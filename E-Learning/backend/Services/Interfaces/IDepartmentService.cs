using backend.DTOs;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<ServiceResult<DepartmentDTO>> CreateDepartment(CreateDepartmentDTO dto, int universityId);
        Task<ServiceResult<DepartmentDTO>> GetDepartmentById(int departmentId);
        Task<ServiceResult<List<DepartmentDTO>>> GetDepartmentsByUniversity(int universityId);
        Task<ServiceResult<List<DepartmentDTO>>> GetAllDepartments(int page, int pageSize);
        Task<ServiceResult<DepartmentDTO>> UpdateDepartment(int departmentId, int universityId, UpdateDepartmentDTO dto);
        Task<ServiceResult<bool>> DeleteDepartment(int departmentId, int userId);
        Task<ServiceResult<List<DepartmentDTO>>> SearchDepartments(string query, int? universityId);
        Task<ServiceResult<List<CourseDTO>>> GetDepartmentCourses(int departmentId, int page, int pageSize);
        Task<ServiceResult<DepartmentStatsDTO>> GetDepartmentStats(int departmentId);
    }
}

using backend.DTOs;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IUniversityService
    {
        Task<ServiceResult<UniversityDTO>> CreateUniversity(CreateUniversityDTO dto);
        Task<ServiceResult<UniversityDTO>> GetUniversityById(int universityId);
        Task<ServiceResult<List<UniversityDTO>>> GetAllUniversities(int page, int pageSize);
        Task<ServiceResult<UniversityDTO>> UpdateUniversity(int universityId, UpdateUniversityDTO dto);
        Task<ServiceResult<bool>> DeleteUniversity(int universityId);
        Task<ServiceResult<List<CourseDTO>>> GetUniversityCourses(int universityId, int page, int pageSize);
        Task<ServiceResult<List<TeacherDTO>>> GetUniversityTeachers(int universityId, int page, int pageSize);
        Task<ServiceResult<List<StudentDTO>>> GetUniversityStudents(int universityId, int page, int pageSize);
        Task<ServiceResult<UniversityDetailDTO>> GetUniversityDetails(int universityId);
        Task<ServiceResult<UniversityStatsDTO>> GetUniversityStats(int universityId);
    }
}

namespace backend.Services.Interfaces
{
    public interface ICourseService
    {
        Task<ServiceResult<PaginatedResponse<CourseDTO>>> GetAllCourses(
            string? search, int? universityId, int? departmentId, string? difficulty,
            bool? isFree, string? status, string? sortBy, int page, int pageSize);
        Task<ServiceResult<CourseDetailDTO>> GetCourseById(int id, int? userId);
        Task<ServiceResult<PaginatedResponse<CourseDTO>>> GetCoursesByUniversity(
            int universityId, int page, int pageSize);
        Task<ServiceResult<PaginatedResponse<CourseDTO>>> GetCoursesByDepartment(
            int departmentId, int page, int pageSize);
        Task<ServiceResult<List<CourseDTO>>> GetPopularCourses();
        Task<ServiceResult<List<CourseDTO>>> GetTrendingCourses();
        Task<ServiceResult<List<CourseDTO>>> GetNewCourses();
        Task<ServiceResult<PaginatedResponse<CourseDTO>>> GetCoursesByTeacher(
            int teacherId, int page, int pageSize);
        Task<ServiceResult<CourseDTO>> CreateCourse(CourseCreateDTO dto, int teacherId);
        Task<ServiceResult<CourseDTO>> UpdateCourse(int id, int userId, string userRole, CourseUpdateDTO dto);
        Task<ServiceResult<CourseDTO>> UpdateCourseStatus(int id, int adminId, UpdateCourseStatusDTO dto);
        Task<ServiceResult<EnrollmentDTO>> EnrollInCourse(int userId, int courseId);
        Task<ServiceResult<PaginatedResponse<EnrollmentDTO>>> GetUserEnrolledCourses(
            int userId, string? status, int page, int pageSize);
        Task<ServiceResult<PaginatedResponse<CourseDTO>>> GetUserCreatedCourses(
            int userId, string? status, int page, int pageSize);
        Task<ServiceResult<List<ModuleDTO>>> GetCourseModules(int courseId);
        Task<ServiceResult<PaginatedResponse<ReviewDTO>>> GetCourseReviews(
            int courseId, int page, int pageSize);
        Task<ServiceResult<PaginatedResponse<CourseDTO>>> SearchCourses(
            string query, int? universityId, int? departmentId, int page, int pageSize);
        Task<ServiceResult<PaginatedResponse<CourseDTO>>> FilterCourses(
            CourseFilterDTO filter, int page, int pageSize);
        Task<ServiceResult<bool>> DeleteCourse(int id, int userId, string userRole);
        Task<ServiceResult<CourseStatsDTO>> GetCourseStats(int id);
    }
}
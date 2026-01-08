using backend.DTOs;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task<ServiceResult<EnrollmentDTO>> EnrollInCourse(int courseId, int userId);
        Task<ServiceResult<bool>> UnenrollFromCourse(int courseId, int userId);
        Task<ServiceResult<EnrollmentDTO>> GetEnrollment(int enrollmentId);
        Task<ServiceResult<List<EnrollmentDTO>>> GetUserEnrollments(int userId, int page, int pageSize);
        Task<ServiceResult<List<EnrollmentDTO>>> GetCourseEnrollments(int courseId, int page, int pageSize);
        Task<ServiceResult<bool>> UpdateEnrollmentProgress(int enrollmentId, UpdateProgressDTO dto);
        Task<ServiceResult<EnrollmentProgressDTO>> GetEnrollmentProgress(int enrollmentId, int userId);
        Task<ServiceResult<bool>> CompleteLesson(int enrollmentId, int lessonId);
        Task<ServiceResult<bool>> CompleteModule(int enrollmentId, int moduleId, int userId);
        Task<ServiceResult<bool>> CompleteCourse(int enrollmentId, int userId);
        Task<ServiceResult<bool>> SubmitAssignment(int enrollmentId, int assignmentId, SubmitAssignmentDTO dto);
        Task<ServiceResult<bool>> CompleteEnrollment(int enrollmentId);
        Task<ServiceResult<EnrollmentStatsDTO>> GetEnrollmentStats(int userId);
        Task<ServiceResult<CourseProgressDTO>> GetCourseProgress(int courseId, int userId);
        Task<ServiceResult<List<StudentDTO>>> GetCourseStudents(int courseId, int page, int pageSize);
        Task<ServiceResult<StudentProgressDTO>> GetStudentProgress(int courseId, int studentId);
        Task<ServiceResult<List<EnrollmentResponseDTO>>> GetActiveEnrollments(int userId);
    }
}

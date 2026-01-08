using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<ServiceResult<AuthResponseDTO>> Register(RegisterDTO dto)
            => Task.FromResult(ServiceResult<AuthResponseDTO>.FailureResult("Not implemented"));

        public Task<ServiceResult<AuthResponseDTO>> Login(LoginDTO dto)
            => Task.FromResult(ServiceResult<AuthResponseDTO>.FailureResult("Not implemented"));







        public Task<ServiceResult<bool>> ChangePassword(int userId, ChangePasswordDTO dto)
            => Task.FromResult(ServiceResult<bool>.FailureResult("Not implemented"));

        public Task<ServiceResult<UserDTO>> GetUserProfile(int userId)
            => Task.FromResult(ServiceResult<UserDTO>.FailureResult("Not implemented"));

        public Task<ServiceResult<UserDTO>> UpdateProfile(int userId, UpdateProfileDTO dto)
            => Task.FromResult(ServiceResult<UserDTO>.FailureResult("Not implemented"));

        public Task<ServiceResult<bool>> ForgotPassword(string email)
            => Task.FromResult(ServiceResult<bool>.FailureResult("Not implemented"));

        public Task<ServiceResult<UserDTO>> BecomeTeacher(int userId)
            => Task.FromResult(ServiceResult<UserDTO>.FailureResult("Not implemented"));

        public Task<ServiceResult<UserDTO>> JoinCompetitionMode(int userId)
            => Task.FromResult(ServiceResult<UserDTO>.FailureResult("Not implemented"));

        public Task<ServiceResult<DashboardDTO>> GetUserDashboard(int userId)
            => Task.FromResult(ServiceResult<DashboardDTO>.FailureResult("Not implemented"));

        public Task<ServiceResult<User>> GetUserById(int userId)
            => Task.FromResult(ServiceResult<User>.FailureResult("Not implemented"));
    }
}

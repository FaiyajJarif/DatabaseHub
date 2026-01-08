using backend.DTOs;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IRankingService
    {
        Task<ServiceResult<LeaderboardDTO>> GetLeaderboard(int? universityId, int? departmentId, int? courseId, int? clanId, int page, int pageSize);
        Task<ServiceResult<RankingDTO>> GetUserRanking(int userId, int? universityId, int? departmentId, int? courseId);
        Task<ServiceResult<List<RankingDTO>>> GetTopUsers(int limit);
        Task<ServiceResult<RankingHistoryDTO>> GetUserRankingHistory(int userId, int days);
        Task<ServiceResult<TeacherLeaderboardDTO>> GetTeacherLeaderboard(int? universityId, int? departmentId, int page, int pageSize);
        Task<ServiceResult<RankingDTO>> UpdateUserRanking(int userId);
        Task<ServiceResult<List<RankingDTO>>> UpdateAllRankings();
    }
}

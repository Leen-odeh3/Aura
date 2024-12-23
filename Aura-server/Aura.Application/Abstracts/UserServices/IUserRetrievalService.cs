using Aura.Domain.DTOs.User;

namespace Aura.Application.Abstracts.UserServices;
public interface IUserRetrievalService
{
    Task<UsersWithPaginationResponseDto> GetUsers(int pageNumber, int pageSize, string searchText = null);
    Task<UserResponseDto> GetUserById(int userId);
}
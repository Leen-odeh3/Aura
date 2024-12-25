using Aura.Domain.DTOs.User;

namespace Aura.Application.Abstracts.UserServices;
public interface IUserAccountService
{
    Task<object> LoginUserAsync(UserRequestDto userRequestDto);
    Task<UserResponseDto> RegisterUserAsync(UserRequestDto userRequestDto);
    Task<UserResponseDto> GetUserByJwtTokenAsync();
    Task ChangePasswordAsync(int userId, ChangePasswordRequestDto changePasswordDto);
    Task ChangeUserAboutAsync(int userId, string newAbout);
    public Task<List<UserResponseDto>> GetFollowersAsync(int userId);

}
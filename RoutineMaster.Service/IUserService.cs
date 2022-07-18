using RoutineMaster.Models.Dtos;

namespace RoutineMaster.Service
{
    public interface IUserService
    {
        Task<LoginResultDto> CreateUser(CreateUserDto dto);
        Task<LoginResultDto> Login(LoginDto dto);
    }
}
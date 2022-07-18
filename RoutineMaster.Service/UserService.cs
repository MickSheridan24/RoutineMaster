using RoutineMaster.Data;
using RoutineMaster.Models.Dtos;
using RoutineMaster.Models.Entities;
using static BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RoutineMaster.Service
{
    public class UserService : IUserService
    {
        private RMDataContext context;

        public UserService(RMDataContext context){
            this.context = context;
        }

        public async Task<LoginResultDto> CreateUser(CreateUserDto dto)
        {
            var nameExists = context.Users.Any(u => u.Username == dto.Username);

            if(!nameExists){
                var entity = new User{
                    Username = dto.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                };

                context.Users.Add(entity);
                await context.SaveChangesAsync();


                var income = new UserIncome{
                    UserId = entity.Id
                };

                context.UserIncomes.Add(income);
                await context.SaveChangesAsync();

                
            }

            return new LoginResultDto();
        }

        public Task<LoginResultDto> Login(LoginDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
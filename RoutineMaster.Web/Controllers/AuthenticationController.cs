
using Microsoft.AspNetCore.Mvc;
using RoutineMaster.Models.Dtos;
using RoutineMaster.Service;

namespace RoutineMaster.Web.Controllers
{
    public class AuthenticationController
    {
        private IUserService service;

        public AuthenticationController(IUserService service){
            this.service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto){
            var token = await service.Login(dto);
            return new JsonResult(token);            
        }


        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto){
            var token = await service.CreateUser(dto);
            return new JsonResult(token);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using User.Domain;
using User.WebAPI.Models;

namespace User.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly UserDomainService _userDomainService;

        public LoginController(UserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        //[UnitOfWork(typeof(UserDbContext))]
        [HttpPost]
        public async Task<IActionResult> LoginByPhoneAndPassword(LoginByPhoneAndPasswordRequest request)
        {
            if (request.Password.Length <= 3)
            {
                //体现数据校验
                return BadRequest("密码长度必须大于3");
            }
            var result = await _userDomainService.CheckPasswordAsync(request.PhoneNumber, request.Password);
            switch (result)
            {
                case UserAccessResult.Ok:
                    return Ok("登陆成功");
                case UserAccessResult.PasswordError:
                case UserAccessResult.NoPassword:
                case UserAccessResult.PhoneNumberNotFound:
                    return BadRequest("登陆失败");
                case UserAccessResult.Lockout:
                    return BadRequest("账户被锁定");
                default: throw new ApplicationException($"未知值:{result}");
            }
        }
    }
}

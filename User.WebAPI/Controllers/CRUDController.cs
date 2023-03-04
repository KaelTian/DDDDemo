using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Domain;
using User.Infrastructure;
using User.WebAPI.Attributes;
using User.WebAPI.Models;

namespace User.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserDbContext _userDbContext;
        public CRUDController(IUserRepository userRepository, UserDbContext userDbContext)
        {
            _userRepository = userRepository;
            _userDbContext = userDbContext;
        }

        [HttpPost]
        [UnitOfWork(typeof(UserDbContext))]
        public async Task<IActionResult> AddNewUser(AddUserRequest request)
        {
            if(await _userRepository.FindOneAsync(request.PhoneNumber) != null)
            {
                return BadRequest("手机号已经存在");
            }
            var user=new User.Domain.User(request.PhoneNumber);
            user.ChangePassword(request.password);
            await _userDbContext.Users.AddAsync(user);
            return Ok("User创建陈宫!");
        }

    }
}

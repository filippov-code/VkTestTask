using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using VkTestTask.Domain.Interfaces;
using VkTestTask.Domain.Models;

namespace VkTestTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<string> loginsInProgress = new();
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(string login, string password, bool isAdmin)
        {
            if (!await _userRepository.IsLoginAreAvailableAsync(login) || loginsInProgress.Contains(login))
                return BadRequest("Login are busy");
            if (isAdmin && await _userRepository.AdminAlreadyExistAsync())
                return BadRequest("Admin already exist");

            loginsInProgress.Add(login);
            var newUser = new User
            {
                Login = login,
                PasswordHash = GetPasswordHash(password),
                CreatedDate = DateTime.UtcNow,
                UserGroup = isAdmin ? await _userRepository.GetUserGroupEntityAsync(UserGroupCode.Admin) : await _userRepository.GetUserGroupEntityAsync(UserGroupCode.User),
                UserState = await _userRepository.GetUserStateEntityAsync(UserStateCode.Active)
            };
            await _userRepository.CreateAsync(newUser);
            loginsInProgress.Remove(login);

            return newUser;
        }
        [HttpGet]
        public async Task<ActionResult<User>> GetByLogin(string login)
        {
            var user = await _userRepository.GetByLoginAsync(login);

            return user == null ? UserNotFound() : user;
        }
        [HttpGet]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            return user == null ? UserNotFound() : user;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _userRepository.GetAllAsync());
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetFew(int count, int offset)
        {
            if (count <= 0 || offset < 0)
                return BadRequest("Count or offset cannot be less than zero");

            return Ok(await _userRepository.GetFewAsync(count, offset));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteByLogin(string login)
        {
            var user = await _userRepository.GetByLoginAsync(login);
            if (user == null)
                return UserNotFound();

            await _userRepository.DeleteAsync(user);

            return NoContent();
        }

        #region Helpers
        private byte[] GetPasswordHash(string password) => SHA256.HashData(Encoding.UTF8.GetBytes(password));
        private ActionResult UserNotFound() => BadRequest("User was not found");
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkTestTask.Domain.Models;

namespace VkTestTask.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetAsync(Guid id);
        Task<User?> GetByLoginAsync(string login);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetFewAsync(int count, int offset);
        Task<bool> AdminAlreadyExistAsync();
        Task<bool> IsLoginAreAvailableAsync(string login);
        Task<UserState> GetUserStateEntityAsync(UserStateCode code);
        Task<UserGroup> GetUserGroupEntityAsync(UserGroupCode code);
    }
}

using Microsoft.EntityFrameworkCore;
using VkTestTask.Domain.Interfaces;
using VkTestTask.Domain.Models;
using VkTestTask.Repository.Data;

namespace VkTestTask.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await Task.Delay(5000);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            entity.UserState = await GetUserStateEntityAsync(UserStateCode.Blocked);
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetAsync(Guid id)
        => await _context.Users
            .Include(x => x.UserGroupId)
            .Include(x => x.UserStateId)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User?> GetByLoginAsync(string login)
        => await _context.Users
            .Include(x => x.UserGroup)
            .Include(x => x.UserState)
            .FirstOrDefaultAsync(x => x.Login == login);

        public async Task<IEnumerable<User>> GetAllAsync()
        => await _context.Users
            .Include(x => x.UserGroup)
            .Include(x => x.UserState)
            .ToArrayAsync();

        public async Task<IEnumerable<User>> GetFewAsync(int count, int offset)
        => await _context.Users
            .Skip(offset)
            .Take(count)
            .Include(x => x.UserGroup)
            .Include(x => x.UserState)
            .ToArrayAsync();

        public async Task<bool> IsLoginAreAvailableAsync(string login)
        => !await _context.Users.AnyAsync(x => x.Login == login);
        

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task<bool> AdminAlreadyExistAsync() 
        => await _context.Users
            .Include(x => x.UserGroup)
            .AnyAsync(x => x.UserGroup.Code == UserGroupCode.Admin);

        public async Task<UserState> GetUserStateEntityAsync(UserStateCode code)
        => await _context.UserStates.FirstAsync(x => x.Code == code);

        public async Task<UserGroup> GetUserGroupEntityAsync(UserGroupCode code)
        => await _context.UserGroups.FirstAsync(x => x.Code == code);
    }
}

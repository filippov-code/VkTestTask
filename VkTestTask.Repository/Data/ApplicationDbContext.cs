using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkTestTask.Domain.Models;

namespace VkTestTask.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserState> UserStates { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=*dbname*;Username=postgres;Password=*password*");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>().HasData(
                new UserGroup { Id = Guid.NewGuid(), Code = UserGroupCode.User, Description = "Пользователь" },
                new UserGroup { Id = Guid.NewGuid(), Code = UserGroupCode.Admin, Description = "Администратор" }
                );

            modelBuilder.Entity<UserState>().HasData(
                new UserState { Id = Guid.NewGuid(), Code = UserStateCode.Active, Description = "Активный" },
                new UserState { Id = Guid.NewGuid(), Code = UserStateCode.Blocked, Description = "Заблокированный" }
                );

        }
    }
}

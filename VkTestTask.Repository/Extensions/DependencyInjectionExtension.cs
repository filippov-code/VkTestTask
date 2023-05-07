using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkTestTask.Domain.Interfaces;
using VkTestTask.Repository.Data;
using VkTestTask.Repository.Repositories;

namespace VkTestTask.Repository.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddDbContext<ApplicationDbContext>();

            return services;
        }
    }
}

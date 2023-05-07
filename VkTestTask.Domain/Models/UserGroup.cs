using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkTestTask.Domain.Models
{
    public class UserGroup
    {
        public Guid Id { get; set; }
        public UserGroupCode Code { get; set; }
        public string Description { get; set; }
    }

    public enum UserGroupCode
    {
        Admin,
        User
    }
}

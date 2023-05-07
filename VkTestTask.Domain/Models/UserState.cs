using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkTestTask.Domain.Models
{
    public class UserState
    {
        public Guid Id { get; set; }
        public UserStateCode Code { get; set; }
        public string Description { get; set; }
    }

    public enum UserStateCode
    {
        Active,
        Blocked
    }
}

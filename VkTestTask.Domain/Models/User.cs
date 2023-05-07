using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkTestTask.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }
        public Guid UserStateId { get; set; }
        public UserState? UserState {get; set;}
    }
}

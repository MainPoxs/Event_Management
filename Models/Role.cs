using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class Role
    {
        public int RoleId { get; set; } = 0;
        public string? TitleRole { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
            = new List<User>();       
    }
}

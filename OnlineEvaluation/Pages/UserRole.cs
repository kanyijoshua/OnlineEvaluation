using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using OnlineEvaluation.Models;

namespace OnlineEvaluation.Pages
{
    public class UserRole
    {
        public UserRole()
        {

        }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public List<ApplicationUser> Users;
    }
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual IdentityRole Role { get; set; }
    }
}

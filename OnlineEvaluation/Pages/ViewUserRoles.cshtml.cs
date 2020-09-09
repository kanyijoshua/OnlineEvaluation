
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using OnlineEvaluation.Models;
using OnlineEvaluation.Data;

namespace OnlineEvaluation.Pages
{
    [Authorize(Roles ="Admin")]
    public class ViewUserRolesModel : PageModel
    {
        public ApplicationDbContext _context { get; }
        public UserManager<ApplicationUser> UserManagers { get; }
        public RoleManager<IdentityRole> RoleManagers { get; }

        public ViewUserRolesModel(ApplicationDbContext context, UserManager<ApplicationUser> userManagers, RoleManager<IdentityRole> roleManagers)
        {
            _context = context;
            UserManagers = userManagers;
            RoleManagers = roleManagers;
        }
        [BindProperty(SupportsGet = true)]
        public string RoleId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string flashMessage { get; set; }
        public string RoleName { get; set; }
        public List<ApplicationUser> Users;

        public void OnGet()
        {
            var rolename = RoleManagers.FindByIdAsync(RoleId).Result.Name;
            Users = UserManagers.GetUsersInRoleAsync(rolename).Result.ToList();
            RoleName = rolename;
        }
        public IActionResult OnGetAddUserRole(string roleId)
        {
            var rolename = RoleManagers.FindByIdAsync(RoleId).Result.Name;
            var Usersinrole = UserManagers.GetUsersInRoleAsync(rolename).Result.ToList();
            var UserList = _context.Users.ToList();
            var Usersnotinrole = UserList.Except(Usersinrole).ToList();
            var Usersrole = new UserRole();
            Usersrole.RoleId = roleId;
            Usersrole.Users = Usersnotinrole;
            return Partial("_adduserole", Usersrole);
        }
        public IActionResult OnGetAddUserToRole(string userId)
        {
            var UsersId = UserManagers.FindByIdAsync(userId).Result;
            var rolename = RoleManagers.FindByIdAsync(RoleId).Result.Name;
            var result1 = UserManagers.AddToRoleAsync(UsersId, rolename).Result;
            if (result1.Succeeded)
            {
                flashMessage = "User added to role";
            }
            else
            {
                flashMessage = "Error adding user to role";
            }
            return RedirectToPage("ViewUserRoles", new { RoleId=RoleId, flashMessage = flashMessage });
        }
        public IActionResult OnGetDeleteUserRole(string userId)
        {
            var UsersId = UserManagers.FindByIdAsync(userId).Result;
            var rolename = RoleManagers.FindByIdAsync(RoleId).Result.Name;
            if (UsersId.Email == "admin@gmail.com" && rolename == "Admin")
            {
                return RedirectToPage("ViewUserRoles", new { RoleId = RoleId, flashMessage = flashMessage });
            }
            else
            {
                var result1 = UserManagers.RemoveFromRoleAsync(UsersId, rolename).Result;
                if (result1.Succeeded)
                {
                    flashMessage = "User removed from role";
                }
                else
                {
                    flashMessage = "Error removing user from role";
                }
            }
            return RedirectToPage("ViewUserRoles", new { RoleId = RoleId, flashMessage = flashMessage });
        }
    }
}
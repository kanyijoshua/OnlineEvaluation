using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineEvaluation.Models;

namespace WorkloadApp.Pages
{
    [Authorize(Roles = "Admin")]
    public class RolesModel : PageModel
    {
        public UserManager<ApplicationUser> userManager;
        public RoleManager<IdentityRole> roleManager;
        public RolesModel(UserManager<ApplicationUser> userManagers, RoleManager<IdentityRole> roleManagers)
        {
            userManager = userManagers;
            roleManager = roleManagers;
        }
        public class RoleViewModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        [BindProperty]
        public List<RoleViewModel> RoleViewModels { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string roleId)
        {
            var model = new List<RoleViewModel>();
            var roles = roleManager.Roles.Select(x => new { Id = x.Id, Name = x.Name });
            foreach (var item in roles)
            {
                model.Add(new RoleViewModel() { Id = item.Id, Name = item.Name });
            }

            RoleViewModels = model;

            return Page();
        }
        public IActionResult OnGetUsers(string RoleId)
        {
            return RedirectToPage("/ViewUserRoles",new { RoleId });
        }
    }
}

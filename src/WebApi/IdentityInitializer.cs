using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApi {
    public class IdentityInitializer {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public IdentityInitializer(RoleManager<IdentityRole> roleManager, UserManager<User> userManager) {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task AddDefaultRoles() {
            // TODO: error and exception handling
            var roleNames = new List<string>() { "Admin", "Contributer", "Viewer" };

            if (!_roleManager.Roles.Any()) {
                foreach (var name in roleNames) {
                    var role = new IdentityRole(name);
                    await _roleManager.CreateAsync(role);
                }
            }
        }

        public async Task AddDefaultAdminUser(string email, string password) {
            // TODO: error and exception handling
            var adminUser = await _userManager.FindByEmailAsync(email);
            if (adminUser == null) {
                adminUser = new User() {
                    UserName = email,
                    Email = email
                };
                var result = await _userManager.CreateAsync(adminUser, password);
                if (result.Succeeded) {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}

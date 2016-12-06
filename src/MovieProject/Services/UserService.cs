using Entities.Models.Requests;
using Entities.Models.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MovieProject.Models;
using MovieProject.Services.Interfaces;
using Repositories.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ICollection<UserResponse>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();

            return await ToUserViewModelAsync(users);
        }

        public async Task<IEnumerable<RoleResponse>> DeleteRoleAsync(RoleUserRequest roleUserRequest)
        {
            var user = await _userManager
                .FindByIdAsync(roleUserRequest.UserId);

            var role = await _roleManager
                .FindByNameAsync(roleUserRequest.RoleName);

            await _userManager.RemoveFromRoleAsync(user, role.Name);

            var roles = await _userManager.GetRolesAsync(user);
            return ToRoleViewModels(roles);
        }

        public async Task<IEnumerable<RoleResponse>> AddRoleAsync(RoleUserRequest roleUserRequest)
        {
            var user = await _userManager
                .FindByIdAsync(roleUserRequest.UserId);

            var role = await _roleManager
                .FindByNameAsync(roleUserRequest.RoleName);

            await _userManager.AddToRoleAsync(user, role.Name);

            var roles = await _userManager.GetRolesAsync(user);
            return ToRoleViewModels(roles);
        }

        public async Task<IEnumerable<RoleResponse>> GetRemainingRolesAsync(UserRequest userRequest)
        {
            var allRoles = _roleManager.Roles;
            var user = await _userManager.FindByIdAsync(userRequest.UserId);

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Count() == 0)
                return ToRoleViewModels(allRoles.Select(role => role.Name));

            var roles = allRoles
                .Where(role => userRoles.All(userRole => userRole != role.Name))
                .Select(role => role.Name);

            return ToRoleViewModels(roles);
        }

        private async Task<ICollection<UserResponse>> ToUserViewModelAsync(IEnumerable<ApplicationUser> users)
        {
            var usersViewModelList = new List<UserResponse>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                usersViewModelList.Add(new UserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = ToRoleViewModels(userRoles)
                });
            }

            return usersViewModelList;
        }

        private static IEnumerable<RoleResponse> ToRoleViewModels(IEnumerable<string> roles)
        {
            return roles.Select(role => new RoleResponse
            {
                Name = role
            });
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieProject.Services.Interfaces;
using Entities.Models.Responses;
using Entities.Models.Requests;

namespace MovieProject.Controllers.WebApi
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserResponse>> GetUsers()
        {
            return await _userService.GetUsersAsync();
        }

        [HttpPost]
        public async Task<IEnumerable<RoleResponse>> AddRole([FromBody] RoleUserRequest request)
        {
            return await _userService.AddRoleAsync(request);
        }

        [HttpPost]
        public async Task<IEnumerable<RoleResponse>> DeleteRole([FromBody] RoleUserRequest request)
        {
            return await _userService.DeleteRoleAsync(request);
        }

        [HttpPost]
        public async Task<IEnumerable<RoleResponse>> GetRemainingRoles([FromBody] UserRequest userRequest) 
        {
            return await _userService.GetRemainingRolesAsync(userRequest);
        }
    }
}

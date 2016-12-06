using Entities.Models.Requests;
using Entities.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<UserResponse>> GetUsersAsync();
        Task<IEnumerable<RoleResponse>> DeleteRoleAsync(RoleUserRequest roleUserRequest);
        Task<IEnumerable<RoleResponse>> AddRoleAsync(RoleUserRequest roleUserRequest);
        Task<IEnumerable<RoleResponse>> GetRemainingRolesAsync(UserRequest userRequest);
        Task<ICollection<UserResponse>> BlockUserAsync(UserRequest userRequest);
    }
}

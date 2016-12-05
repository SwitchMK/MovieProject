using System.Collections.Generic;

namespace Entities.Models.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<RoleResponse> Roles { get; set; }
    }
}

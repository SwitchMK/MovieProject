using System.Threading.Tasks;
using Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MovieProject.Models;
using System.Linq;

namespace Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<ApplicationUser> _userDataSet;

        public UserRepository(DbContext context)
        {
            _userDataSet = context.Set<ApplicationUser>();
        }

        public async Task<ApplicationUser[]> GetUsersAsync()
        {
            return await _userDataSet.Where(p => !p.IsBlock).ToArrayAsync();
        }
    }
}

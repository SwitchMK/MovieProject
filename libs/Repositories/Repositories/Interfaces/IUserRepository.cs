using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MovieProject.Models;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser[]> GetUsersAsync();
    }
}

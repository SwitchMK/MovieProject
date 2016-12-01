using Entities.Entities;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IDistributorRepository
    {
        Task<Distributor> GetDistributorAsync(long filmId);
    }
}

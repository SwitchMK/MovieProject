using Entities.Entities;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IPeopleRepository
    {
        Task<People[]> GetPeopleAsync();
        Task<People> GetPersonAsync(long peopleId);
    }
}

using Entities.Entities;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IPeopleCareerRepository
    {
        Task<PeopleCareer[]> GetPeopleCareersAsync(long personId);
    }
}

using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.Interfaces;
using System.Linq;

namespace Repositories.Repositories
{
    public class PeopleCareerRepository : IPeopleCareerRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<PeopleCareer> _peopleCareerDataSet;

        public PeopleCareerRepository(DbContext context)
        {
            _context = context;
            _peopleCareerDataSet = context.Set<PeopleCareer>();
        }

        public Task<PeopleCareer[]> GetPeopleCareersAsync(long personId)
        {
            return _peopleCareerDataSet
                .Where(p => p.PersonId == personId)
                .Include(p => p.Career)
                .ToArrayAsync();
        }
    }
}

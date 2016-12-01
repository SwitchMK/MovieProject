using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.Interfaces;
using System.Linq;

namespace Repositories.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<People> _peopleDataSet;

        public PeopleRepository(DbContext context)
        {
            _context = context;
            _peopleDataSet = context.Set<People>();
        }

        public async Task<People[]> GetPeopleAsync()
        {
            return await _peopleDataSet.ToArrayAsync();
        }

        public Task<People> GetPersonAsync(long peopleId)
        {
            return _peopleDataSet
                .Where(p => p.Id == peopleId)
                .Include(p => p.Films)
                .Include(p => p.Careers)
                .Include(p => p.Country)
                .FirstOrDefaultAsync();
        }
    }
}

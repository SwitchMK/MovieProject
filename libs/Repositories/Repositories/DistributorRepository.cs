using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class DistributorRepository : IDistributorRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Distributor> _distributorDataSet;

        public DistributorRepository(DbContext context)
        {
            _context = context;
            _distributorDataSet = context.Set<Distributor>();
        }

        public async Task<Distributor> GetDistributorAsync(long distributorId)
        {
            return await _distributorDataSet.FirstOrDefaultAsync(p => p.Id == distributorId);
        }
    }
}

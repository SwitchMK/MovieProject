using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class TheatreRepository : ITheatreRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Theatre> _theatreDataSet;

        public TheatreRepository(DbContext context)
        {
            _context = context;
            _theatreDataSet = context.Set<Theatre>();
        }

        public async Task<Theatre[]> GetTheatresAsync()
        {
            return await _theatreDataSet.ToArrayAsync();
        }
    }
}

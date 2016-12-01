using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.Interfaces;
using System.Linq;

namespace Repositories.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Feedback> _feedbackDataSet;

        public FeedbackRepository(DbContext context)
        {
            _context = context;
            _feedbackDataSet = context.Set<Feedback>();
        }

        public async Task SaveFeedbackAsync(Feedback feedback)
        {
            _feedbackDataSet.Add(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task<Feedback[]> GetFeedbacksAsync(long filmId)
        {
            return await _feedbackDataSet.Where(p => p.FilmId == filmId)
                .Include(p => p.User)
                .OrderByDescending(p => p.DateOfPublication)
                .ToArrayAsync();
        }
    }
}

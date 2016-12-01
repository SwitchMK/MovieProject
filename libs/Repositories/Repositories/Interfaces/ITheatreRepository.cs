using Entities.Entities;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface ITheatreRepository
    {
        Task<Theatre[]> GetTheatresAsync();
    }
}

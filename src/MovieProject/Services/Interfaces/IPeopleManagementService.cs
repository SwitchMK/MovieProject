using Entities.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieProject.Services.Interfaces
{
    public interface IPeopleManagementService
    {
        Task<IEnumerable<PeopleListItemResponse>> GetPeopleAsync();
        Task<FullPeopleDetailsResponse> GetPersonDetailsAsync(long personId);
    }
}

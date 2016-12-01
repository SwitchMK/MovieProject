using Entities.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieProject.Controllers.WebApi
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class PeopleController : Controller
    {
        private readonly IPeopleManagementService _peopleManagementService;

        public PeopleController(IPeopleManagementService peopleManagementService)
        {
            _peopleManagementService = peopleManagementService;
        }

        [HttpGet]
        public async Task<IEnumerable<PeopleListItemResponse>> GetPeople()
        {
            return await _peopleManagementService.GetPeopleAsync();
        }

        [HttpPost]
        public async Task<FullPeopleDetailsResponse> GetPersonDetails([FromBody] long personId)
        {
            return await _peopleManagementService.GetPersonDetailsAsync(personId);
        }
    }
}

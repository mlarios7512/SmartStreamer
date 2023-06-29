using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamBudget.Models;
using StreamBudget.Services.Abstract;
using StreamBudget.DAL.Abstract;

namespace StreamBudget.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistInfoController : ControllerBase
    {
        private readonly IStreamAvailService _streamAvailService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<Person> _personRepo;

        public WatchlistInfoController(IStreamAvailService streamAvailService, UserManager<IdentityUser> userManager, IRepository<Person> personRepository)
        {
            _streamAvailService = streamAvailService;
            _userManager = userManager;
            _personRepo = personRepository;
        }

        //[HttpPost("remove/series/{imdbId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<> RemoveSeriesFromWatchlist(string imdbId) 
        //{

        //}

    }
}

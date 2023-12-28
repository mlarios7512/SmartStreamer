using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamBudget.Models;
using StreamBudget.Services.Abstract;
using StreamBudget.DAL.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using StreamBudget.Models.DTO;

namespace StreamBudget.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistInfoController : ControllerBase
    {
        private readonly IStreamAvailService _streamAvailService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPersonRepository _personRepo;
        private readonly IWatchlistItemRepository _watchlistItemRepository;
        private readonly IWatchlistRepository _watchlistRepository;
        

        public WatchlistInfoController(IStreamAvailService streamAvailService, 
            UserManager<IdentityUser> userManager, 
            IPersonRepository personRepository, 
            IWatchlistItemRepository watchlistItemRepository,
            IWatchlistRepository watchlistRepository)
        {
            _streamAvailService = streamAvailService;
            _userManager = userManager;
            _personRepo = personRepository;
            _watchlistItemRepository = watchlistItemRepository;
            _watchlistRepository = watchlistRepository;
        }

        [HttpPost("add/series")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult AddSeriesToWatchlist([Bind("CurWatchlistId , Title, ImdbId, FirstYear, Runtime, TotalEpisodeCount")] NewWatchlistItemDTO newWatchlistItemInfo) 
        {
            if (ModelState.IsValid)
            {
                string aspId = _userManager.GetUserId(User);
                Person curUser = _personRepo.FindPersonByAspId(aspId);

                if(_watchlistRepository.DoesUserOwnWatchlist(curUser.Id, newWatchlistItemInfo.CurWatchlistId) == true)
                {
                    if (_watchlistItemRepository.DoesItemAlreadyExistInWatchlist(newWatchlistItemInfo.ImdbId, newWatchlistItemInfo.CurWatchlistId) == true)
                    {
                        return Ok("preexisting entry");
                    }

                    WatchlistItem newEntry = new WatchlistItem();
                    newEntry.Title = newWatchlistItemInfo.Title;
                    newEntry.ImdbId = newWatchlistItemInfo.ImdbId;
                    newEntry.FirstAirYear = newWatchlistItemInfo.FirstYear;
                    newEntry.EpisodeRuntime = newWatchlistItemInfo.Runtime;
                    newEntry.TotalEpisodeCount = newWatchlistItemInfo.TotalEpisodeCount;
                    newEntry.WatchlistId = newWatchlistItemInfo.CurWatchlistId;

                    _watchlistItemRepository.AddOrUpdate(newEntry);
                    return Ok(newWatchlistItemInfo);
                }
                return NotFound();
            }

            return BadRequest();   
        }

        [HttpPost("remove/series/{imdbId}/{watchlistId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> RemoveSeriesFromWatchlist(string imdbId, int watchlistId)
        {
            try 
            {
                string aspId = _userManager.GetUserId(User);
                Person curUser = _personRepo.FindPersonByAspId(aspId);

                if (_watchlistRepository.DoesUserOwnWatchlist(curUser.Id, watchlistId) == false) 
                {
                    return BadRequest();
                }

                int curwatchlistId = _watchlistRepository.FindById(watchlistId).Id;
                _watchlistItemRepository.DeleteWatchlistItemBySeriesId(curwatchlistId, imdbId);
                return Ok(imdbId);
            }
            catch (NullReferenceException)
            {
                return BadRequest();
            }
            
        }

    }
}

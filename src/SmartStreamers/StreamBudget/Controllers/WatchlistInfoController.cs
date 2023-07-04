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
        public ActionResult AddSeriesToWatchlist([Bind("CurWatchlistId , TitleSTA, ImdbIdSTA, FirstYearSTA, RuntimeSTA, TotalEpisodeCountSTA")] WatchlistItemDTO newWatchlistItemInfo) 
        {
            if (ModelState.IsValid)
            {
                string aspId = _userManager.GetUserId(User);
                Person curUser = _personRepo.FindPersonByAspId(aspId);

                if(_watchlistRepository.DoesUserOwnWatchlist(curUser.Id, newWatchlistItemInfo.CurWatchlistId) == true)
                {
                   

                    if (_watchlistItemRepository.DoesItemAlreadyExistInWatchlist(newWatchlistItemInfo.ImdbIdSTA, newWatchlistItemInfo.CurWatchlistId) == true)
                    {
                        newWatchlistItemInfo.RuntimeSTA = -304;
                        return Ok(newWatchlistItemInfo);
                    }

                    WatchlistItem newEntry = new WatchlistItem();
                    newEntry.Title = newWatchlistItemInfo.TitleSTA;
                    newEntry.ImdbId = newWatchlistItemInfo.ImdbIdSTA;
                    newEntry.FirstAirYear = newWatchlistItemInfo.FirstYearSTA;
                    newEntry.EpisodeRuntime = newWatchlistItemInfo.RuntimeSTA;
                    newEntry.TotalEpisodeCount = newWatchlistItemInfo.TotalEpisodeCountSTA;
                    newEntry.WatchlistId = newWatchlistItemInfo.CurWatchlistId;

                    _watchlistItemRepository.AddOrUpdate(newEntry);

                    return Ok(newWatchlistItemInfo);
                }
                
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
                    return BadRequest("tt-ERROR-DELETION");
                }

                int curwatchlistId = _watchlistRepository.FindById(watchlistId).Id;
                _watchlistItemRepository.DeleteWatchlistItemBySeriesId(curwatchlistId, imdbId);
                return Ok(imdbId);
            }
            catch (NullReferenceException)
            {
                return BadRequest("tt-ERROR-DELETION");
            }
            
        }

    }
}

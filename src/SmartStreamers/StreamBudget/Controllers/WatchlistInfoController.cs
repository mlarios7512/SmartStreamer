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
        private readonly IRepository<Watchlist> _watchlistRepository;
        

        public WatchlistInfoController(IStreamAvailService streamAvailService, 
            UserManager<IdentityUser> userManager, 
            IPersonRepository personRepository, 
            IWatchlistItemRepository watchlistItemRepository,
            IRepository<Watchlist> watchlistRepository)
        {
            _streamAvailService = streamAvailService;
            _userManager = userManager;
            _personRepo = personRepository;
            _watchlistItemRepository = watchlistItemRepository;
        }

        [HttpPost("add/series")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddSeriesToWatchlist([Bind("CurWatchlistId , TitleSTA, ImdbIdSTA, FirstYearSTA, RuntimeSTA, TotalEpisodeCountSTA")] WatchlistItemDTO newWatchlistItemInfo) 
        {
            if (ModelState.IsValid)
            {
                bool alreadyInDB = true;

                if (alreadyInDB == true)
                {
                    newWatchlistItemInfo.RuntimeSTA = -304;
                    return Ok(newWatchlistItemInfo);
                }
    
                return Ok(newWatchlistItemInfo);
            }

            return BadRequest();
       
            
            

         
            
        }

        [HttpPost("remove/series/{imdbId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> RemoveSeriesFromWatchlist(string imdbId, int curWatchlistId)
        {
            try 
            {
                int curwatchlistId = _watchlistRepository.FindById(curWatchlistId).Id;
                _watchlistItemRepository.DeleteWatchlistItemBySeriesId(curwatchlistId, imdbId);

                
                //_watchlistRepository.DeleteEntryBySeriesId(imdbId);
                return Ok(imdbId);
            }
            catch (NullReferenceException)  //IDK WHAT EXCEPTION THIS WOULD BE.
            {
                return BadRequest("tt-ERROR-DELETION");
            }
            
        }

    }
}

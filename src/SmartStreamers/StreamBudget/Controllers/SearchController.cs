using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StreamBudget.DAL.Abstract;
using StreamBudget.Models;
using StreamBudget.Models.DTO.StreamAvail;
using StreamBudget.Models.Other;
using StreamBudget.Services.Abstract;
using StreamBudget.ViewModels;
using System.Text.RegularExpressions;

namespace StreamBudget.Controllers
{
    public class SearchController : Controller
    {
        private readonly IStreamAvailService _streamAvailService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPersonRepository _personRepository;
        private readonly IWatchlistRepository _watchlistRepository;

        public SearchController(
            IStreamAvailService streamAvailService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IPersonRepository personRepository,
            IWatchlistRepository watchlistRepository)
        {
            _streamAvailService = streamAvailService;
            _userManager = userManager;
            _signInManager = signInManager;
            _personRepository = personRepository;
            _watchlistRepository = watchlistRepository;
        }

        [Authorize]
        public async Task<IActionResult> Search(int watchlistId, string titleName)
        {
            string aspId = _userManager.GetUserId(User);
            Person curUser = _personRepository.FindPersonByAspId(aspId);
            if (_watchlistRepository.DoesUserOwnWatchlist(curUser.Id, watchlistId) == false)
            {
                return NotFound();
            }


            IEnumerable<SearchResultDTO> searchResults = Enumerable.Empty<SearchResultDTO>();
            if (!titleName.IsNullOrEmpty()) 
            {
                Regex regex = new Regex(@"^[a-zA-Z \w\d:;!]{1,100}$");
                if (regex.IsMatch(titleName)) 
                {
                    searchResults = await _streamAvailService.GetBasicSearch(titleName);
                }
            }

            SeriesSearchVM searchVM = new SeriesSearchVM() 
            {
                SearchResults = searchResults,
                WatchlistId = watchlistId
            };            

            SeriesWatchtimeEstimate seriesCompletionTime = null;
           
            foreach (var item in searchVM.SearchResults)
            {
                seriesCompletionTime = new SeriesWatchtimeEstimate("full series" ,item.EpisodeCount, item.Runtime);
                
                if(item.SeasonDetails.Count > 0) 
                {
                    SeasonWatchtimeEstimate seasonWatchtime = null;
                    seriesCompletionTime.SeasonWatchtimes = new List<SeasonWatchtimeEstimate>();
                    foreach (var season in item.SeasonDetails)
                    {
                        seasonWatchtime = new SeasonWatchtimeEstimate(season.OfficialName, season.EpisodeCount, item.Runtime);
                        seriesCompletionTime.SeasonWatchtimes.Add(seasonWatchtime);
                    }
                }
                searchVM.CompletionTimes.Add(seriesCompletionTime);
            }
    
            return View(searchVM);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamBudget.DAL.Abstract;
using StreamBudget.Models;
using StreamBudget.Models.DTO.StreamAvail;
using StreamBudget.Models.Other;
using StreamBudget.ViewModels;

namespace StreamBudget.Controllers
{
    public class WatchlistController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPersonRepository _personRepository;
        private readonly IWatchlistItemRepository _watchlistItemRepository;
        private readonly IWatchlistRepository _watchlistRepository;
        

        public WatchlistController(UserManager<IdentityUser> userManager, 
            IPersonRepository personRepository,
             IWatchlistItemRepository watchlistItemRepository,
            IWatchlistRepository watchlistRepository) 
        {
            _personRepository = personRepository;
            _userManager = userManager;
            _watchlistItemRepository = watchlistItemRepository;
            _watchlistRepository = watchlistRepository;
        }

        [Authorize]
        public IActionResult ViewWatchlists()
        {
            string aspId = _userManager.GetUserId(User);
            int normalId = _personRepository.FindPersonByAspId(aspId).Id;
            IEnumerable<Watchlist> userWatchlists = _watchlistRepository.GetAllWatchlistsForUser(normalId);


            return View(userWatchlists);
        }

        [Authorize]
        public IActionResult ViewWatchlistItems(int watchlistId)
        {
            string aspId = _userManager.GetUserId(User);
            Person curUser = _personRepository.FindPersonByAspId(aspId);

            if (_watchlistRepository.DoesUserOwnWatchlist(curUser.Id, watchlistId) == false)
            {
                return RedirectToAction("Index", "Home");
            }

            List<WatchlistItem> itemsInWatchlist = _watchlistItemRepository.GetWatchlistItemByWatchlistId(watchlistId);

            WatchlistVM watchlistDisplay = new WatchlistVM();
            watchlistDisplay.WatchlistId = watchlistId;
            watchlistDisplay.WatchlistItems = itemsInWatchlist.AsEnumerable();
            watchlistDisplay.WatchlistPlatformPrice = _watchlistRepository.FindById(watchlistId).SelectedStreamingCost;

            SeriesWatchtimeEstimate seriesCompletionTime = null;
            foreach (var item in itemsInWatchlist)
            {
                seriesCompletionTime = new SeriesWatchtimeEstimate("full series", item.TotalEpisodeCount, item.EpisodeRuntime);
                watchlistDisplay.CompletionTimes.Add(seriesCompletionTime);
            }

            return View(watchlistDisplay);
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateWatchlist() 
        {
            Watchlist watchlist = new Watchlist();
            return View(watchlist);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateWatchlist(Watchlist newWatchlist) 
        {
            string aspId = _userManager.GetUserId(User);
            Person curUser = _personRepository.FindPersonByAspId(aspId);
            newWatchlist.OwnerId = curUser.Id;
            ModelState.ClearValidationState("Owner");
            newWatchlist.Owner = _personRepository.FindPersonByAspId(aspId);
            TryValidateModel(newWatchlist);
            if (!ModelState.IsValid) 
            {
                newWatchlist.OwnerId = 0;
                return View(newWatchlist);
            }

            Watchlist watchlistToAdd = new Watchlist();
            watchlistToAdd.Name = newWatchlist.Name;
            watchlistToAdd.OwnerId = curUser.Id;
            watchlistToAdd.StreamingPlatform = newWatchlist.StreamingPlatform;
            watchlistToAdd.SelectedStreamingCost = newWatchlist.SelectedStreamingCost;

            _watchlistRepository.AddOrUpdate(watchlistToAdd);

            return RedirectToAction("ViewWatchlists", "Watchlist");
        }
    }
}

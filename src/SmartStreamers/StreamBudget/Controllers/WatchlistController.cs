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
        private readonly IWatchlistRepository _watchlistRepository;

        public WatchlistController(UserManager<IdentityUser> userManager, 
            IPersonRepository personRepository, 
            IWatchlistRepository watchlistRepository) 
        {
            _personRepository = personRepository;
            _userManager = userManager;
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
        public IActionResult ViewWatchlistItems()
        {
            string aspId = _userManager.GetUserId(User);
            Person curUser = _personRepository.FindPersonByAspId(aspId);

            IList<SearchResultDTO> someResults = new List<SearchResultDTO>{
                new SearchResultDTO
                {
                    Type = "show",
                    Title = "Psycho Pass",
                    Overview = "Psycho-Pass is set in a futuristic era in Japan where ...",
                    FirstAirYear = 2012,
                    LastAirYear = 2019,
                    ImdbRating = 82,
                    BackdropURL = "https://image.tmdb.org/t/p/original/2HtnTJLs3CDUTu6ug8rib5vNnU2.jpg",
                    AdvisedMinimumAudienceAge = 16,
                    ImdbId = "tt2379308",
                    Runtime = 28,
                    EpisodeCount = 41,
                    SeasonCount = 3,
                    StreamingInfo = new List<StreamingPlatformDTO>
                    {
                        new StreamingPlatformDTO
                        {
                            PlatformName = "HBO",
                            AvailableOnSubscription = false,
                        },
                        new StreamingPlatformDTO
                        {
                            PlatformName = "Hulu",
                            AvailableOnSubscription = true,
                        },
                    }
                },
                new SearchResultDTO
                {
                    Type = "show",
                    Title = "Batman: The Animated Series",
                    Overview = "Vowing to avenge the murder of his parents ...",
                    FirstAirYear = 1992,
                    LastAirYear = 1995,
                    ImdbRating = 90,
                    BackdropURL = "https://image.tmdb.org/t/p/original/2Eib5rvQXl2TTp4GBLgTnvAQNUL.jpg",
                    AdvisedMinimumAudienceAge = 6,
                    ImdbId = "tt0103359",
                    Runtime = 22,
                    EpisodeCount = 85,
                    SeasonCount = 4,
                    StreamingInfo = new List<StreamingPlatformDTO>
                    {
                        new StreamingPlatformDTO
                        {
                            PlatformName = "HBO",
                            AvailableOnSubscription = true,
                        },
                        new StreamingPlatformDTO
                        {
                            PlatformName = "Disney",
                            AvailableOnSubscription = false,
                        }
                    }
                },


                //new SearchResultDTO
                //{
                //    Type = "show",
                //    Title = "Dude in Cape",
                //    Overview = "What the title says ...",
                //    FirstAirYear = 1992,
                //    LastAirYear = 1995,
                //    ImdbRating = 90,
                //    BackdropURL = "https://image.tmdb.org/t/p/original/2Eib5rvQXl2TTp4GBLgTnvAQNUL.jpg",
                //    AdvisedMinimumAudienceAge = 6,
                //    ImdbId = "tt0103359",
                //    Runtime = 60,
                //    EpisodeCount = 1480,
                //    SeasonCount = 4,
                //    StreamingInfo = new List<StreamingPlatformDTO>
                //    {
                //        new StreamingPlatformDTO
                //        {
                //            PlatformName = "HBO",
                //            AvailableOnSubscription = true,
                //        },
                //        new StreamingPlatformDTO
                //        {
                //            PlatformName = "Disney",
                //            AvailableOnSubscription = false,
                //        }
                //    }
                //},
                new SearchResultDTO
                {
                    Type = "show",
                    Title = "Spider-man",
                    Overview = "Bitten by a radioactive spider, Peter Parker...",
                    FirstAirYear = 1994,
                    LastAirYear = 1998,
                    ImdbRating = 82,
                    BackdropURL = "https://image.tmdb.org/t/p/original/cYt2vWSHMpwAg9uJ24vb4g5CC4Y.jpg",
                    AdvisedMinimumAudienceAge = 16,
                    ImdbId = "tt0112175",
                    Runtime = null, //PRETENDING THIS HAS NO RUNTIME.
                    EpisodeCount = 41,
                    SeasonCount = 3,
                    StreamingInfo = new List<StreamingPlatformDTO>
                    {
                        new StreamingPlatformDTO
                        {
                            PlatformName = "HBO",
                            AvailableOnSubscription = false,
                        },
                        new StreamingPlatformDTO
                        {
                            PlatformName = "Hulu",
                            AvailableOnSubscription = true,
                        },
                    }
                },

            };

            IEnumerable<SearchResultDTO> resultsToReturn = someResults.AsEnumerable();

            WatchlistVM watchlistDisplay = new WatchlistVM();
            watchlistDisplay.SearchResults = resultsToReturn;

            CompletionTime seriesCompletionTime = new CompletionTime();
            foreach (var item in resultsToReturn)
            {
                seriesCompletionTime = new CompletionTime(item.EpisodeCount, item.Runtime);
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

            _watchlistRepository.AddOrUpdate(watchlistToAdd);

            return RedirectToAction("ViewWatchlists", "Watchlist");
        }
    }
}

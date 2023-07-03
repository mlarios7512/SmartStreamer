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
        public IActionResult Search(int watchlistId, string titleName)
        {
            string aspId = _userManager.GetUserId(User);
            Person curUser = _personRepository.FindPersonByAspId(aspId);
            if (_watchlistRepository.DoesUserOwnWatchlist(curUser.Id, watchlistId) == false)
            {
                return NotFound();
            }


            //IEnumerable<SearchResultDTO> searchResults = await _streamAvailService.GetBasicSearch(titleName);

            //return View(searchResults);

            //---------HARD CODED to save API calls (below)-------------------

            IList<Models.DTO.StreamAvail.WatchlistItemDTO> someResults = new List<Models.DTO.StreamAvail.WatchlistItemDTO>{
                new Models.DTO.StreamAvail.WatchlistItemDTO
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
                new Models.DTO.StreamAvail.WatchlistItemDTO
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
                new Models.DTO.StreamAvail.WatchlistItemDTO
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

            IEnumerable<Models.DTO.StreamAvail.WatchlistItemDTO> resultsToReturn = someResults.AsEnumerable();

            SeriesSearchVM searchVM = new SeriesSearchVM();
            searchVM.SearchResults = resultsToReturn;
            searchVM.WatchlistId = watchlistId;
            

            CompletionTime seriesCompletionTime = new CompletionTime();
            foreach (var item in searchVM.SearchResults)
            {
                seriesCompletionTime = new CompletionTime(item.EpisodeCount, item.Runtime);
                searchVM.CompletionTimes.Add(seriesCompletionTime);
            }

            return View(searchVM);
        }


        //NOTE: To enable safe pratices of adding a series to a specific user's wathclist, all users MUST have a UNIQUE "username"???
        //For possible help, see the MCM project: "EditListenerInformation" inside the "ListenerController".

        //[HttpGet]
        //public IActionResult Search()
        //{
        //    return View();
        //}

        //public async Task<IActionResult> SearchResults(string titleName)
        //{
        //    //IEnumerable<SearchResultDTO> searchResults = await _streamAvailService.GetBasicSearch(titleName);

        //    //return View(searchResults);

        //    //---------HARD CODED to save API calls (below)-------------------

        //    IList<SearchResultDTO> someResults = new List<SearchResultDTO>{
        //        new SearchResultDTO
        //        {
        //            Type = "show",
        //            Title = "Psycho Pass",
        //            Overview = "Psycho-Pass is set in a futuristic era in Japan where ...",
        //            FirstAirYear = 2012,
        //            LastAirYear = 2019,
        //            ImdbRating = 82,
        //            BackdropURL = "https://image.tmdb.org/t/p/original/2HtnTJLs3CDUTu6ug8rib5vNnU2.jpg",
        //            AdvisedMinimumAudienceAge = 16,
        //            ImdbId = "tt2379308",
        //            Runtime = 28,
        //            EpisodeCount = 41,
        //            SeasonCount = 3,
        //            StreamingInfo = new List<StreamingPlatformDTO>
        //            {
        //                new StreamingPlatformDTO
        //                {
        //                    PlatformName = "HBO",
        //                    AvailableOnSubscription = false,
        //                },
        //                new StreamingPlatformDTO
        //                {
        //                    PlatformName = "Hulu",
        //                    AvailableOnSubscription = true,
        //                },
        //            }
        //        },
        //        new SearchResultDTO
        //        {
        //            Type = "show",
        //            Title = "Batman: The Animated Series",
        //            Overview = "Vowing to avenge the murder of his parents ...",
        //            FirstAirYear = 1992,
        //            LastAirYear = 1995,
        //            ImdbRating = 90,
        //            BackdropURL = "https://image.tmdb.org/t/p/original/2Eib5rvQXl2TTp4GBLgTnvAQNUL.jpg",
        //            AdvisedMinimumAudienceAge = 6,
        //            ImdbId = "tt0103359",
        //            Runtime = 22,
        //            EpisodeCount = 85,
        //            SeasonCount = 4,
        //            StreamingInfo = new List<StreamingPlatformDTO>
        //            {
        //                new StreamingPlatformDTO
        //                {
        //                    PlatformName = "HBO",
        //                    AvailableOnSubscription = true,
        //                },
        //                new StreamingPlatformDTO
        //                {
        //                    PlatformName = "Disney",
        //                    AvailableOnSubscription = false,
        //                }
        //            }
        //        },


        //        //new SearchResultDTO
        //        //{
        //        //    Type = "show",
        //        //    Title = "Dude in Cape",
        //        //    Overview = "What the title says ...",
        //        //    FirstAirYear = 1992,
        //        //    LastAirYear = 1995,
        //        //    ImdbRating = 90,
        //        //    BackdropURL = "https://image.tmdb.org/t/p/original/2Eib5rvQXl2TTp4GBLgTnvAQNUL.jpg",
        //        //    AdvisedMinimumAudienceAge = 6,
        //        //    ImdbId = "tt0103359",
        //        //    Runtime = 60,
        //        //    EpisodeCount = 1480,
        //        //    SeasonCount = 4,
        //        //    StreamingInfo = new List<StreamingPlatformDTO>
        //        //    {
        //        //        new StreamingPlatformDTO
        //        //        {
        //        //            PlatformName = "HBO",
        //        //            AvailableOnSubscription = true,
        //        //        },
        //        //        new StreamingPlatformDTO
        //        //        {
        //        //            PlatformName = "Disney",
        //        //            AvailableOnSubscription = false,
        //        //        }
        //        //    }
        //        //},
        //        new SearchResultDTO
        //        {
        //            Type = "show",
        //            Title = "Spider-man",
        //            Overview = "Bitten by a radioactive spider, Peter Parker...",
        //            FirstAirYear = 1994,
        //            LastAirYear = 1998,
        //            ImdbRating = 82,
        //            BackdropURL = "https://image.tmdb.org/t/p/original/cYt2vWSHMpwAg9uJ24vb4g5CC4Y.jpg",
        //            AdvisedMinimumAudienceAge = 16,
        //            ImdbId = "tt0112175",
        //            Runtime = null, //PRETENDING THIS HAS NO RUNTIME.
        //            EpisodeCount = 41,
        //            SeasonCount = 3,
        //            StreamingInfo = new List<StreamingPlatformDTO>
        //            {
        //                new StreamingPlatformDTO
        //                {
        //                    PlatformName = "HBO",
        //                    AvailableOnSubscription = false,
        //                },
        //                new StreamingPlatformDTO
        //                {
        //                    PlatformName = "Hulu",
        //                    AvailableOnSubscription = true,
        //                },
        //            }
        //        },

        //    };

        //    IEnumerable<SearchResultDTO> resultsToReturn = someResults.AsEnumerable();

        //    return View(resultsToReturn);
        //}


    }
}

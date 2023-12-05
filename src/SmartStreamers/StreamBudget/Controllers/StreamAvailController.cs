using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamBudget.Models.DTO.StreamAvail;
using StreamBudget.Services.Abstract;

namespace StreamBudget.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamAvailController : ControllerBase
    {
        private readonly IStreamAvailService _streamAvailService;

        public StreamAvailController(IStreamAvailService streamAvailService)
        {
            _streamAvailService = streamAvailService;
        }

        // REAL VER (uses API calls. Used for visitor page only.
        //[HttpGet("search/title/{titleName}")]
        //public async Task<IEnumerable<SearchResultDTO>> GetSearchResults(string titleName)
        //{
        //    return await _streamAvailService.GetBasicSearch(titleName);
        //}

        //PRACTICE VER (to save on API calls). Used for visitor page only.
        [HttpGet("search/title/{titleName}")]
        public async Task<IEnumerable<SearchResultDTO>> GetSearchResults(string titleName)
        {
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

            return someResults;
        }
    }
}

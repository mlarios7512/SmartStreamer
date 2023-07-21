using Newtonsoft.Json.Linq;

namespace StreamBudget.Models.DTO.StreamAvail
{
    public class SeasonDetailsDTO
    {
        public int? EpisodeCount { get; set; }

        public SeasonDetailsDTO() 
        {

        }

        public static List<SeasonDetailsDTO> GetSeasonDetails_FromJSON(List<JToken> allTitles, List<SearchResultDTO> mediaItems) 
        {
            if (allTitles != null)
            {
                SeasonDetailsDTO seasonDetails = null;
                foreach (var item in allTitles)
                {
                    
                    var seasonsInfo = item.SelectToken("seasons");
                    seasonDetails = new SeasonDetailsDTO();
                    if (seasonsInfo != null)
                    {
                        foreach (var season in seasonsInfo)
                        {
                            int? episodeCount = null;
                            var episodes = season.SelectToken("episodes");

                            if(episodes != null) 
                            {
                                episodeCount = 0;
                                foreach (var episode in episodes)
                                {
                                    episodeCount++;
                                }
                            }
                        }
                    }


                }
            }

            return new List<SeasonDetailsDTO>();
        }
    }
}

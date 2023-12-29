using Newtonsoft.Json.Linq;

namespace StreamBudget.Models.DTO.StreamAvail
{
    public class SeasonDetailsDTO
    {
        public string OfficialName { get; set; } = null;
        public int EpisodeCount { get; set; } = 0;

        public SeasonDetailsDTO() 
        {

        }
        public static List<SeasonDetailsDTO> Parse_GetSeasonSpecificDetails(List<JToken> allSeasonsForATvSeries)
        {
            if (allSeasonsForATvSeries != null)
            {
                List<SeasonDetailsDTO> detailsToReturn = new List<SeasonDetailsDTO>(); 
                foreach (var season in allSeasonsForATvSeries)
                {
                    var episodes = season.SelectToken("episodes");

                    SeasonDetailsDTO seasonDetails = new SeasonDetailsDTO();
                    seasonDetails.OfficialName = (string)season?.SelectToken("title");
                    if (episodes != null) 
                    {
                        seasonDetails.EpisodeCount = episodes.ToList().Count;
                    }
                    detailsToReturn.Add(seasonDetails);
                    
                }
                return detailsToReturn;

            }

            return new List<SeasonDetailsDTO>();
        }
    }
}

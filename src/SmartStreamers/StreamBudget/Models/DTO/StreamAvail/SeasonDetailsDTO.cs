using Newtonsoft.Json.Linq;

namespace StreamBudget.Models.DTO.StreamAvail
{
    public class SeasonDetailsDTO
    {
        public int EpisodeCount { get; set; } = 0;

        public SeasonDetailsDTO() 
        {

        }

        public static List<SeasonDetailsDTO> GetSeasonDetails_FromJSON(List<JToken> allTitles)
        {
            if (allTitles != null)
            {
                List<SeasonDetailsDTO> tvSeriesSeasonInfo = new List<SeasonDetailsDTO>();
                foreach (var item in allTitles)
                {

                    var seasonsInfo = item.SelectToken("seasons");
                    if (seasonsInfo != null)
                    {
                        foreach (var season in seasonsInfo)
                        {
                            SeasonDetailsDTO seasonDetails = new SeasonDetailsDTO();
                            var episodes = season.SelectToken("episodes");

                            if (episodes != null)
                            {
                                seasonDetails.EpisodeCount = 0;
                                foreach (var episode in episodes)
                                {
                                    seasonDetails.EpisodeCount++;
                                }
                            }
                            tvSeriesSeasonInfo.Add(seasonDetails);
                        }
                    }



                }
                return tvSeriesSeasonInfo;

            }

            return new List<SeasonDetailsDTO>();
        }

        //----------------------------


        //public static SeasonDetailsDTO GetSeasonDetailsSINGLE_FromJSON(List<JToken> allEpisodesForASeason)
        //{
        //    if(allEpisodesForASeason != null) 
        //    {
        //       SeasonDetailsDTO seasonDetails = new SeasonDetailsDTO();

        //        seasonDetails.EpisodeCount = allEpisodesForASeason.Count;
    

        //       return seasonDetails;
        //    }

        //    return new SeasonDetailsDTO();
        //}

        //-------


        public static List<SeasonDetailsDTO> GetSeasonDetailsSINGLE_FromJSON_V2(List<JToken> allSeasonsForATvSeries)
        {
            if (allSeasonsForATvSeries != null)
            {
                List<SeasonDetailsDTO> detailsToReturn = new List<SeasonDetailsDTO>(); 
                //--------------

                foreach (var season in allSeasonsForATvSeries)
                {

                    var episodes = season.SelectToken("episodes");

                    SeasonDetailsDTO seasonDetails = new SeasonDetailsDTO();
                    if (episodes != null) 
                    {
                        seasonDetails.EpisodeCount = episodes.ToList().Count;
                    }
                    detailsToReturn.Add(seasonDetails);
                    
                }

                return detailsToReturn;
                //--------------

            }

            return new List<SeasonDetailsDTO>();
        }
    }
}

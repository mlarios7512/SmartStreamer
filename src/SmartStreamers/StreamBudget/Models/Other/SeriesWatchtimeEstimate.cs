using StreamBudget.Models.DTO.StreamAvail;

namespace StreamBudget.Models.Other
{
    public class SeriesWatchtimeEstimate
    {
        //public int? TimeNeededToView { get; set; } = null;
        public string SeriesName { get; set; } = null;     //"season 1" or "full series". ("Full series" will need to be hard coded.)
        public int? EstimatedWatchtime { get; set; } = null;
        public List<SeasonWatchtimeEstimate> SeasonWatchtimes {get; set;} = null;

    public SeriesWatchtimeEstimate(string sectionName = "Full series", int? totalEpisodeCount = null, int? episodeRuntimeInMinutes = null) 
        {
            SeriesName = sectionName;
            if (episodeRuntimeInMinutes != null & totalEpisodeCount != null)
            {
                EstimatedWatchtime = GetWatchtimeEstimate(totalEpisodeCount, episodeRuntimeInMinutes);
            }
        }

        public int GetWatchtimeEstimate(int? episodeCount, int? approxEpisoderuntime) 
        {
            return (int)Math.Ceiling(((double)episodeCount * (double)approxEpisoderuntime) / 60); //NEED TO VERIFY THIS CALCULATION.
        }

        //public List<int?> SeasonRunTimes = null;

        //public CompletionTimes(int? episodeRuntimeInMinutes = null, int? totalEpisodeCount = null ,int? seasonCount = null, object? episodesPerSeason = null) //NOT FINAL VERSION. Just need to move on for now. 
        //{
        //    FullSeries = episodeRuntimeInMinutes * totalEpisodeCount;

        //    if(seasonCount != null) 
        //    {
        //        SeasonRunTimes = new List<int?>((int)seasonCount);
        //    }
        //    else
        //    {
        //        SeasonRunTimes = null;
        //    }
            
        //}
    }
}

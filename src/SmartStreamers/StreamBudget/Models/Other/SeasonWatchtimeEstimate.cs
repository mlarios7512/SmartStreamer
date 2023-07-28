namespace StreamBudget.Models.Other
{
    public class SeasonWatchtimeEstimate
    {
        public string SeasonName { get; set; } = null; //Some shows return something odd such as "season 1.0" as the season's name.
        public int? EstimatedWatchtime { get; set; } = null;

        public SeasonWatchtimeEstimate(string seasonName = "Unknown season", int? episodeCount = null, int? episodeRuntimeInMinutes = null)
        {
            SeasonName = seasonName;

            if(episodeCount != null && episodeRuntimeInMinutes != null) 
            {
                EstimatedWatchtime = GetWatchtimeEstimate(episodeCount, episodeRuntimeInMinutes);
            }
            
        }

        public int GetWatchtimeEstimate(int? episodeCount, int? approxEpisoderuntime)
        {
            return (int)Math.Ceiling(((double)episodeCount * (double)approxEpisoderuntime) / 60); //NEED TO VERIFY THIS CALCULATION.
        }
    }
}

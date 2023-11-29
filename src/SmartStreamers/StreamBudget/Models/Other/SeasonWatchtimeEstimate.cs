namespace StreamBudget.Models.Other
{
    public class SeasonWatchtimeEstimate
    {
        public string SeasonName { get; set; } = null; //Some shows return something odd such as "season 1.0" as the season's name.
        public int? WatchtimeInHours { get; set; } = null; //NEED TO SWAP OUT OF API.
        public int? WatchtimeInMinutes { get; set; } = null; //Need to implement.

        public SeasonWatchtimeEstimate(string seasonName = "Unknown season", int? episodeCount = null, int? episodeRuntimeInMinutes = null)
        {
            SeasonName = seasonName;

            if(episodeCount != null && episodeRuntimeInMinutes != null) 
            {
                WatchtimeInHours = GetWatchtimeEstimateInHours(episodeCount, episodeRuntimeInMinutes);
                WatchtimeInMinutes = GetWatchtimeEstimateInMinutes(episodeCount, episodeRuntimeInMinutes);
            }   
        }

        public int GetWatchtimeEstimateInHours(int? episodeCount, int? avgEpisodeRuntimeInMinutes)
        {
            return (int)Math.Ceiling(((double)episodeCount * (double)avgEpisodeRuntimeInMinutes) / 60);
        }
        public int GetWatchtimeEstimateInMinutes(int? episodeCount, int? avgEpisodeRuntimeInMinutes) 
        {
            return (int)(episodeCount * avgEpisodeRuntimeInMinutes);
        }
    }
}

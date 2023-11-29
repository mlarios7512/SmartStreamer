namespace StreamBudget.Models.Other
{
    public class SeasonWatchtimeEstimate
    {
        public string SeasonName { get; set; } = null; //Some shows return something odd such as "season 1.0" as the season's name.
        public int? WatchtimeInHours { get; set; } = null; //NEED TO SWAP OUT OF API.
        public int? WatchtimeInMinutes { get; set; } = null; //Need to implement.
        public string UserFriendlyDisplay { get; set; } = "(N/A)";

        public SeasonWatchtimeEstimate(string seasonName = "Unknown season", int? episodeCount = null, int? episodeRuntimeInMinutes = null)
        {
            SeasonName = seasonName;

            if(episodeCount != null && episodeRuntimeInMinutes != null) 
            {
                WatchtimeInHours = GetWatchtimeEstimateInHours(episodeCount, episodeRuntimeInMinutes);
                WatchtimeInMinutes = GetWatchtimeEstimateInMinutes(episodeCount, episodeRuntimeInMinutes);
                UserFriendlyDisplay = GetFriendlyWatchtimeEstimate(episodeCount, episodeRuntimeInMinutes);
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

        public string GetFriendlyWatchtimeEstimate(int? episodeCount, int? avgEpisodeRuntimeInMinutes)
        {
            if(episodeCount != null && avgEpisodeRuntimeInMinutes != null) 
            {
                double totalTime = (double)(episodeCount * avgEpisodeRuntimeInMinutes);
                string timeSpanAsString = $"{TimeSpan.FromMinutes(totalTime):hh\\:mm}".ToString();

                int hrsMinSeperatorIndex = timeSpanAsString.IndexOf(":");
                string neatDisplay = timeSpanAsString.Insert(hrsMinSeperatorIndex, "h ");
                neatDisplay = neatDisplay.Replace(":", "");
                neatDisplay = neatDisplay + "m";

                return neatDisplay;
            }
            return "(N/A)";

        }
    }
}

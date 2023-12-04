using StreamBudget.Models.DTO.StreamAvail;

namespace StreamBudget.Models.Other
{
    public class SeriesWatchtimeEstimate
    {
        public string SectionName { get; set; } = null; 
        public int? WatchtimeInMinutes { get; set; } = null;
        public string UserFriendlyDisplay { get; set; } = "(N/A)";
        public List<SeasonWatchtimeEstimate> SeasonWatchtimes {get; set;} = null;

        public SeriesWatchtimeEstimate(string sectionName = "Full series", int? totalEpisodeCount = null, int? episodeRuntimeInMinutes = null) 
        {
            SectionName = sectionName;
            if (episodeRuntimeInMinutes != null & totalEpisodeCount != null)
            {
                WatchtimeInMinutes = GetWatchtimeEstimateInMinutes(totalEpisodeCount, episodeRuntimeInMinutes);
                UserFriendlyDisplay = GetFriendlyWatchtimeEstimate(totalEpisodeCount, episodeRuntimeInMinutes);
            }
        }
        public int GetWatchtimeEstimateInMinutes(int? episodeCount, int? avgEpisodeRuntimeInMinutes) 
        {
            return (int)(episodeCount * avgEpisodeRuntimeInMinutes);
        }
        public string GetFriendlyWatchtimeEstimate(int? episodeCount, int? avgEpisodeRuntimeInMinutes)
        {
            double totalTime = (double)(episodeCount * avgEpisodeRuntimeInMinutes);
            string timeSpanAsString = $"{TimeSpan.FromMinutes(totalTime):hh\\:mm}".ToString();

            int hrsMinSeperatorIndex = timeSpanAsString.IndexOf(":");
            string neatDisplay = timeSpanAsString.Insert(hrsMinSeperatorIndex, "h ");
            neatDisplay = neatDisplay.Replace(":", "");
            neatDisplay = neatDisplay + "m";

            return neatDisplay;

        }
    }
}

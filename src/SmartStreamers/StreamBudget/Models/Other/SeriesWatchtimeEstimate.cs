using StreamBudget.Models.DTO.StreamAvail;

namespace StreamBudget.Models.Other
{
    public class SeriesWatchtimeEstimate
    {
        public string SectionName { get; set; } = null; 
        public int? WatchtimeInHours { get; set; } = null;
        public int? WatchtimeInMinutes { get; set; } = null;
        public List<SeasonWatchtimeEstimate> SeasonWatchtimes {get; set;} = null;

        public SeriesWatchtimeEstimate(string sectionName = "Full series", int? totalEpisodeCount = null, int? episodeRuntimeInMinutes = null) 
        {
            SectionName = sectionName;
            if (episodeRuntimeInMinutes != null & totalEpisodeCount != null)
            {
                WatchtimeInHours = GetWatchtimeEstimateInHours(totalEpisodeCount, episodeRuntimeInMinutes);
                WatchtimeInMinutes = GetWatchtimeEstimateInMinutes(totalEpisodeCount, episodeRuntimeInMinutes);
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

using StreamBudget.Models.DTO.StreamAvail;

namespace StreamBudget.Models.Other
{
    public class SeriesWatchtimeEstimate
    {
        public string SectionName { get; set; } = null; 
        public int? EstimatedWatchtime { get; set; } = null;
        public List<SeasonWatchtimeEstimate> SeasonWatchtimes {get; set;} = null;

    public SeriesWatchtimeEstimate(string sectionName = "Full series", int? totalEpisodeCount = null, int? episodeRuntimeInMinutes = null) 
        {
            SectionName = sectionName;
            if (episodeRuntimeInMinutes != null & totalEpisodeCount != null)
            {
                EstimatedWatchtime = GetWatchtimeEstimate(totalEpisodeCount, episodeRuntimeInMinutes);
            }
        }

        public int GetWatchtimeEstimate(int? episodeCount, int? approxEpisoderuntime) 
        {
            return (int)Math.Ceiling(((double)episodeCount * (double)approxEpisoderuntime) / 60); //NEED TO VERIFY THIS CALCULATION.
        }
    }
}

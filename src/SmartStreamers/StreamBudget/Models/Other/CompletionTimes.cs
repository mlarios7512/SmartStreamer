namespace StreamBudget.Models.Other
{
    public class CompletionTime
    {
        public int? FullSeries { get; set; } = null;

        public CompletionTime(int? totalEpisodeCount = null, int? episodeRuntimeInMinutes = null) 
        {
            if (episodeRuntimeInMinutes != null & totalEpisodeCount != null)
            {
                FullSeries = (int)Math.Ceiling(((double)totalEpisodeCount * (double)episodeRuntimeInMinutes) / 60);  //NEED TO VERIFY THIS CALCULATION.
            }

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

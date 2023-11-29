using System.Diagnostics;

namespace StreamBudget.Models.Other.SeasonInfo
{
    public class TVWatchtimeEstimate
    {
        public string SeasonName { get; set; } = "(Unknown season)";
        public int WatchtimeInMinutes { get; set; } = 0;
        public int WatchtimeInHours { get; set; } = 0;

        /// <summary>
        /// Intended to create a new watchtime estimate for a finishing an entire TV series.
        /// </summary>
        /// <param name="seasonName">Name you wish to assign (such as "Full series")</param>
        /// <param name="totalRuntimeInMinutes">Time (in minutes) expected to take to watch the entire TV series</param>
        public TVWatchtimeEstimate(string seasonName, int totalRuntimeInMinutes) 
        {
            SeasonName = seasonName;
            WatchtimeInMinutes = totalRuntimeInMinutes;

            if(totalRuntimeInMinutes > 0) 
            {
                WatchtimeInHours = totalRuntimeInMinutes / 60;
            }
            else
            {
                WatchtimeInHours = 0;
            }
        }

        /// <summary>
        /// Intended to create a new watchtime estimate for an existing season
        /// </summary>
        /// <param name="seasonName">Name of the season. Defaults to "(Unknown season)" if left blank.</param>
        /// <param name="avgEpisodeRuntime">Average runtime per episode for the TV series</param>
        /// <param name="seasonEpisodeCount">Number of episodes in the given season</param>
        public TVWatchtimeEstimate(string seasonName, int avgEpisodeRuntime, int seasonEpisodeCount)
        {
            SeasonName = seasonName;
            WatchtimeInMinutes = GetWatchtimeEstimateInMinutes(avgEpisodeRuntime, seasonEpisodeCount);
            WatchtimeInHours = GetWatchtimeEstimateInHours(avgEpisodeRuntime, seasonEpisodeCount);
        }
        /// <summary>
        /// Returns an estimate (in hours) for watching all episodes within a given season. Result is rounded up to largest integer.
        /// </summary>
        /// <param name="avgEpisodeRuntime">Average runtime (in minutes) for an episode</param>
        /// <param name="seasonEpisodeCount">Number of episodes for a specific season</param>
        /// <returns></returns>
        public int GetWatchtimeEstimateInHours(int avgEpisodeRuntime, int seasonEpisodeCount)
        {
            return (int)Math.Ceiling(((double)seasonEpisodeCount * (double)avgEpisodeRuntime) / 60);
        }
        /// <summary>
        /// Returns an estimate (in minutes) for watching all episodes within a given season.
        /// </summary>
        /// <param name="avgEpisodeRuntime">Average runtime (in minutes) for each episode.</param>
        /// <param name="seasonEpisodeCount">Total number of episodes in this season</param>
        /// <returns>An integer representing an estimated watchtime for a season</returns>
        public int GetWatchtimeEstimateInMinutes(int avgEpisodeRuntime, int seasonEpisodeCount)
        {
            return avgEpisodeRuntime * seasonEpisodeCount;
        }
    }
}

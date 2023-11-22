using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace StreamBudget.Models.DTO.StreamAvail
{
    public class SearchResultDTO
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public List<StreamingPlatformDTO> StreamingInfo { get; set; } = new List<StreamingPlatformDTO>();
        public int ImdbRating { get; set; }
        public int AdvisedMinimumAudienceAge { get; set; }
        public string ImdbId { get; set; }

        public int? Runtime { get; set; } //Will also serve as "episodeRuntime" (for a series). WARNING: API CAN return null for this.

        //---Series specific (below)---------
        public int FirstAirYear { get; set; }
        public int LastAirYear { get; set; }

        public int EpisodeCount { get; set; }
        public int SeasonCount { get; set; }
        public string BackdropURL { get; set; }

        public List<SeasonDetailsDTO> SeasonDetails { get; set; } = new List<SeasonDetailsDTO>();

        public void FromJSON_SingleSeriesDetails(object? obj)
        {
            JObject jObject = null;
            try
            {
                jObject = JObject.Parse((string)obj);
            }
            catch (JsonReaderException)
            {
                Debug.WriteLine("Error parsing JSON. (SearchResultDTO - Single Details version)");
            }
            if (jObject != null)
            {
                this.Type = (string)jObject["result"]["type"];
                this.Title = (string)jObject["result"]["title"];
                this.Overview = (string)jObject["result"]["overview"];
                this.FirstAirYear = (int)jObject["result"]["firstAirYear"];
                this.LastAirYear = (int)jObject["result"]["lastAirYear"];
                this.ImdbRating = (int)jObject["result"]["imdbRating"];
                this.BackdropURL = (string)jObject["result"]["backdropURLs"]["original"];
                this.AdvisedMinimumAudienceAge = (int)jObject["result"]["advisedMinimumAudienceAge"];
                this.ImdbId = (string)jObject["result"]["imdbId"];
                this.Runtime = (int?)jObject["result"]["episodeRuntimes"]?.FirstOrDefault();
                this.EpisodeCount = (int)jObject["result"]["episodeCount"];
                this.SeasonCount = (int)jObject["result"]["seasonCount"];


            }
        }

        public static IList<SearchResultDTO> FromJSON(object? obj)
        {
            JObject? jObject = null;
            try
            {
                jObject = JObject.Parse((string)obj);
            }
            catch (JsonReaderException)
            {
                Debug.WriteLine("Error parsing JSON. (SearchResultDTO - Enum version)");
            }
            if (jObject != null)
            {
                IEnumerable<SearchResultDTO> MediaItemsAsEnum = jObject["result"].Select(i => new SearchResultDTO()
                {
                    Type = (string)i["type"],
                    Title = (string)i["title"],
                    Overview = (string)i["overview"],
                    FirstAirYear = (int)i["firstAirYear"],
                    LastAirYear = (int)i["lastAirYear"],
                    ImdbRating = (int)i["imdbRating"],
                    BackdropURL = (string)i["backdropURLs"]["original"],
                    AdvisedMinimumAudienceAge = (int)i["advisedMinimumAudienceAge"],
                    ImdbId = (string)i["imdbId"],
                    Runtime = (int?)i["episodeRuntimes"]?.FirstOrDefault(),
                    EpisodeCount = (int)i["episodeCount"],
                    SeasonCount = (int)i["seasonCount"],
                    SeasonDetails = (List<SeasonDetailsDTO>)SeasonDetailsDTO.GetSeasonDetails_FromJSON(i.SelectToken("seasons")
                                                                                                        .ToList()
                                                                                                         )
                });

                //OLD CODE (above)------------

                //IEnumerable<SearchResultDTO> MediaItemsAsEnum = jObject["result"].Select(i => new SearchResultDTO()
                //{
                //    Type = (string)i["type"],
                //    Title = (string)i["title"],
                //    Overview = (string)i["overview"],
                //    FirstAirYear = (int)i["firstAirYear"],
                //    LastAirYear = (int)i["lastAirYear"],
                //    ImdbRating = (int)i["imdbRating"],
                //    BackdropURL = (string)i["backdropURLs"]["original"],
                //    AdvisedMinimumAudienceAge = (int)i["advisedMinimumAudienceAge"],
                //    ImdbId = (string)i["imdbId"],
                //    Runtime = (int?)i["episodeRuntimes"]?.FirstOrDefault(),
                //    EpisodeCount = (int)i["episodeCount"],
                //    SeasonCount = (int)i["seasonCount"],
                //    SeasonDetails = null
                //});

                return MediaItemsAsEnum.ToList();
            }

            return new List<SearchResultDTO>();
        }
    }
}

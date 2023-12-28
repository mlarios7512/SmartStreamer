using Newtonsoft.Json.Linq;

namespace StreamBudget.Models.DTO.StreamAvail
{
    public class StreamingPlatformDTO
    {
        public string PlatformName { get; set; } = "(???)";
        public bool? AvailableOnSubscription { get; set; } = null;

        public StreamingPlatformDTO()
        {
        }
        public enum Platform
        {
            Apple = 0,
            Disney = 1,
            HBO = 2,
            Hulu = 3,
            Netflix = 4,
            Paramount = 5,
            Prime = 6
        }

        public static void Parse_GetPlatformAvailability(List<JToken> allTitles, List<SearchResultDTO> mediaItems)
        {
            List<StreamingPlatformDTO> PlatformDetails = new List<StreamingPlatformDTO>();
            int mediaItemIndex = 0;

            StreamingPlatformDTO StreamingPlatformInfo = new StreamingPlatformDTO();

            foreach (var item in mediaItems)
            {
                List<bool?> platformAvailability = new();
                foreach (var platform in Enum.GetValues(typeof(Platform)))
                {
                    string curPlatform = platform.ToString().ToLower();
                    bool? result = allTitles.ElementAtOrDefault(mediaItemIndex)?.SelectToken("streamingInfo")?.SelectToken("us")?.SelectToken(curPlatform)?.Any(x => x.SelectToken("type")?.ToString() == "subscription");
                    platformAvailability.Add(result);
                }

                item.StreamingInfo = new List<StreamingPlatformDTO>();

                int platformAvailIValuelndex = 0;
                foreach (var platform in Enum.GetValues(typeof(Platform)))
                {
                    StreamingPlatformInfo = new StreamingPlatformDTO();
                    StreamingPlatformInfo.PlatformName = platform.ToString();
                    StreamingPlatformInfo.AvailableOnSubscription = platformAvailability.ElementAt(platformAvailIValuelndex);
                    item.StreamingInfo.Add(StreamingPlatformInfo);

                    platformAvailIValuelndex++;
                }

                mediaItemIndex++;
            }
        }

    }
}

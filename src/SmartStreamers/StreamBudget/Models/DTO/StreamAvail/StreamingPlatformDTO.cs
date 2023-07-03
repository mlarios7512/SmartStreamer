using Newtonsoft.Json.Linq;

namespace StreamBudget.Models.DTO.StreamAvail
{
    public class StreamingPlatformDTO
    {
        public string PlatformName { get; set; } = "(???)";
        public bool AvailableOnSubscription { get; set; } = false;

        public StreamingPlatformDTO()
        {

        }

        public static void GetPlatformDetails_FromJSON(List<JToken> allTitles, List<SearchResultDTO> mediaItems)
        {

            List<StreamingPlatformDTO> PlatformDetails = new List<StreamingPlatformDTO>();
            int mediaItemIndex = 0;

            StreamingPlatformDTO StreamingPlatformInfo = new StreamingPlatformDTO();


            foreach (var item in mediaItems)
            {
                bool? availableOnappleSub = allTitles.ElementAtOrDefault(mediaItemIndex)?.SelectToken("streamingInfo")?.SelectToken("us")?.SelectToken("apple")?.Any(x => x.SelectToken("type")?.ToString() == "subscription");
                bool? availableOnHboSub = allTitles.ElementAtOrDefault(mediaItemIndex)?.SelectToken("streamingInfo")?.SelectToken("us")?.SelectToken("hbo")?.Any(x => x.SelectToken("type")?.ToString() == "subscription");
                bool? availableOnHuluSub = allTitles.ElementAtOrDefault(mediaItemIndex)?.SelectToken("streamingInfo")?.SelectToken("us")?.SelectToken("hulu")?.Any(x => x.SelectToken("type")?.ToString() == "subscription"); ;
                bool? availableOnNetflixSub = allTitles.ElementAtOrDefault(mediaItemIndex)?.SelectToken("streamingInfo")?.SelectToken("us")?.SelectToken("netflix")?.Any(x => x.SelectToken("type")?.ToString() == "subscription");
                bool? availableOnParamountSub = allTitles.ElementAtOrDefault(mediaItemIndex)?.SelectToken("streamingInfo")?.SelectToken("us")?.SelectToken("paramount")?.Any(x => x.SelectToken("type")?.ToString() == "subscription");
                bool? availableOnPeacockSub = allTitles.ElementAtOrDefault(mediaItemIndex)?.SelectToken("streamingInfo")?.SelectToken("us")?.SelectToken("peacock")?.Any(x => x.SelectToken("type")?.ToString() == "subscription");
                bool? availableOnPrimeSub = allTitles.ElementAtOrDefault(mediaItemIndex)?.SelectToken("streamingInfo")?.SelectToken("us")?.SelectToken("prime")?.Any(x => x.SelectToken("type")?.ToString() == "subscription");

                item.StreamingInfo = new List<StreamingPlatformDTO>();
                StreamingPlatformInfo = new StreamingPlatformDTO();
                StreamingPlatformInfo.PlatformName = "Apple";
                if (availableOnappleSub != null)
                {
                    if (availableOnappleSub == true)
                    {
                        StreamingPlatformInfo.AvailableOnSubscription = true;
                    }
                }
                item.StreamingInfo.Add(StreamingPlatformInfo);


                //---------
                StreamingPlatformInfo = new StreamingPlatformDTO();
                StreamingPlatformInfo.PlatformName = "HBO";
                if (availableOnHboSub != null)
                {
                    if (availableOnHboSub == true)
                    {
                        StreamingPlatformInfo.AvailableOnSubscription = true;
                    }
                }
                item.StreamingInfo.Add(StreamingPlatformInfo);
                //-----
                StreamingPlatformInfo = new StreamingPlatformDTO();
                StreamingPlatformInfo.PlatformName = "Hulu";
                if (availableOnHuluSub != null)
                {
                    if (availableOnHuluSub == true)
                    {
                        StreamingPlatformInfo.AvailableOnSubscription = true;
                    }
                }
                item.StreamingInfo.Add(StreamingPlatformInfo);
                //-------
                StreamingPlatformInfo = new StreamingPlatformDTO();
                StreamingPlatformInfo.PlatformName = "Netflix";
                if (availableOnNetflixSub != null)
                {
                    if (availableOnNetflixSub == true)
                    {
                        StreamingPlatformInfo.AvailableOnSubscription = true;
                    }
                }
                item.StreamingInfo.Add(StreamingPlatformInfo);
                //-------
                StreamingPlatformInfo = new StreamingPlatformDTO();
                StreamingPlatformInfo.PlatformName = "Paramount";
                if (availableOnParamountSub != null)
                {
                    if (availableOnParamountSub == true)
                    {
                        StreamingPlatformInfo.AvailableOnSubscription = true;
                    }
                }
                item.StreamingInfo.Add(StreamingPlatformInfo);
                //--------
                StreamingPlatformInfo = new StreamingPlatformDTO();
                StreamingPlatformInfo.PlatformName = "Peacock";
                if (availableOnPeacockSub != null)
                {
                    if (availableOnPeacockSub == true)
                    {
                        StreamingPlatformInfo.AvailableOnSubscription = true;
                    }
                }
                item.StreamingInfo.Add(StreamingPlatformInfo);
                //--------
                StreamingPlatformInfo = new StreamingPlatformDTO();
                StreamingPlatformInfo.PlatformName = "Prime";
                if (availableOnPrimeSub != null) //Ensures the platform exists.
                {
                    if (availableOnPrimeSub == true) //Ensures the series is avaiable through that platform as part of the platform's "subscription".
                    {

                        StreamingPlatformInfo.AvailableOnSubscription = true;
                    }

                }
                item.StreamingInfo.Add(StreamingPlatformInfo);

                mediaItemIndex++;
            }
        }

    }
}

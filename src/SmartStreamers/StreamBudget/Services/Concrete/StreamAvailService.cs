using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using Microsoft.Net.Http.Headers;
using StreamBudget.Services.Abstract;
using System.Diagnostics;
using StreamBudget.Models.DTO.StreamAvail;



namespace StreamBudget.Services.Concrete
{
    public class StreamAvailService : IStreamAvailService
    {
        private string Key { get; set; }
        private string BaseSource { get; set; }
        public static readonly HttpClient _httpClient = new HttpClient();

        public StreamAvailService(string apiKey)
        {
            Key = apiKey;
            BaseSource = "https://streaming-availability.p.rapidapi.com/v2";
        }

        public async Task<string> GetJsonStringFromEndpointGet(string bearerToken, string uri)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri)
            {

                Headers =
                    {
                        {HeaderNames.Accept, "application/json; charset=utf-8" }
                    }
            };
            httpRequestMessage.Headers.Add("X-RapidAPI-Key", bearerToken);
            httpRequestMessage.Headers.Add("Authorization", $"Bearer {bearerToken}");


            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Debug.WriteLine($"HTTP Response code:{response.StatusCode}");
                return responseText;
            }
            else
            {
                Debug.WriteLine($"HTTP Response code:{response.StatusCode}");
                throw new HttpRequestException();
            }
        }


        public async Task<IEnumerable<SearchResultDTO>> GetBasicSearch(string titleName)
        {
            string source = BaseSource + "/search/title?title=" + titleName + "&country=us&show_type=series&output_language=en";

            string response = await GetJsonStringFromEndpointGet(Key, source);

            //1) Get basic info of search results.
            IEnumerable<SearchResultDTO> SearchResults = SearchResultDTO.FromJSON(response).AsEnumerable();

            
            JObject? jObj = JObject.Parse((string)response);
            if (jObj != null)
            {

                //2) Get avail status of TV shows for each platform.
                StreamingPlatformDTO.GetPlatformDetails_FromJSON(jObj["result"].Children().ToList(), SearchResults.ToList());



                //3) Get the number of episodes per season for each TV show.


                //Step #3 is NOT possible (according to "Daniel Bruckner" here):
                //https://stackoverflow.com/questions/807797/linq-select-an-object-and-change-some-properties-without-creating-a-new-object


                //SearchResults = (IEnumerable<SearchResultDTO>)jObj["result"].Select(i => SearchResults.ToList().ForEach(sr => sr.SeasonDetails =

                //    (List<SeasonDetailsDTO>)SeasonDetailsDTO.GetSeasonDetails_FromJSON(i.SelectToken("seasons")
                //                                                                                        .ToList()
                //                                                                                         ))
                //);
            }



            return SearchResults; //Return search results & use "StreamingPlatformDTO.GetPlatformDetails_FromJSON".
        }

        public async Task<SearchResultDTO> GetSeriesDetails(string imdbId)
        {
            string source = BaseSource + "get/basic?imdb_id=" + imdbId + "&country=us";
            source = "https://streaming-availability.p.rapidapi.com/v2/get/basic?imdb_id=" + imdbId + "&country=us";
            string response = await GetJsonStringFromEndpointGet(Key, source);
            SearchResultDTO series = new SearchResultDTO();
            series.FromJSON_SingleSeriesDetails(response);

            return series;
        }
    }
}

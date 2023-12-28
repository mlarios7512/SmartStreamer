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

        public async Task<string> GetJsonStringFromEndpoint(string bearerToken, string uri)
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
            string response = await GetJsonStringFromEndpoint(Key, source);

            IEnumerable<SearchResultDTO> SearchResults = SearchResultDTO.Parse_GetAllTVShows(response).AsEnumerable();

            JObject? jObj = JObject.Parse((string)response);
            if (jObj != null)
            {
                StreamingPlatformDTO.Parse_GetPlatformAvailability(jObj["result"].Children().ToList(), SearchResults.ToList());

                //Reminder: Find alternative way to decouple "GetSeasonSpecificDetails" method from "GetAllTVShows" in the future.
            }

            return SearchResults;
        }

        public async Task<SearchResultDTO> GetSeriesDetails(string imdbId)
        {
            string source = BaseSource + "get/basic?imdb_id=" + imdbId + "&country=us";
            source = "https://streaming-availability.p.rapidapi.com/v2/get/basic?imdb_id=" + imdbId + "&country=us";
            string response = await GetJsonStringFromEndpoint(Key, source);
            SearchResultDTO series = new SearchResultDTO();
            series.Parse_IndividualSeriesDetails(response);

            return series;
        }
    }
}

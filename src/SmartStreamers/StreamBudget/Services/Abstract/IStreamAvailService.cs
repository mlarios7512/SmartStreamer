using StreamBudget.Models.DTO.StreamAvail;

namespace StreamBudget.Services.Abstract
{
    public interface IStreamAvailService
    {
        /// <summary>
        /// Get search results for a given TV series ONLY. (No movies.
        /// Only returns TV series available in the US that have English listed
        /// as a language option.)
        /// </summary>
        /// <param name="titleName">The name of the TV series to search for.</param>
        /// <returns>A list of search results. Returns an empty list if no search results are founds.</returns>
        public Task<IEnumerable<SearchResultDTO>> GetBasicSearch(string titleName);

        /// <summary>
        /// Get details about a TV series. (Does NOT provide a list of 
        /// streaming platforms the TV series is hosted on.)
        /// </summary>
        /// <param name="imdbId">ImdbId of the show to get info about.</param>
        /// <returns>A SearchResult if the imdbID is valid. Returns a new SearchResultDTO otherwise.</returns>
        public Task<SearchResultDTO> GetSeriesDetails(string imdbId);
    }
}

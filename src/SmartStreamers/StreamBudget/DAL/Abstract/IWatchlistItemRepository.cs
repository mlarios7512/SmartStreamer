using StreamBudget.Models;

namespace StreamBudget.DAL.Abstract
{
    public interface IWatchlistItemRepository: IRepository<WatchlistItem>
    {
        /// <summary>
        /// Delete a TV series from a watchlist. (Does nothing if the given watchlist does NOT exist).
        /// </summary>
        /// <param name="watchlistId">The ID of the watchlist to remove the TV series from.</param>
        /// <param name="imdbId">The ImdbID of the TV series that will be removed from the watchlist.</param>
        public void DeleteWatchlistItemBySeriesId(int watchlistId, string imdbId);


        /// <summary>
        /// Check if a TV series is already included in a watchlist.
        /// </summary>
        /// <param name="ImdbID">ImdbID of a TV series to check for in a watchlist.</param>
        /// <param name="watchlistId">The ID of the watchlist that will be checked.</param>
        /// <returns></returns>
        public bool DoesItemAlreadyExistInWatchlist(string ImdbID, int watchlistId);


        /// <summary>
        /// Get a list of all items from a specific watchlist.
        /// </summary>
        /// <param name="watchlistId">The ID of the watchlist to get the all TV items from.</param>
        /// <returns>All items from a watchlist (if the watchlist exists. Returns a empty list otherwise.)</returns>
        List<WatchlistItem> GetWatchlistItemByWatchlistId(int watchlistId);
    }
}

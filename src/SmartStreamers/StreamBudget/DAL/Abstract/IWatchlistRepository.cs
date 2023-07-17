using StreamBudget.Models;

namespace StreamBudget.DAL.Abstract
{
    public interface IWatchlistRepository: IRepository<Watchlist>
    {
        /// <summary>
        /// Get all watchlists for a specific user.
        /// </summary>
        /// <param name="userId">The normal (NOT Identity ID) for a user.</param>
        /// <returns>All watchlists for the user with the given ID.</returns>
        IEnumerable<Watchlist> GetAllWatchlistsForUser(int userId);

        /// <summary>
        /// Check if a user owns a watchlist.
        /// </summary>
        /// <param name="userId">The normal (NOT Identity ID) for a user.</param>
        /// <param name="watchlistId">The ID of the watchlist that will be checked if the user owns.</param>
        /// <returns>True if the user is the owner of the watchlist. False otherwise.</returns>
        bool DoesUserOwnWatchlist(int userId, int watchlistId);
    }
}

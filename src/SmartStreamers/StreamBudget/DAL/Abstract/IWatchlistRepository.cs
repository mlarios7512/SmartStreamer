using StreamBudget.Models;

namespace StreamBudget.DAL.Abstract
{
    public interface IWatchlistRepository: IRepository<Watchlist>
    {
        IEnumerable<Watchlist> GetAllWatchlistsForUser(int curUserId);
        bool DoesUserOwnWatchlist(int curUserId, int watchlistId);
    }
}

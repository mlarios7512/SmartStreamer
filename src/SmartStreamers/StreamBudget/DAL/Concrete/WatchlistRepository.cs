using Microsoft.EntityFrameworkCore;
using StreamBudget.DAL.Abstract;
using StreamBudget.Models;

namespace StreamBudget.DAL.Concrete
{
    public class WatchlistRepository: Repository<Watchlist>, IWatchlistRepository
    {
        public WatchlistRepository(DbContext ctx) : base(ctx)
        {
        }

        public IEnumerable<Watchlist> GetAllWatchlistsForUser(int curUserId) 
        {
            try 
            {
                return _dbSet.Where(w => w.OwnerId == curUserId);
            }
            catch (InvalidOperationException) 
            {
                return Enumerable.Empty<Watchlist>();
            }    
        }
    }
}

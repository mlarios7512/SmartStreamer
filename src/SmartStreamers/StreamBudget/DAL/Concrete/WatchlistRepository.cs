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

        public bool DoesUserOwnWatchlist(int curUserId, int watchlistId) 
        {
            try 
            {
                Watchlist watchlist = _dbSet.SingleOrDefault(w => w.OwnerId == curUserId && w.Id == watchlistId);
                if (watchlist == null)
                {
                    return false;
                }
            }
            catch(InvalidOperationException)
            {
                return false;
            }
            
         
            return true;    
        }
    }
}

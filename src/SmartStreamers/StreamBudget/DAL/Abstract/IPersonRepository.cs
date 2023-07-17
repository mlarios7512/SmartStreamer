using StreamBudget.Models;

namespace StreamBudget.DAL.Abstract
{
    public interface IPersonRepository : IRepository<Person>
    {
        /// <summary>
        /// Get a user's information (from normal DB).
        /// </summary>
        /// <param name="aspUserId">The ASP.NET Identity ID of the user.</param>
        /// <returns>All information about the user (if the user is found. Returns null otherwise.)</returns>
        Person FindPersonByAspId(string aspUserId);

        /// <summary>
        /// Update a user's info or add them the DB.
        /// </summary>
        /// <param name="user">The user to add/who's information needs to be updated.</param>
        void AddOrUpdateListener(Person user);
    }
}

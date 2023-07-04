using StreamBudget.Models;

namespace StreamBudget.DAL.Abstract
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person FindPersonByAspId(string aspUserId);

        void AddOrUpdateListener(Person user);
    }
}

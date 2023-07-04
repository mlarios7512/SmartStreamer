using Microsoft.EntityFrameworkCore;
using StreamBudget.DAL.Abstract;
using StreamBudget.Models;

namespace StreamBudget.DAL.Concrete
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext ctx) : base(ctx)
        {
        }


        public Person FindPersonByAspId(string aspUserId)
        {
            if (aspUserId == null)
            {
                return null;
            }

            try
            {
                var person = new Person();

                person = _dbSet.First(l => l.AspnetIdentityId.Equals(aspUserId));
                return person;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public void AddOrUpdateListener(Person person)
        {
            _dbSet.Update(person);
        }
    }
}

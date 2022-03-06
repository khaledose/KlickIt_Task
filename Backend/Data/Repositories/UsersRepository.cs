using Data.IRepositories;
using Domain.Entities;

namespace Data.Repositories;

public class UsersRepository : GenericRepository<User>, IUsersRepository
{
    public UsersRepository(KlickItContext context) : base(context)
    {
    }
}

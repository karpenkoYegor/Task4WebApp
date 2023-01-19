using System.Linq.Expressions;
using Task4WebApp.Data.Entities;

namespace Task4WebApp.Data;

public class UserDataRepository : RepositoryBase<UserData>, IUserDataRepository
{
    public UserDataRepository(AppDbContext context) : base(context)
    {
    }
}
using Microsoft.EntityFrameworkCore;
using Task4WebApp.Data.Entities;

namespace Task4WebApp.Data;

public class UserAccountRepository : RepositoryBase<UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(AppDbContext context) : base(context)
    {
    }
    public IEnumerable<UserAccount> GetUsers()
    {
        var data = Context.UserAccounts.Include(u=>u.UserData).ToList();
        return data;
    }

    public UserAccount GetUserByEmail(string email)
    {
        return FindByEmail(u => u.Email == email);
    }
    
}
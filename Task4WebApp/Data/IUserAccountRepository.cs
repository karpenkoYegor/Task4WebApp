using Task4WebApp.Data.Entities;

namespace Task4WebApp.Data;

public interface IUserAccountRepository : IRepositoryBase<UserAccount>
{
    IEnumerable<UserAccount> GetUsers();
    UserAccount GetUserByEmail(string email);
}
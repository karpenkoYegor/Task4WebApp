namespace Task4WebApp.Data;

public interface IRepositoryWrapper
{
    IUserAccountRepository UserAccount { get; }
    IUserDataRepository UserData { get; }
    void Save();
}
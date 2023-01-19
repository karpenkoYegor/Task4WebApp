namespace Task4WebApp.Data;

public class RepositoryWrapper : IRepositoryWrapper
{
    private AppDbContext _context;
    private IUserAccountRepository _userAccountRepository;
    private IUserDataRepository _userDatesRepository;

    public RepositoryWrapper(AppDbContext context)
    {
        _context = context;
    }

    public IUserAccountRepository UserAccount
    {
        get
        {
            if (_userAccountRepository == null)
                _userAccountRepository = new UserAccountRepository(_context);
            return _userAccountRepository;
        }
    }

    public IUserDataRepository UserData { get
        {
            if (_userDatesRepository == null)
                _userDatesRepository = new UserDataRepository(_context);
            return _userDatesRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
using Task4WebApp.Data.Entities;

namespace Task4WebApp.Models;

public class UserRowModel
{
    public UserAccount User { get; set; }
    public CheckboxOption Checkbox { get; set; }

    public UserRowModel(UserAccount user, CheckboxOption option)
    {
        User = user;
        Checkbox = option;
    }
}
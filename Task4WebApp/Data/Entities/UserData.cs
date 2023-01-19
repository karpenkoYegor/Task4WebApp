using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task4WebApp.Data.Entities;

public class UserData
{
    [Key]
    public int UserDataId { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime LastLoginTime { get; set; }
    public string Name { get; set; }
    public bool IsBlocked { get; set; }
    public string UserAccountId { get; set; }
    [ForeignKey("UserAccountId")]
    public UserAccount UserAccount { get; set; }

    public UserData(DateTime registerDate, DateTime lastLoginTime, string name, string userAccountId)
    {
        RegisterDate = registerDate;
        LastLoginTime = lastLoginTime;
        Name = name;
        IsBlocked = false;
        UserAccountId = userAccountId;
    }
}
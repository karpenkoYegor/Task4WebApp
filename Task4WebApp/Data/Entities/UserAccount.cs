using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Task4WebApp.Data.Entities;

public class UserAccount : IdentityUser
{
    public int UserDataId { get; set; }
    public UserData UserData { get; set; }
}

public enum Status
{
    Admin,
    User
}
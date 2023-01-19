using Task4WebApp.Data.Entities;

namespace Task4WebApp.Models;

public class IndexViewModel
{
    public List<UserRowModel> Users { get; set; }
    public List<string> UniqueName { get; set; }
}
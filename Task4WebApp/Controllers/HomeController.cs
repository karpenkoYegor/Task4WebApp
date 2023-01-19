using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Task4WebApp.Data;
using Task4WebApp.Data.Entities;
using Task4WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;

namespace Task4WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly SignInManager<UserAccount> _signInManager;
        private IEnumerable<UserAccount> _users;
        private readonly IJSRuntime _js;

        public HomeController(IJSRuntime js, SignInManager<UserAccount> signInManager, IRepositoryWrapper repository)
        {
            _signInManager = signInManager;
            _repository = repository;
            _users = _repository.UserAccount.GetUsers();
            _js = js;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            var currentUser = _users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (currentUser == null) return RedirectToAction("Login", "Account");
            if (!currentUser.UserData.IsBlocked)
            { 
                List<UserRowModel> usersModel = new List<UserRowModel>();
                foreach (var user in _users)
                {
                    var checkbox = new CheckboxOption()
                    {
                        IsChecked = false,
                        Value = $"{user.UserData.UserDataId}",
                        Description = $"Option {user.UserData.UserDataId}"
                    };
                    usersModel.Add(new UserRowModel(user, checkbox));
                }

                IndexViewModel model = new IndexViewModel()
                {
                    Users = usersModel
                };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult BlockUsers(IndexViewModel model)
        {
            SetIsBlocked(true, model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UnBlockUsers(IndexViewModel model)
        {
            SetIsBlocked(false, model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteUsers(IndexViewModel model)
        {
            var currentUser = _users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (currentUser != null && !currentUser.UserData.IsBlocked)
            {
                foreach (var userId in model.UniqueName)
                {
                    var user = _users.Where(u => u.UserData.UserDataId == Convert.ToInt32(userId)).First();
                    _repository.UserAccount.Delete(user);
                }
                _repository.Save();
            }
            return RedirectToAction("Index");
        }

        public void SetIsBlocked(bool isBlocked, IndexViewModel model)
        {
            var currentUser = _users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (currentUser != null && !currentUser.UserData.IsBlocked && model.UniqueName != null)
            {
                foreach (var userId in model.UniqueName)
                {
                    var userData = _repository.UserData.FindById(
                        u => u.UserDataId == Convert.ToInt32(userId));
                    userData.IsBlocked = isBlocked;
                    _repository.UserData.Update(userData);
                }
                _repository.Save();
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
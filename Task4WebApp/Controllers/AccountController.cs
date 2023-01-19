using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task4WebApp.Data;
using Task4WebApp.Data.Entities;
using Task4WebApp.Models;

namespace Task4WebApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<UserAccount> _userManager;
    private readonly SignInManager<UserAccount> _signInManager;
    private readonly IRepositoryWrapper _repository;

    public AccountController(UserManager<UserAccount> userManager, 
        SignInManager<UserAccount> signInManager,
        IRepositoryWrapper repository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _repository = repository;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            Guid id = Guid.NewGuid();
            UserAccount user = new UserAccount()
            {
                Email = model.Email,
                UserName = model.Email,
                Id = id.ToString()
            };
            UserData data = new UserData(DateTime.Now, DateTime.Now, model.Name, id.ToString());
            _repository.UserData.Create(data);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result =
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                var currentUser = _userManager.FindByEmailAsync(model.Email).Result;
                var currentUserData = _repository.UserData.FindById(u => u.UserAccountId == currentUser.Id);
                currentUserData.LastLoginTime = DateTime.Now;
                _repository.UserData.Update(currentUserData);
                _repository.Save();
                return RedirectToAction("Index", "Home", model.Email);
            }
            ModelState.AddModelError("", "Неправильный логин и (или) пароль");
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
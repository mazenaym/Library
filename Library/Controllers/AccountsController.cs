﻿using Library.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class AccountsController : Controller
    {

        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }

        public IActionResult Register()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email

                };

                var res = _UserManager.CreateAsync(user, model.Password).Result;
                if (res.Succeeded) 
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid) 
            { 
                var user = _UserManager.FindByEmailAsync(model.Email).Result;
                if (user is not null)
                { 
                    if (_UserManager.CheckPasswordAsync(user, model.Password).Result)
                    {
                        var res = _SignInManager.PasswordSignInAsync(user, model.Password, false, false).Result;
                        if (res.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            
            }
            ModelState.AddModelError(string.Empty, "Email or password is incorrect");
             

            return View();
        }
    } 
}
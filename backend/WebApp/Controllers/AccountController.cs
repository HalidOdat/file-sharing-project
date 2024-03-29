﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AccountController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
    : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        RegisterModel model = new RegisterModel();
        return View(model);
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Register(RegisterModel request)
    {
        if (!ModelState.IsValid) return View(request);
        var userCheck = await userManager.FindByEmailAsync(request.Email);
        if (userCheck != null)
        {
            ModelState.AddModelError("message", "Email already exists.");
            return View(request);
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            NormalizedUserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true,
        };
        var result = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            return RedirectToAction("Login");
        }

        if (result.Errors.Any())
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("message", error.Description);
            }
        }

        return View(request);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        LoginModel model = new LoginModel();
        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is { EmailConfirmed: false })
        {
            ModelState.AddModelError("message", "Email not confirmed yet");
            return View(model);

        }
        if (await userManager.CheckPasswordAsync(user, model.Password) == false)
        {
            ModelState.AddModelError("message", "Invalid credentials");
            return View(model);

        }

        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);

       {
            await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("message", "Invalid login attempt");
        return View(model);
    }


    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}

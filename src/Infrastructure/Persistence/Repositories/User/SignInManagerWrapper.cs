
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class SignInManagerWrapper : ISignInManager
{
    private readonly SignInManager<UserEntity> _signInManager;

    public SignInManagerWrapper(SignInManager<UserEntity> signInManager)
    {
        _signInManager = signInManager;
    }
    public bool IsSigned(ClaimsPrincipal claims)
    {
        return _signInManager.IsSignedIn(claims);
    }

    public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
        return await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}

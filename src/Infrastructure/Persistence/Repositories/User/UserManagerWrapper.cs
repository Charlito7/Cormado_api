using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class UserManagerWrapper : IUserManager
{
    private readonly UserManager<UserEntity> _userManager; 

    public UserManagerWrapper(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> AddToRoleAsync(UserEntity entity, string role)
    {
        return await _userManager.AddToRoleAsync(entity, role);
    }

    public async Task<IdentityResult> CreateAsync(UserEntity UserEntity, string password)
    {
        return await _userManager.CreateAsync(UserEntity, password);
    }

    public async Task<UserEntity> FindByEmailAsync(string emailId)
    {
        return await _userManager.FindByEmailAsync(emailId);
    }

    public async Task<UserEntity> FindByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<UserEntity> FindByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<string> GetPasswordResetTokenAsync(UserEntity UserEntity)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(UserEntity);
    }

    public async Task<IdentityResult> ResetPasswordAsync(UserEntity UserEntity, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(UserEntity, token, newPassword);
    }

    public async Task<IdentityResult> UpdateAsync(UserEntity UserEntity)
    {
        return await _userManager.UpdateAsync(UserEntity);
    }
}

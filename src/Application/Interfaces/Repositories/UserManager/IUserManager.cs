using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IUserManager
{
    Task<UserEntity> FindByIdAsync(string userId);
    Task<UserEntity> FindByNameAsync(string userName);
    Task<UserEntity> FindByEmailAsync(string emailId);
    Task<IdentityResult> CreateAsync(UserEntity UserEntity, string password);
    Task<IdentityResult> UpdateAsync(UserEntity UserEntity);
    Task<string> GetPasswordResetTokenAsync(UserEntity UserEntity);
    Task<IdentityResult> ResetPasswordAsync(UserEntity UserEntity, string token, string newPassword);
    Task<IdentityResult> AddToRoleAsync(UserEntity entity, string role);
    
    

}

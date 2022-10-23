using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class UserEntity : IdentityUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override string? Id { get; set; }
    public override string? UserName { get; set; }
    public override string? Email { get; set; }
    public override string? NormalizedUserName { get; set; }
    public override string? NormalizedEmail { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public DateTime CreatedOn { get; set; }

    public string? Password { get; set; }

    //auditable
    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public bool? IsDeleted { get; set; }
    public DateTime? LastDeleted { get; set; }
    public string? LastDeletedBy { get; set; }

    // Note: overriding properties in IdentityUser base class, so that EF generates the columns in right order
    public virtual DateTimeOffset? LockoutEnd { get; set; }
    public override bool TwoFactorEnabled { get; set; }
    public override bool PhoneNumberConfirmed { get; set; }
    public override string? PhoneNumber { get; set; }
    public virtual string? ConcurrencyStamp { get; set; }
    public override string? SecurityStamp { get; set; }
    public override string? PasswordHash { get; set; }
    public override bool EmailConfirmed { get; set; }
    public override bool LockoutEnabled { get; set; }
    public override int AccessFailedCount { get; set; }
}

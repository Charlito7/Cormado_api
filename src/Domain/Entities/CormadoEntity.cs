using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class CormadoEntity : AuditableEntity
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? CommercialName { get; set; }
    public string? PointOfContact { get; set; }
    public string? Email { get; set; }
    public bool IfEmailConfirmed { get; set; }  
    public string? Phone { get; set; }

    public string? Password { get; set; }
    public string? PasswordHash { get; set; }
    public bool IsFirstPasswordChange { get; set; }
    public byte[]? Salt { get; set; }

    public string? Address { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }

    public DateTime? CreatedDate { get; set; }


}
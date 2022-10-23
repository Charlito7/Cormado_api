using Application.Models;
using Application.WebContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICormadoServices
{
    Task<ServiceResult<CreateCormadoResponse>> CreateCormadoAsync(CreateCormadoRequest request,
                                               ClaimsPrincipal authenticatedUser);
}

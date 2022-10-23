using Application.Common.Helpers;
using Application.Common.Helpers.Security;
using Application.Interfaces;
using Application.Models;
using Application.WebContract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class CormadoServices: ICormadoServices
{   private readonly ICormadoRepository _repository;
    private HashPassword hashPassword;
    public CormadoServices(ICormadoRepository repository) { 
        _repository = repository;
        this.hashPassword = new HashPassword();
    }

    public async Task<ServiceResult<CreateCormadoResponse>>
         CreateCormadoAsync(CreateCormadoRequest request, ClaimsPrincipal authenticatedUser)
    {
        //define an error result
        ServiceResult<CreateCormadoResponse> errorResult =
                    new ServiceResult<CreateCormadoResponse>(HttpStatusCode.BadRequest);

        //check if request is null to avoid null exception
        // check for empty or null string
        if (request == null
            || string.IsNullOrEmpty(request.LegalName)
            || string.IsNullOrEmpty(request.CommercialName)
            || string.IsNullOrEmpty(request.Phone)
            || string.IsNullOrEmpty(request.Address)
            || string.IsNullOrEmpty(request.City)) 

        {
            return errorResult;
        }

        //Generate the password
        var pwd = GenerateFirstPassword.FirstPassword(request.CommercialName);
        var pwdHash = hashPassword.HashText(pwd);

        CormadoEntity cormadoEntity = new CormadoEntity
        {
           LegalName = request.LegalName,
           CommercialName = request.CommercialName,
           Phone = request.Phone,
           Address = request.Address,
           City = request.City,
           Email = request.Email,
           PointOfContact = "test POC",
           Password = pwd,
           PasswordHash = pwdHash.Hash,
           Salt = pwdHash.Salt,
            IsFirstPasswordChange = false
        };

       var result =  await _repository.CreateCormadoAsync(cormadoEntity);



        return new ServiceResult<CreateCormadoResponse>(new CreateCormadoResponse());
    }
}

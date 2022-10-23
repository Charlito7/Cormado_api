using Application.Interfaces;
using Application.Interfaces.Token;
using Application.WebContract;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Base;

namespace WebApi.Controllers.Cormado;

//[Authorize(Policy = "CreateCormadoRight")]
public class CormadoController : BaseAuthController
{
    private readonly ITokenServices _tokenServives;
    private readonly IConfiguration _config;
    private readonly ICormadoServices _createCormadoServices;

   
    public CormadoController(ITokenServices tokenServives, 
                                  IConfiguration configuration,
                                  ICormadoServices createCormadoServices) : base(tokenServives, configuration)
    {
        _tokenServives = tokenServives;
        _config = configuration;
        _createCormadoServices = createCormadoServices;
    }


    [HttpPost]
    //[Authorize]
    [Route("Create", Name = "CreateCormado")]
    public async Task<IActionResult> CreateController(CreateCormadoRequest request)
    {
        if (User.Identity.IsAuthenticated) { 
        }


       await _createCormadoServices.CreateCormadoAsync(request,User);
      
        return BadRequest();
    }


}

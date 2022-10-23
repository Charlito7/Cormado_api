using Application.Interfaces.Token;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Base;

namespace WebApi.Controllers.Product
{
    public class ProductController : BaseAuthController
    {
        private readonly ITokenServices _tokenServives;
        private readonly IConfiguration _config;
        
        public ProductController(ITokenServices tokenServives, IConfiguration configuration) : base(tokenServives, configuration)
        {
            _tokenServives = tokenServives;
            _config = configuration;
        }

        [HttpPost]
        //[Authorize]
        [Route("Create", Name = "CreateCormado")]
        public async Task<IActionResult> CreateController(CreateCormadoRequest request)
        {
            if (User.Identity.IsAuthenticated)
            {
            }


            await _createCormadoServices.CreateCormadoAsync(request, User);

            return BadRequest();
        }
    }
}

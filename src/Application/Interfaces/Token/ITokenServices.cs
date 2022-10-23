using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Token
{
    public interface ITokenServices
    {
        string BuildToken(string key, string issuer, UserEntity user);
        bool ValidateToken(string key, string issuer, string audience, string token);
    }
}

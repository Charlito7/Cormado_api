using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICormadoRepository
{
    Task<CormadoEntity> GetByEmailAsync(string email);
    Task<CormadoEntity> UpdateCormado(CormadoEntity cormadoEntity);
    Task<CormadoEntity> CreateCormadoAsync(CormadoEntity cormadoEntity);
}


using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class CormadoRepository : ICormadoRepository
{
    private ApplicationDbContext _context;
    public CormadoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<CormadoEntity> CreateCormadoAsync(CormadoEntity cormadoEntity)
    {
        throw new NotImplementedException();
    }

    public async Task<CormadoEntity> GetByEmailAsync(string email)
    {

#pragma warning disable CS8603 // Possible null reference return.
        return await _context.Cormados.Where(Cormado => Cormado.Email == email)
                    .FirstOrDefaultAsync<CormadoEntity>();
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<CormadoEntity> UpdateCormado(CormadoEntity CormadoEntity)
    {
        try
        {
            var test =  _context.Cormados.Update(CormadoEntity);
            await _context.SaveChangesAsync();
            return CormadoEntity;
        }
    catch (Exception ex) {
            return null;
        }

    }
}

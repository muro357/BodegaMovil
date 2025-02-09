using BodegaMovil.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.UseCases.Interfaces
{
    public interface ITiendaRepository
    {
        Task<Tienda> GetById(int id);

        Task<List<Tienda>> GetTiendasHabilitadas(); 
    }
}
